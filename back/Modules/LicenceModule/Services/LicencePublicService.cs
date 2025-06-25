using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Back.Models.LicenceRelated;
using Back.Modules.LicenceModule.Dtos;
using Back.Modules.LicenceModule.Services;
using Back.Utils;

namespace Back.Modules.LicenceModule.Services
{
    public class LicencePublicService : IlicencePublicService
    {
        private readonly ILicenceService _licenceService;
        private readonly ILicenceOptionService _licenceOptionService;
        private readonly ILicenceOrderService _licenceOrderService;
        private readonly IDbConnection _db;

        public LicencePublicService(
            ILicenceService licenceService,
            ILicenceOptionService licenceOptionService,
            ILicenceOrderService licenceOrderService,
            IDbConnection db)
        {
            _licenceService = licenceService;
            _licenceOptionService = licenceOptionService;
            _licenceOrderService = licenceOrderService;
            _db = db;
        }

        public async Task<ActivationResultDto> ActivateLicenceAsync(string email, string licenceKey, string deviceFingerprint, string ipAddress)
        {
            try
            {
                Console.WriteLine($"111111111[ActivateLicenceAsync] Email: {email}, LicenceKey: {licenceKey}, DeviceFingerprint: {deviceFingerprint}, IP: {ipAddress}");

                var licenceOrder = await ValidateLicenceKeyAsync(email, licenceKey);
                if (licenceOrder == null)
                {
                    return new ActivationResultDto { Success = false, Message = "Invalid license" };
                }
                Console.WriteLine($"222222222[ActivateLicenceAsync] Email: {email}, LicenceKey: {licenceKey}, DeviceFingerprint: {deviceFingerprint}, IP: {ipAddress}");

                var canActivate = await CheckDeviceLimitAsync(licenceOrder.LicenceOrderId, licenceOrder.LicenceId);
                if (!canActivate)
                {
                    return new ActivationResultDto { Success = false, Message = "User/device limit exceeded" };
                }
                Console.WriteLine($"333333333[ActivateLicenceAsync] Email: {email}, LicenceKey: {licenceKey}, DeviceFingerprint: {deviceFingerprint}, IP: {ipAddress}");


                var alreadyActivated = await IsDeviceAlreadyActivatedAsync(licenceOrder.LicenceOrderId, deviceFingerprint);
                if (alreadyActivated)
                {
                    return new ActivationResultDto { Success = false, Message = "Device already activated" };
                }
                Console.WriteLine($"444444444[ActivateLicenceAsync] Email: {email}, LicenceKey: {licenceKey}, DeviceFingerprint: {deviceFingerprint}, IP: {ipAddress}");


                var activationId = await RegisterDeviceActivationAsync(licenceOrder.LicenceOrderId, licenceOrder.UserId, deviceFingerprint);

                return new ActivationResultDto
                {
                    Success = true,
                    Message = "License activated (access granted)",
                    ActivationId = activationId.ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error activating licence: {ex.Message}");
                return new ActivationResultDto { Success = false, Message = "Activation failed due to system error" };
            }
        }

        private async Task<LicenceOrder?> ValidateLicenceKeyAsync(string email, string licenceKey)
        {
            const string query = @"
                SELECT lo.licence_order_id, lo.user_id, lo.licence_id, lo.private_key, lo.status
                FROM licence_order lo
                JOIN users u ON lo.user_id = u.user_id
                WHERE u.email = @Email 
                AND lo.private_key = @LicenceKey
                AND lo.status = 'Active'
                AND lo.is_archived = FALSE
                AND u.is_archived = FALSE";

            return await _db.QueryFirstOrDefaultAsync<LicenceOrder>(query, new { Email = email, LicenceKey = licenceKey });
        }

        private async Task<bool> CheckDeviceLimitAsync(int licenceOrderId, string licenceId)
        {
            const string maxDevicesQuery = @"
                SELECT max_devices 
                FROM licence 
                WHERE licence_id = @LicenceId 
                AND is_archived = FALSE";

            var maxDevices = await _db.QueryFirstOrDefaultAsync<int>(maxDevicesQuery, new { LicenceId = licenceId });

            const string activationCountQuery = @"
                SELECT COUNT(*) 
                FROM licence_activation 
                WHERE licence_order_id = @LicenceOrderId 
                AND is_archived = FALSE";

            var currentActivations = await _db.QueryFirstOrDefaultAsync<int>(activationCountQuery, new { LicenceOrderId = licenceOrderId });

            return currentActivations < maxDevices;
        }

        private async Task<bool> IsDeviceAlreadyActivatedAsync(int licenceOrderId, string deviceFingerprint)
        {
            const string query = @"
                SELECT COUNT(*) 
                FROM licence_activation 
                WHERE licence_order_id = @LicenceOrderId 
                AND device_print = @DeviceFingerprint 
                AND is_archived = FALSE";

            var count = await _db.QueryFirstOrDefaultAsync<int>(query, new { LicenceOrderId = licenceOrderId, DeviceFingerprint = deviceFingerprint });
            return count > 0;
        }

        private async Task<int> RegisterDeviceActivationAsync(int licenceOrderId, int userId, string deviceFingerprint)
        {
            const string query = @"
                INSERT INTO licence_activation (licence_order_id, device_print, user_id, activation_date, is_archived)
                VALUES (@LicenceOrderId, @DeviceFingerprint, @UserId, NOW(), FALSE);
                SELECT LAST_INSERT_ID();";

            return await _db.QueryFirstOrDefaultAsync<int>(query, new
            {
                LicenceOrderId = licenceOrderId,
                DeviceFingerprint = deviceFingerprint,
                UserId = userId
            });
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
                GracePeriod = licence.GracePeriod.ToString(),
                PublicKey = licence.PublicKey,
                Price = licence.Price,
                IsArchived = licence.IsArchived,
                Options = options != null ? new List<LicenceOption>(options) : new()
            };
        }

        public async Task<LicenceDetailsDto?> CheckLicenceAsync(string licenceKey, string device_print, int userId)
        {
            Console.WriteLine($"[CheckLicenceAsync] LicenceKey: {licenceKey}, DevicePrint: {device_print}, UserId: {userId}");

            await Task.Delay(100); // simulate async work
            return new LicenceDetailsDto();
        }

        public async Task CreateLicenceOrderAsync(CreateLicenceOrderDto dto)
        {
            var id = IdGenerator.GenerateId("lo");

            var order = new LicenceOrder
            {
                LicenceOrderId = int.Parse(id.Split('_')[1]),
                UserId = dto.UserId,
                LicenceId = dto.LicenceId,
                PurchaseDate = DateTime.UtcNow,
                Status = LicenceOrderStatus.Active,
                Reseller = "konnect",
                IsArchived = false,
                PrivateKey = "",
            };

            await _licenceOrderService.CreateAsync(order);
        }
    }
}
