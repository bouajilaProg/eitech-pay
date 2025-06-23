using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.SubscriptionRelated;
using Back.Modules.SubscriptionModule.Services;

namespace Back.Modules.SubscriptionModule
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
        /// Get all subscriptions.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetAll()
        {
            var subscriptions = await _subscriptionService.GetAllAsync();
            return Ok(subscriptions);
        }

        /// <summary>
        /// Get a subscription by ID.
        /// </summary>
        [HttpGet("{subId}")]
        public async Task<ActionResult<Subscription?>> GetById(string subId)
        {
            var subscription = await _subscriptionService.GetByIdAsync(subId);
            if (subscription == null)
                return NotFound();
            return Ok(subscription);
        }

        /// <summary>
        /// Create a new subscription.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<string>> Create(Subscription subscription)
        {
            var newId = await _subscriptionService.CreateAsync(subscription);
            return CreatedAtAction(nameof(GetById), new { subId = newId }, newId);
        }

        /// <summary>
        /// Update an existing subscription.
        /// </summary>
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

        /// <summary>
        /// Soft delete a subscription.
        /// </summary>
        [HttpDelete("{subId}")]
        public async Task<ActionResult> Delete(string subId)
        {
            var deleted = await _subscriptionService.DeleteAsync(subId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
