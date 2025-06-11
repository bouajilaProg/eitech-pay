using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Back.Models.LicenceRelated;
using Back.Modules.LicenceModule.Services;
using Back.Modules.GeneralServices;
using Back.Modules.LicenceModule.Dtos;

namespace Back.Modules.LicenceModule
{
    /// <summary>
    /// Controller responsible for managing Licences and their associated options.
    /// </summary>
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

        /// <summary>
        /// Retrieves all licences.
        /// </summary>
        /// <returns>A list of all licences.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Licence>>> GetAll()
        {
            var licences = await _licenceService.GetAllAsync();
            return Ok(licences);
        }

        /// <summary>
        /// Retrieves a specific licence by its ID.
        /// </summary>
        /// <param name="id">The ID of the licence to retrieve.</param>
        /// <returns>The requested licence if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Licence>> GetById(int id)
        {
            var licence = await _licenceService.GetByIdAsync(id);
            if (licence == null)
                return NotFound();

            return Ok(licence);
        }

        /// <summary>
        /// Creates a new licence.
        /// </summary>
        /// <param name="createLicenceDto">The data to create the licence.</param>
        /// <returns>The ID of the created licence.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateLicenceDto createLicenceDto)
        {
            var newId = await _licenceService.CreateAsync(createLicenceDto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
        }

        /// <summary>
        /// Updates an existing licence.
        /// </summary>
        /// <param name="id">The ID of the licence to update.</param>
        /// <param name="licence">The updated licence data.</param>
        /// <returns>No content if successful; otherwise, NotFound or BadRequest.</returns>
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

        /// <summary>
        /// Deletes a licence by ID.
        /// </summary>
        /// <param name="id">The ID of the licence to delete.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _licenceService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // -------------------- Licence Option Endpoints --------------------

        /// <summary>
        /// Retrieves all licence options.
        /// </summary>
        /// <returns>A list of all licence options.</returns>
        [HttpGet("options")]
        public async Task<ActionResult<IEnumerable<LicenceOption>>> GetAllOptions()
        {
            var options = await _licenceOptionService.GetAllAsync();
            return Ok(options);
        }

        /// <summary>
        /// Retrieves a licence option by ID.
        /// </summary>
        /// <param name="id">The ID of the option to retrieve.</param>
        /// <returns>The requested licence option if found; otherwise, NotFound.</returns>
        [HttpGet("options/{id}")]
        public async Task<ActionResult<LicenceOption>> GetOptionById(int id)
        {
            var option = await _licenceOptionService.GetByIdAsync(id);
            if (option == null)
                return NotFound();

            return Ok(option);
        }

        /// <summary>
        /// Retrieves all options associated with a specific licence.
        /// </summary>
        /// <param name="licenceId">The ID of the licence.</param>
        /// <returns>A list of options for the specified licence.</returns>
        [HttpGet("{licenceId}/options")]
        public async Task<ActionResult<IEnumerable<LicenceOption>>> GetOptionsByLicenceId(int licenceId)
        {
            var options = await _licenceOptionService.GetByLicenceIdAsync(licenceId);
            return Ok(options);
        }

        /// <summary>
        /// Creates a new licence option.
        /// </summary>
        /// <param name="option">The licence option to create.</param>
        /// <returns>The ID of the created option.</returns>
        [HttpPost("options")]
        public async Task<ActionResult<int>> CreateOption([FromBody] LicenceOption option)
        {
            var newId = await _licenceOptionService.CreateAsync(option);
            return CreatedAtAction(nameof(GetOptionById), new { id = newId }, newId);
        }

        /// <summary>
        /// Updates an existing licence option.
        /// </summary>
        /// <param name="id">The ID of the option to update.</param>
        /// <param name="option">The updated licence option data.</param>
        /// <returns>No content if successful; otherwise, NotFound or BadRequest.</returns>
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

        /// <summary>
        /// Deletes a licence option by ID.
        /// </summary>
        /// <param name="id">The ID of the option to delete.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
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
