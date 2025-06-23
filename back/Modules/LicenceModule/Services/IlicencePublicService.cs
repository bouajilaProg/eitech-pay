using System.Collections.Generic;
using System.Threading.Tasks;

using Back.Models.LicenceRelated;

using Back.Modules.LicenceModule.Dtos;

namespace Back.Modules.LicenceModule.Services
{
    public interface IlicencePublicService 
    {
        // activate licence
        Task<ActivationResultDto> ActivateLicenceAsync(string email, string licenceKey, string deviceFingerprint, string ipAddress);

        // check if licence is valid
        Task<LicenceDetailsDto?> CheckLicenceAsync(string licenceKey, string device_print, int userId);

        // get licence details
        Task<LicenceDetailsDto?> GetLicenceDetailsAsync(string LicenceId);
    }
}
