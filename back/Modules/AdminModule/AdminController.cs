using Microsoft.AspNetCore.Mvc;
using Back.Modules.AdminModule.Services; 
using Back.Modules.AdminModule.Dtos;


namespace Back.Modules.AdminModule
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginRequestDto loginDto)
        {
            var username = loginDto.Username;
            var password = loginDto.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Username and password must not be empty.");
            }
            var token = _adminService.Login(username, password);
            return Ok(token);
        }

        [HttpPost("check-token")]
        public ActionResult<bool> CheckToken(string token)
        {
            var isValid = _adminService.CheckToken(token);
            return Ok(isValid);
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword(string oldpasswd, string newpasswd)
        {
            _adminService.ChangePassword(oldpasswd, newpasswd);
            return NoContent();
        }

        [HttpPost("change-payment-details")]
        public IActionResult ChangePaymentDetails(string apiKey, string konnectId)
        {
            _adminService.ChangePaymentDetails(apiKey, konnectId);
            return NoContent();
        }
    }
}
