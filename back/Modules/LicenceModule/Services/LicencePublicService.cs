using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.LicenceRelated;
using Back.Modules.LicenceModule.Dtos;
using Back.Modules.LicenceModule.Services;

namespace Back.Modules.LicenceModule.Services
{
    public class LicencePublicService : IlicencePublicService
    {
        private readonly ILicenceService _licenceService;
        private readonly ILicenceOptionService _licenceOptionService;

        public LicencePublicService(
            ILicenceService licenceService,
            ILicenceOptionService licenceOptionService)
        {
            _licenceService = licenceService;
            _licenceOptionService = licenceOptionService;
        }

        public async Task<bool> ActivateLicenceAsync(string licenceKey, string device_print, string email, string tel)
        {
            Console.WriteLine($"[ActivateLicenceAsync] LicenceKey: {licenceKey}, DevicePrint: {device_print}, Email: {email}, Tel: {tel}");

            // TODO: implement logic here (e.g., check key, store device info, activate in DB, etc.)
            await Task.Delay(100); // simulate async work

            return true; // change based on actual activation result
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
                GracePeriod = int.TryParse(licence.GracePeriod, out var grace) ? grace : 0,
                PublicKey = licence.PublicKey,
                Price = licence.Price,
                IsArchived = licence.IsArchived,
                Options = options != null ? new List<LicenceOption>(options) : new()
            };
        }
    }
}
