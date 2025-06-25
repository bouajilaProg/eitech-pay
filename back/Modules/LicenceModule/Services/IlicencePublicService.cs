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

      // check licence
      Task<LicenceDetailsDto?> CheckLicenceAsync(string licence_id,string ip , string device_print, string tel,string email);

      // get licence details
      Task<LicenceDetailsDto?> GetLicenceDetailsAsync(string LicenceId);

      Task CreateLicenceOrderAsync(CreateLicenceOrderDto dto);
    }
}
