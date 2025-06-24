using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public LicencePublicService(
            ILicenceService licenceService,
            ILicenceOptionService licenceOptionService,
            ILicenceOrderService licenceOrderService)
        {
            _licenceService = licenceService;
            _licenceOptionService = licenceOptionService;
            _licenceOrderService = licenceOrderService;
        }

        public async Task<ActivationResultDto> ActivateLicenceAsync(string licenceKey, string device_print, string email, string tel)
        {
            Console.WriteLine($"[ActivateLicenceAsync] LicenceKey: {licenceKey}, DevicePrint: {device_print}, Email: {email}, Tel: {tel}");

            // TODO: implement logic here (e.g., check key, store device info, activate in DB, etc.)
            await Task.Delay(100); // simulate async work

            return new ActivationResultDto(); // change based on actual activation result
        }

        public async Task<LicenceDetailsDto?> CheckLicenceAsync(string licenceKey, string device_print, int userId)
        {
            Console.WriteLine($"[CheckLicenceAsync] LicenceKey: {licenceKey}, DevicePrint: {device_print}, UserId: {userId}");

            // TODO: implement logic here (e.g., lookup licence, validate binding with device, etc.)
            await Task.Delay(100); // simulate async work

            // return dummy data
            return new LicenceDetailsDto();
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

        public async Task CreateLicenceOrderAsync(CreateLicenceOrderDto dto)
        {
            var id = IdGenerator.GenerateId("lo"); // your ID generator

            var order = new LicenceOrder
            {
                LicenceOrderId = int.Parse(id.Split('_')[1]), // Extract numeric part
                UserId = dto.UserId,
                LicenceId = dto.LicenceId,
                PurchaseDate = DateTime.UtcNow,
                Status = LicenceOrderStatus.Active,
                Reseller = "konnect",
                IsArchived = false,
                PrivateKey = "", // Fill if needed
            };

            await _licenceOrderService.CreateAsync(order);
        }

    }
    
}
