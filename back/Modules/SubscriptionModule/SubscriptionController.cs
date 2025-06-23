using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.SubscriptionRelated;
using Back.Modules.SubscriptionModule.Services;

namespace Back.Modules.SubscriptionModule
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ITiersService _tiersService;

        public SubscriptionController(
            ISubscriptionService subscriptionService,
            ITiersService tiersService)
        {
            _subscriptionService = subscriptionService;
            _tiersService = tiersService;
        }

        // --- Subscription Endpoints ---

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetAll()
        {
            var subscriptions = await _subscriptionService.GetAllAsync();
            return Ok(subscriptions);
        }

        [HttpGet("{subId}")]
        public async Task<ActionResult<Subscription?>> GetById(string subId)
        {
            var subscription = await _subscriptionService.GetByIdAsync(subId);
            if (subscription == null)
                return NotFound();
            return Ok(subscription);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(Subscription subscription)
        {
            var newId = await _subscriptionService.CreateAsync(subscription);
            return CreatedAtAction(nameof(GetById), new { subId = newId }, newId);
        }

        [HttpPut("{subId}")]
        public async Task<ActionResult> Update(string subId, Subscription subscription)
        {
            if (subId != subscription.SubscriptionId)
                return BadRequest("ID mismatch.");

            var updated = await _subscriptionService.UpdateAsync(subscription);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{subId}")]
        public async Task<ActionResult> Delete(string subId)
        {
            var deleted = await _subscriptionService.DeleteAsync(subId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // --- Tier Endpoints ---

        /// <summary>
        /// Get all subscription tiers (not archived).
        /// </summary>
        [HttpGet("tiers")]
        public async Task<ActionResult<IEnumerable<SubscriptionTier>>> GetAllTiers()
        {
            var tiers = await _tiersService.GetAllAsync();
            return Ok(tiers);
        }

        /// <summary>
        /// Get a subscription tier by its ID.
        /// </summary>
        [HttpGet("tiers/{tierId}")]
        public async Task<ActionResult<SubscriptionTier?>> GetTierById(string tierId)
        {
            var tier = await _tiersService.GetByIdAsync(tierId);
            if (tier == null)
                return NotFound();
            return Ok(tier);
        }

        /// <summary>
        /// Get all tiers for a given subscription ID.
        /// </summary>
        [HttpGet("{subId}/tiers")]
        public async Task<ActionResult<IEnumerable<SubscriptionTier>>> GetTiersBySubscriptionId(string subId)
        {
            var tiers = await _tiersService.GetSubscriptionByID(subId);
            return Ok(tiers);
        }

        /// <summary>
        /// Create a new subscription tier.
        /// </summary>
        [HttpPost("tiers")]
        public async Task<ActionResult<string>> CreateTier(SubscriptionTier tier)
        {
            var newId = await _tiersService.CreateAsync(tier);
            return CreatedAtAction(nameof(GetTierById), new { tierId = newId }, newId);
        }

        /// <summary>
        /// Update a subscription tier by ID.
        /// </summary>
        [HttpPut("tiers/{tierId}")]
        public async Task<ActionResult> UpdateTier(string tierId, SubscriptionTier tier)
        {
            if (tierId != tier.TierId)
                return BadRequest("ID mismatch.");

            var updated = await _tiersService.UpdateAsync(tier);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Soft delete a subscription tier by ID.
        /// </summary>
        [HttpDelete("tiers/{tierId}")]
        public async Task<ActionResult> DeleteTier(string tierId)
        {
            var deleted = await _tiersService.DeleteAsync(tierId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
