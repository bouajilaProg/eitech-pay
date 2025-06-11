using Back.Models.LicenceRelated;

namespace Back.Modules.PublicModule.Services{

  public interface IPublicLicenceService
  {
      // Get licence detailed
      Task<LicenceDetailedDto?> GetDetailedLicenceById(int licenceId);

  }


}
