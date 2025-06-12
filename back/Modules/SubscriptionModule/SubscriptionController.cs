using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.General; // Assuming Product is here
using Back.Models.SubscriptionRelated; // Assuming SubscriptionTier is here
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
        // No :string constraint needed by default, it handles strings
        [HttpGet("{subId}")]
        public async Task<ActionResult<Product?>> GetById(string subId)
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
        public async Task<ActionResult<string>> Create(Product product)
        {
            var newId = await _subscriptionService.CreateAsync(product);
            // Ensure newId is a string and matches the type of subId
            return CreatedAtAction(nameof(GetById), new { subId = newId }, newId);
        }

        /// <summary>
        /// Update an existing subscription product.
        /// </summary>
        [HttpPut("{subId}")]
        public async Task<ActionResult> Update(string subId, Product product)
        {
            // Assuming Product.Id is now a string
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
        // Removed :int constraint
        [HttpDelete("{subId}")]
        public async Task<ActionResult> Delete(string subId)
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
        // Removed :int constraint
        [HttpGet("{tierId}")]
        public async Task<ActionResult<SubscriptionTier?>> GetById(string tierId)
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
        public async Task<ActionResult<string>> Create(SubscriptionTier tier)
        {
            var newTierId = await _tiersService.CreateAsync(tier);
            // Ensure newTierId is a string and matches the type of tierId
            return CreatedAtAction(nameof(GetById), new { tierId = newTierId }, newTierId);
        }

        /// <summary>
        /// Update a subscription tier.
        /// </summary>
        // Removed :int constraint
        [HttpPut("{tierId}")]
        public async Task<ActionResult> Update(string tierId, SubscriptionTier tier)
        {
            // Assuming SubscriptionTier.TierId is now a string
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
        // Removed :int constraint
        [HttpDelete("{tierId}")]
        public async Task<ActionResult> Delete(string tierId)
        {
            var deleted = await _tiersService.DeleteAsync(tierId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
