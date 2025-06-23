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

        public async Task<ActivationResultDto> ActivateLicenceAsync(string email, string licenceKey, string deviceFingerprint, string ipAddress)
        {
            Console.WriteLine($"[ActivateLicenceAsync] Email: {email}, LicenceKey: {licenceKey}, DeviceFingerprint: {deviceFingerprint}, IPAddress: {ipAddress}");

            // TODO: Implement activation logic (e.g., validate licence key, check device binding, etc.)
            await Task.Delay(100); // Simulate async work

            // Return a dummy activation result for now
            return new ActivationResultDto
            {
                Success = true,
                Message = "Licence activated successfully.",
                ActivationId = Guid.NewGuid().ToString()
            };
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
                GracePeriod = licence.GracePeriod, // Ensure GracePeriod is already an integer
                PublicKey = licence.PublicKey,
                Price = licence.Price,
                IsArchived = licence.IsArchived,
                Options = options != null ? new List<LicenceOption>(options) : new()
            };
        }
    }
}
