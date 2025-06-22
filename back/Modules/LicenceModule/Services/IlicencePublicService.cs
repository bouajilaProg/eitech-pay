using System.Collections.Generic;
using System.Threading.Tasks;

using Back.Models.LicenceRelated;

using Back.Modules.LicenceModule.Dtos;

namespace Back.Modules.LicenceModule.Services
{
    public interface IlicencePublicService 
    {

        // activate licence
      Task<bool> ActivateLicenceAsync(string licenceKey,string device_print,string email,string tel);

      // check if licence is valid
      Task<LicenceDetailsDto?> CheckLicenceAsync(string licenceKey,string device_print,int userId);

      // get licence details



    }
}
