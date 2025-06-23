using Microsoft.AspNetCore.Mvc;
// using Back.Modules.AdminModule.Services; 


namespace Back.modules.AdminModule
{
    // [ApiController]
    // [Route("api/admin")]
    // public class AdminController : ControllerBase
    // {
    //     private readonly IAdminService _adminService;

    //     public AdminController(IAdminService adminService)
    //     {
    //         _adminService = adminService;
    //     }

    //     [HttpPost("login")]
    //     public ActionResult<string> Login(string email, string password)
    //     {
    //         var token = _adminService.Login(email, password);
    //         return Ok(token);
    //     }

    //     [HttpPost("check-token")]
    //     public ActionResult<bool> CheckToken(string token)
    //     {
    //         var isValid = _adminService.CheckToken(token);
    //         return Ok(isValid);
    //     }

    //     [HttpPost("change-password")]
    //     public IActionResult ChangePassword(string oldpasswd, string newpasswd)
    //     {
    //         _adminService.ChangePassword(oldpasswd, newpasswd);
    //         return NoContent();
    //     }

    //     [HttpPost("change-payment-details")]
    //     public IActionResult ChangePaymentDetails(string apiKey, string konnectId)
    //     {
    //         _adminService.ChangePaymentDetails(apiKey, konnectId);
    //         return NoContent();
    //     }
    // }
}
