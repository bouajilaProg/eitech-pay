
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

        public string Login(string username, string password)
        {
            const string query = "SELECT * FROM admin WHERE username = @Username AND is_archived = false";
            var admin = _db.QueryFirstOrDefault<Admin>(query, new { Username = username });

            if (admin == null || !VerifyPassword(password, admin.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            return GenerateJwtToken(admin);
        }

       
     
        public (string AdminId, string AdminName) CheckToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out _);

            var AdminId = principal.FindFirst("AdminId")?.Value;
            var AdminName = principal.FindFirst("AdminName")?.Value;

            Console.WriteLine($"AdminId: {AdminId}, AdminName: {AdminName}");

            if (string.IsNullOrEmpty(AdminId) || string.IsNullOrEmpty(AdminName))
            {
                throw new SecurityTokenException("Required claims missing.");
            }

            return (AdminId, AdminName);
        }


        public void ChangePassword(string oldpasswd, string newpasswd)
        {
            const string query = "SELECT * FROM admin WHERE is_archived = false LIMIT 1";
            var admin = _db.QueryFirstOrDefault<Admin>(query);

            if (admin == null || !VerifyPassword(oldpasswd, admin.Password))
                throw new UnauthorizedAccessException("Incorrect current password");

            _db.Execute("UPDATE admin SET Password = @Password WHERE admin_id = @AdminId",
                new { Password = newpasswd, admin.AdminId });
        }

        public void ChangePaymentDetails(string apiKey, string konnectId)
        {
            if (!ValidateApiKey(apiKey) || !ValidateKonnectId(konnectId))
                throw new ArgumentException("Invalid API Key or Konnect ID");

            const string query = "UPDATE admin SET api_key = @ApiKey WHERE is_archived= false";
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
                    new Claim("AdminId", admin.AdminId.ToString()),
                    new Claim("AdminName", admin.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
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
            return input == hashed;
        }
    }
}
