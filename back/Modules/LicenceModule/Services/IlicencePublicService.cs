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

<<<<<<< HEAD

      // check licence
      Task<LicenceDetailsDto?> CheckLicenceAsync(string licence_id,string ip , string device_print, string tel,string email);

      // get licence details
      Task<LicenceDetailsDto?> GetLicenceDetailsAsync(string LicenceId);
=======
        // check if licence is valid
        Task<LicenceDetailsDto?> CheckLicenceAsync(string licenceKey, string device_print, int userId);
>>>>>>> abb80a02b327426bea1deb38338fd25f11e100ac

        // get licence details
        Task<LicenceDetailsDto?> GetLicenceDetailsAsync(string LicenceId);

        Task CreateLicenceOrderAsync(CreateLicenceOrderDto dto);
    }
}
