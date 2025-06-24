using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.LicenceRelated;
using Back.Modules.LicenceModule.Dtos;
using Back.Modules.LicenceModule.Services;
using Back.Models.General;

using Dapper;
using System.Data;


public class ActivationInfo
{
    public string LicenceId { get; set; } = null!;
    public string DevicePrint { get; set; } = null!;
    public string LicenceOrderId  { get; set; } = null!;
}


namespace Back.Modules.LicenceModule.Services
{
    public class LicencePublicService : IlicencePublicService
    {
        private readonly ILicenceService _licenceService;
        private readonly ILicenceOptionService _licenceOptionService;
        private readonly IDbConnection _db;

        public LicencePublicService(
            ILicenceService licenceService,
            ILicenceOptionService licenceOptionService,
            IDbConnection db)
        {
            _licenceService = licenceService;
            _licenceOptionService = licenceOptionService;
            _db = db;
        }

        public async Task<bool> ActivateLicenceAsync(string licenceKey, string device_print, string email, string tel)
        {
            Console.WriteLine($"[ActivateLicenceAsync] LicenceKey: {licenceKey}, DevicePrint: {device_print}, Email: {email}, Tel: {tel}");

            // TODO: implement logic here (e.g., check key, store device info, activate in DB, etc.)
            await Task.Delay(100); // simulate async work

            return true; // change based on actual activation result
        }

        
      
        public async Task<LicenceDetailsDto?> CheckLicenceAsync(string licence_id, string ip, string device_print, string tel, string email)
        {
            const string userQuery = @"
                SELECT * FROM users 
                WHERE (email = @Email OR phone = @Tel) AND is_archived = false;
            ";

            var user = await _db.QueryFirstOrDefaultAsync<User>(userQuery, new { Email = email, Tel = tel });

            if (user == null)
                throw new KeyNotFoundException("User not found");

            Console.WriteLine(user.UserId);

            // TODO: Implement IP validation logic
            bool isIpValid = true;
            if (!isIpValid)
                throw new UnauthorizedAccessException("Invalid IP address");

            Console.WriteLine("IP valid");

            const string ActivationQuery = @"
                SELECT lo.licence_id AS LicenceId, lo.licence_order_id, la.device_print AS DevicePrint 
                FROM licence_activation la
                JOIN licence_order lo ON lo.licence_order_id = la.licence_order_id
                WHERE lo.licence_id = @LicenceId AND la.user_id = @UserId AND la.is_archived = false;
            ";

            var activation = await _db.QueryFirstOrDefaultAsync<ActivationInfo>(ActivationQuery, new
            {
                LicenceId = licence_id,
                UserId = user.UserId
            });

            if (activation == null)
                throw new KeyNotFoundException("Activation record not found");

            if (string.IsNullOrWhiteSpace(device_print) || activation.DevicePrint != device_print)
            {
                Console.WriteLine("Device incorrect: " + device_print);
                throw new UnauthorizedAccessException("Device fingerprint does not match");
            }

            const string query = @"
                SELECT lo.option_id, lo.licence_id, lo.option_name, lo.description, lo.price, lo.is_archived
                FROM licence_bundle lb
                JOIN licence_option lo ON lb.option_id = lo.option_id
                WHERE lb.licence_order_id = @LicenceOrderId;
            ";

            var licenceOptions = (await _db.QueryAsync<LicenceOption>(query, new { LicenceOrderId = activation.LicenceOrderId })).ToList();

            var licence = await _licenceService.GetByIdAsync(activation.LicenceId);

            if (licence == null)
                throw new KeyNotFoundException("Licence not found");

            if (licence.IsArchived)
                throw new InvalidOperationException("Licence is archived");

            return new LicenceDetailsDto
            {
                LicenceId = licence.LicenceId,
                Name = licence.LicenceName,
                Description = licence.Description,
                MaxDevices = licence.MaxDevices,
                Duration = licence.Duration,
                GracePeriod = int.TryParse(licence.GracePeriod, out var grace) ? grace : 0,
                PublicKey = licence.PublicKey,
                Price = licence.Price,
                IsArchived = licence.IsArchived,
                Options = licenceOptions ?? new List<LicenceOption>()
            };
        }

        public async Task<LicenceDetailsDto?> GetLicenceDetailsAsync(string licenceId)
        {
            var licence = await _licenceService.GetByIdAsync(licenceId);
            if (licence == null || licence.IsArchived)
            {
                return null;
            }

            var options = await _licenceOptionService.GetByLicenceIdAsync(licenceId);

            return new LicenceDetailsDto
            {
                LicenceId = licence.LicenceId,
                Name = licence.LicenceName,
                Description = licence.Description,
                MaxDevices = licence.MaxDevices,
                Duration = licence.Duration,
                GracePeriod = int.TryParse(licence.GracePeriod, out var grace) ? grace : 0,
                PublicKey = licence.PublicKey,
                Price = licence.Price,
                IsArchived = licence.IsArchived,
                Options = options != null ? new List<LicenceOption>(options) : new()
            };
        }
    }
}
