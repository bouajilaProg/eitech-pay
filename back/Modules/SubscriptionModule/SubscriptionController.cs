using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.General;
using Back.Models.SubscriptionRelated;
using Back.Modules.SubscriptionModule.Services;

namespace Back.Module.SubscriptionModule
{
    [ApiController]
    [Route("subscription")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        /// <summary>
        /// Get all subscription products.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _subscriptionService.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get a subscription product by ID.
        /// </summary>
        [HttpGet("{subId}")]
        public async Task<ActionResult<Product?>> GetById(int subId)
        {
            var product = await _subscriptionService.GetByIdAsync(subId);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Create a new subscription product.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<int>> Create(Product product)
        {
            var newId = await _subscriptionService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { subId = newId }, newId);
        }

        /// <summary>
        /// Update an existing subscription product.
        /// </summary>
        [HttpPut("{subId}")]
        public async Task<ActionResult> Update(int subId, Product product)
        {
            if (subId != product.Id)
                return BadRequest("ID mismatch.");

            var updated = await _subscriptionService.UpdateAsync(product);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Soft delete a subscription product.
        /// </summary>
        [HttpDelete("{subId:int}")]
        public async Task<ActionResult> Delete(int subId)
        {
            var deleted = await _subscriptionService.DeleteAsync(subId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }

    [ApiController]
    [Route("subscription/tiers")]
    public class SubscriptionTierController : ControllerBase
    {
        private readonly ITiersService _tiersService;

        public SubscriptionTierController(ITiersService tiersService)
        {
            _tiersService = tiersService;
        }

        /// <summary>
        /// Get all subscription tiers.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionTier>>> GetAll()
        {
            var tiers = await _tiersService.GetAllAsync();
            return Ok(tiers);
        }

        /// <summary>
        /// Get a subscription tier by ID.
        /// </summary>
        [HttpGet("{tierId:int}")]
        public async Task<ActionResult<SubscriptionTier?>> GetById(int tierId)
        {
            var tier = await _tiersService.GetByIdAsync(tierId);
            if (tier == null)
                return NotFound();
            return Ok(tier);
        }

        /// <summary>
        /// Create a new subscription tier.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<int>> Create(SubscriptionTier tier)
        {
            var newTierId = await _tiersService.CreateAsync(tier);
            return CreatedAtAction(nameof(GetById), new { tierId = newTierId }, newTierId);
        }

        /// <summary>
        /// Update a subscription tier.
        /// </summary>
        [HttpPut("{tierId:int}")]
        public async Task<ActionResult> Update(int tierId, SubscriptionTier tier)
        {
            if (tierId != tier.TierId)
                return BadRequest("ID mismatch.");

            var updated = await _tiersService.UpdateAsync(tier);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Delete a subscription tier.
        /// </summary>
        [HttpDelete("{tierId:int}")]
        public async Task<ActionResult> Delete(int tierId)
        {
            var deleted = await _tiersService.DeleteAsync(tierId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
