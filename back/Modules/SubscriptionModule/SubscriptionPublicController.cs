using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back.Modules.SubscriptionModule.Services;
using Back.Modules.SubscriptionModule.Dtos;

namespace Back.Controllers.Public
{
    [ApiController]
    [Route("api/v0/subscription")]
    public class SubscriptionPublicController : ControllerBase
    {
        private readonly ISubscriptionPublicService _subscriptionPublicService;

        public SubscriptionPublicController(ISubscriptionPublicService subscriptionPublicService)
        {
            _subscriptionPublicService = subscriptionPublicService;
        }

        // GET: /api/v0/subscription/{subscriptionId}/details
        [HttpGet("{subscriptionId}/details")]
        public async Task<ActionResult<SubscriptionDetailsDto>> GetSubscriptionDetails(string subscriptionId)
        {
            var details = await _subscriptionPublicService.GetSubscriptionDetailsAsync(subscriptionId);

            if (details == null)
            {
                return NotFound(new { message = "Subscription not found or archived." });
            }

            return Ok(details);
        }

        // ✅ NEW: Check Subscription
        // Example: /api/v0/subscription/check?tierId=TIER-001&email=user@example.com&tel=123456
        [HttpGet("check/{tierId}")]
       public async Task<ActionResult<CheckSubscriptionDto>> CheckSubscription(
            [FromRoute] string tierId,
            [FromQuery] string email,
            [FromQuery] string? tel)
        {
            var result = await _subscriptionPublicService.CheckSubscriptionAsync(tierId, email, tel);

            if (result == null)
            {
                return NotFound(new { message = "No active subscription found." });
            }

            return Ok(result);
        }
    }
}
