using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.LicenceRelated;
using Microsoft.AspNetCore.Mvc;
using Back.Modules.LicenceModule.Services;

namespace Back.Modules.LicenceModule
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicenceController : ControllerBase
    {
        private readonly ILicenceService _licenceService;
        private readonly ILicenceOptionService _licenceOptionService;

        public LicenceController(
            ILicenceService licenceService,
            ILicenceOptionService licenceOptionService)
        {
            _licenceService = licenceService;
            _licenceOptionService = licenceOptionService;
        }

        // -------------------- Licence Endpoints --------------------

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Licence>>> GetAll()
        {
            var licences = await _licenceService.GetAllAsync();
            return Ok(licences);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Licence>> GetById(int id)
        {
            var licence = await _licenceService.GetByIdAsync(id);
            if (licence == null)
                return NotFound();

            return Ok(licence);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Licence licence)
        {
            var newId = await _licenceService.CreateAsync(licence);
            return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Licence licence)
        {
            if (id != licence.LicenceId)
                return BadRequest("ID mismatch");

            var success = await _licenceService.UpdateAsync(licence);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _licenceService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // -------------------- Licence Option Endpoints --------------------

        // GET: api/licence/options
        [HttpGet("options")]
        public async Task<ActionResult<IEnumerable<LicenceOption>>> GetAllOptions()
        {
            var options = await _licenceOptionService.GetAllAsync();
            return Ok(options);
        }

        // GET: api/licence/options/{id}
        [HttpGet("options/{id}")]
        public async Task<ActionResult<LicenceOption>> GetOptionById(int id)
        {
            var option = await _licenceOptionService.GetByIdAsync(id);
            if (option == null)
                return NotFound();

            return Ok(option);
        }

        // GET: api/licence/{licenceId}/options
        [HttpGet("{licenceId}/options")]
        public async Task<ActionResult<IEnumerable<LicenceOption>>> GetOptionsByLicenceId(int licenceId)
        {
            var options = await _licenceOptionService.GetByLicenceIdAsync(licenceId);
            return Ok(options);
        }

        // POST: api/licence/options
        [HttpPost("options")]
        public async Task<ActionResult<int>> CreateOption([FromBody] LicenceOption option)
        {
            var newId = await _licenceOptionService.CreateAsync(option);
            return CreatedAtAction(nameof(GetOptionById), new { id = newId }, newId);
        }

        // PUT: api/licence/options/{id}
        [HttpPut("options/{id}")]
        public async Task<IActionResult> UpdateOption(int id, [FromBody] LicenceOption option)
        {
            if (id != option.OptionId)
                return BadRequest("ID mismatch");

            var success = await _licenceOptionService.UpdateAsync(option);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/licence/options/{id}
        [HttpDelete("options/{id}")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            var success = await _licenceOptionService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
