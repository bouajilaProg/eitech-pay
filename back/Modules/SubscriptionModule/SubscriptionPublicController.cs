using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back.Modules.SubscriptionModule.Services;
using Back.Modules.SubscriptionModule.Dtos;
using System.Text.Json;
using System.Text;

namespace Back.Controllers.Public
{
    [ApiController]
    [Route("api/v0/subscription")]
    public class SubscriptionPublicController : ControllerBase
    {
        private readonly ISubscriptionPublicService _subscriptionPublicService;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SubscriptionPublicController(
            ISubscriptionPublicService subscriptionPublicService,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _subscriptionPublicService = subscriptionPublicService;
            _httpClient = httpClient;
            _configuration = configuration;
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

        [HttpPost("{subscriptionId}/payment/initiate")]
        public async Task<ActionResult> InitiatePayment(string subscriptionId, [FromBody] PaymentRequestDto paymentRequest)
        {
            // Get settings from configuration
            var konnectId = _configuration["Konnect:ReceiverWalletId"];
            var apiKey = _configuration["Konnect:ApiKey"];
            
            if (string.IsNullOrEmpty(konnectId) || string.IsNullOrEmpty(apiKey))
            {
                return BadRequest("Payment configuration not found. Please contact administrator.");
            }

            var paymentData = new
            {
                receiverWalletId = konnectId,
                token = "TND",
                amount = paymentRequest.Amount,
                type = "immediate",
                description = paymentRequest.Description,
                acceptedPaymentMethods = paymentRequest.AcceptedPaymentMethods,
                lifespan = 10,
                checkoutForm = true,
                addPaymentFeesToAmount = true,
                firstName = paymentRequest.FirstName,
                lastName = paymentRequest.LastName,
                phoneNumber = paymentRequest.PhoneNumber,
                email = paymentRequest.Email,
                orderId = paymentRequest.ReferenceId,
                theme = "dark"
            };

            var content = new StringContent(JsonSerializer.Serialize(paymentData), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var konnectApiUrl = $"{_configuration["Konnect:ApiUrl"]}/init-payment";
            var response = await _httpClient.PostAsync(konnectApiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return BadRequest($"Failed to initiate payment. Status: {response.StatusCode}, Error: {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseDict = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);

            if (responseDict == null || !responseDict.ContainsKey("payUrl") || !responseDict.ContainsKey("paymentRef"))
                return BadRequest("Invalid payment response.");

            return Ok(new
            {
                payUrl = responseDict["payUrl"],
                paymentRef = responseDict["paymentRef"]
            });
        }

        [HttpGet("payment/{paymentRef}")]
        public async Task<ActionResult> GetPaymentStatus(string paymentRef)
        {
            var apiKey = _configuration["Konnect:ApiKey"];
            
            if (string.IsNullOrEmpty(apiKey))
            {
                return BadRequest("Payment configuration not found.");
            }

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var konnectApiUrl = $"{_configuration["Konnect:ApiUrl"]}/payment/{paymentRef}";
            var response = await _httpClient.GetAsync(konnectApiUrl);

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var responseContent = await response.Content.ReadAsStringAsync();
            return Ok(responseContent);
        }
    }
}
