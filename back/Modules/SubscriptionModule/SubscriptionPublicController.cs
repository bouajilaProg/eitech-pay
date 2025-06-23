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

        // GET: /api/v0/subscription/{subscriptionId}
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
    }
}
