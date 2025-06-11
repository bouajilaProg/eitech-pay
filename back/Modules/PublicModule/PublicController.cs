
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back.Models.LicenceRelated;
using Back.Modules.PublicModule.Services;

namespace Back.Modules.PublicModule
{
    [ApiController]
    [Route("api/v1/licence")]
    public class LicenceV1Controller : ControllerBase
    {
        private readonly IPublicLicenceService _publicLicenceService;

        public LicenceV1Controller(IPublicLicenceService publicLicenceService)
        {
            _publicLicenceService = publicLicenceService;
        }

        // GET api/v1/licence/{licenceId}
        [HttpGet("{licenceId}")]
        public async Task<ActionResult<LicenceDetailedDto>> GetDetailedLicence(int licenceId)
        {

          // debug write 
          Console.WriteLine($"Received request for licenceId: {licenceId}");


            if (licenceId <= 0)
                return BadRequest("Invalid licenceId");

            var detailedLicence = await _publicLicenceService.GetDetailedLicenceById(licenceId);

            if (detailedLicence == null)
                return NotFound();

            return Ok(detailedLicence);
        }
    }
}
