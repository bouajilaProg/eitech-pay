using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Back.Modules.LicenceModule.Services;
using Back.Modules.LicenceModule.Dtos;
using System.Text.Json;
using System.Text;

namespace Back.Modules.LicenceModule.Controllers
{
    [ApiController]
    [Route("api/v0/licence")]
    public class LicencePublicController : ControllerBase
    {
        private readonly IlicencePublicService _licencePublicService;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public LicencePublicController(
            IlicencePublicService licencePublicService,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _licencePublicService = licencePublicService;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        /// <summary>
        /// Get the details of a licence by its ID.
        /// </summary>
        /// <param name="licenceId">The unique ID of the licence</param>
        /// <returns>Licence details if found and not archived; otherwise, 404</returns>
        [HttpGet("{licenceId}/details")]
        public async Task<ActionResult<LicenceDetailsDto?>> GetLicenceDetails(string licenceId)
        {
            var details = await _licencePublicService.GetLicenceDetailsAsync(licenceId);

            if (details == null)
                return NotFound(new { message = "Licence not found or archived." });

            return Ok(details);
        }

        /// <summary>
        /// Activate a licence for a device.
        /// </summary>
        /// <param name="request">The activation request containing email, licence key, and device fingerprint</param>
        /// <returns>Activation result indicating success or failure with appropriate message</returns>
        [HttpPost("activate")]
        public async Task<ActionResult<ActivationResultDto>> ActivateLicence([FromBody] ActivateLicenceRequestDto request)
        {
            // Get client IP address
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            var result = await _licencePublicService.ActivateLicenceAsync(
                request.Email, 
                request.LicenceKey, 
                request.DeviceFingerprint, 
                ipAddress);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("webhook")]
        public async Task<IActionResult> HandleWebhook([FromQuery] string payment_ref)
        {
            if (string.IsNullOrEmpty(payment_ref))
                return BadRequest("Missing payment reference.");

            var apiKey = _configuration["Konnect:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                return BadRequest("Konnect API Key not configured.");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var konnectApiUrl = $"{_configuration["Konnect:ApiUrl"]}/payment/{payment_ref}";
            var response = await _httpClient.GetAsync(konnectApiUrl);

            if (!response.IsSuccessStatusCode)
                return NotFound("Could not fetch payment details from Konnect.");

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonDocument.Parse(responseContent);
            var payment = responseJson.RootElement.GetProperty("payment");

            string status = payment.GetProperty("status").GetString()!;
            if (status.ToLower() != "completed")
                return Ok("Payment is not completed yet.");

            string orderId = payment.GetProperty("orderId").GetString()!;
            decimal amount = payment.GetProperty("amount").GetDecimal();
            string email = payment.GetProperty("paymentDetails").GetProperty("email").GetString()!;
            string name = payment.GetProperty("paymentDetails").GetProperty("name").GetString()!;
            string phone = payment.GetProperty("paymentDetails").GetProperty("phoneNumber").GetString()!;

            // 🚨 You can customize this to match your actual CreateLicenceOrderAsync parameters
            var orderDto = new CreateLicenceOrderDto
            {
                Email = email,
                FullName = name,
                PhoneNumber = phone,
                ReferenceId = orderId,
                AmountPaid = amount,
                PaymentRef = payment_ref
                // Add more fields if needed
            };

            await _licencePublicService.CreateLicenceOrderAsync(orderDto);

            return Ok("Licence order created successfully.");
        }


        [HttpPost("{licenceId}/payment/initiate")]
        public async Task<ActionResult> InitiatePayment(string licenceId, [FromBody] LicencePaymentRequestDto paymentRequest)
        {
            // Get settings from configuration
            var konnectId = _configuration["Konnect:ReceiverWalletId"];
            var apiKey = _configuration["Konnect:ApiKey"];
            
            if (string.IsNullOrEmpty(konnectId) || string.IsNullOrEmpty(apiKey))
            {
                return BadRequest("Payment configuration not found. Please contact administrator.");
            }

            Console.WriteLine($"sssssssssssssasasasa4545454");

            var paymentData = new
            {
                receiverWalletId = konnectId,
                token = "TND",
                amount = paymentRequest.Amount,
                type = "immediate",
                description = paymentRequest.Description,
                acceptedPaymentMethods = paymentRequest.AcceptedPaymentMethods,
                lifespan = 10,
                webhook = "https://webhook.site/5a59eb60-8cc2-4ef2-bd4c-129f49059cef",
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
            Console.WriteLine($"Konnect API Response: {response.StatusCode}");

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

        // [HttpGet("payment/{paymentRef}")]
        // public async Task<ActionResult> GetPaymentStatus(string paymentRef)
        // {
        //     var apiKey = _configuration["Konnect:ApiKey"];
            
        //     if (string.IsNullOrEmpty(apiKey))
        //     {
        //         return BadRequest("Payment configuration not found.");
        //     }

        //     _httpClient.DefaultRequestHeaders.Clear();
        //     _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

        //     var konnectApiUrl = $"{_configuration["Konnect:ApiUrl"]}/payment/{paymentRef}";
        //     var response = await _httpClient.GetAsync(konnectApiUrl);

        //     if (!response.IsSuccessStatusCode)
        //         return NotFound();

        //     var responseContent = await response.Content.ReadAsStringAsync();
        //     return Ok(responseContent);
        // }
    }
}
