
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Back.Models.General;
using Microsoft.IdentityModel.Tokens;
using Dapper;

namespace Back.Modules.AdminModule.Services
{
    public class AdminService : IAdminService
    {
        private readonly IDbConnection _db;
        private readonly string _jwtSecret;

        public AdminService(IDbConnection db, IConfiguration config)
        {
            _db = db;
            _jwtSecret = config["Jwt:Secret"] ?? throw new ArgumentNullException("JWT secret not configured.");
        }

        public string Login(string email, string password)
        {
            const string query = "SELECT * FROM Admin WHERE user_name = @Email AND is_archived = false";
            var admin = _db.QueryFirstOrDefault<Admin>(query, new { Email = email });

            if (admin == null || !VerifyPassword(password, admin.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            return GenerateJwtToken(admin);
        }

        public bool CheckToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSecret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ChangePassword(string oldpasswd, string newpasswd)
        {
            const string query = "SELECT * FROM Admin WHERE is_archived = false LIMIT 1";
            var admin = _db.QueryFirstOrDefault<Admin>(query);

            if (admin == null || !VerifyPassword(oldpasswd, admin.Password))
                throw new UnauthorizedAccessException("Incorrect current password");

            string newHash = HashPassword(newpasswd);
            _db.Execute("UPDATE Admin SET Password = @Password WHERE admin_id = @AdminId",
                new { Password = newHash, admin.AdminId });
        }

        public void ChangePaymentDetails(string apiKey, string konnectId)
        {
            if (!ValidateApiKey(apiKey) || !ValidateKonnectId(konnectId))
                throw new ArgumentException("Invalid API Key or Konnect ID");

            const string query = "UPDATE Admin SET api_key = @ApiKey WHERE is_archived= false";
            _db.Execute(query, new { ApiKey = apiKey });
        }

        public string GenerateApiKey()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        }

        public bool ValidateKonnectId(string konnectId)
        {
            return !string.IsNullOrWhiteSpace(konnectId) && konnectId.Length >= 8;
        }

        public bool ValidatePassword(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit);
        }

        public bool ValidateApiKey(string apiKey)
        {
            return apiKey.Length == 64 && apiKey.All(c => Uri.IsHexDigit(c));
        }

        // ---------- Private Helpers ----------

        private string GenerateJwtToken(Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                    new Claim(ClaimTypes.Name, admin.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }

        private bool VerifyPassword(string input, string hashed)
        {
            return HashPassword(input) == hashed;
        }
    }
}
