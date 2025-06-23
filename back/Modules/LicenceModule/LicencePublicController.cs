
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
    }
}
