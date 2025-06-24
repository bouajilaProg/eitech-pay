
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Back.Modules.LicenceModule.Services;
using Back.Modules.LicenceModule.Dtos;

namespace Back.Modules.LicenceModule.Controllers
{
    [ApiController]
    [Route("api/v0/licence")]
    public class LicencePublicController : ControllerBase
    {
        private readonly IlicencePublicService _licencePublicService;

        public LicencePublicController(IlicencePublicService licencePublicService)
        {
            _licencePublicService = licencePublicService;
        }

        /// <summary>
        /// Get the details of a licence by its ID.
        /// </summary>
        /// <param name="licenceId">The unique ID of the licence</param>
        /// <returns>Licence details if found and not archived; otherwise, 404</returns>
        [HttpGet("{licenceId}/details")]
        public async Task<ActionResult<LicenceDetailsDto?>> GetLicenceDetails(string licenceId)
        {
            var details = await _licencePublicService.GetLicenceDetailsAsync(licenceId);

            if (details == null)
                return NotFound(new { message = "Licence not found or archived." });

            return Ok(details);
        }

    
        [HttpGet("check/{licence_id}")]
        public async Task<ActionResult<LicenceDetailsDto>> CheckLicence(
          [FromRoute] string licence_id,
          [FromQuery] string? ip,
          [FromQuery(Name = "device")] string? device_print = null,
          [FromQuery] string? tel = null,
          [FromQuery] string? email = null)
        {
          var result = await _licencePublicService.CheckLicenceAsync(
              licence_id, ip, device_print, tel, email);

          if (result == null)
              return NotFound("Licence not found or invalid parameters.");

          return Ok(result);
        }
    } 
}
