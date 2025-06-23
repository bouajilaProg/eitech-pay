using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
// using Back.Modules.AdminModule.Services; 


namespace Back.modules.AdminModule
=======
using Back.Modules.AdminModule.Services; 
using Back.Modules.AdminModule.Dtos;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;




namespace Back.Modules.AdminModule
>>>>>>> upstream/main
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

<<<<<<< HEAD
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
=======


    [HttpPost("decode-token")]
    public ActionResult<object> DecodeToken([FromBody] string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();

            // Parse token without signature validation
            var jwtToken = handler.ReadJwtToken(token);

            var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => (object)c.Value);

            return Ok(claims);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                error = "Invalid JWT format.",
                message = ex.Message
            });
        }
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
        public ActionResult CheckToken([FromBody]string token)
        {
          var Result = _adminService.CheckToken(token);
          return Ok(new
          {
              AdminId = Result.AdminId,
              AdminName = Result.AdminName
          });

        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto dto)
        {
            _adminService.ChangePassword(dto.OldPassword, dto.NewPassword);
            return NoContent();
        }

        [HttpPost("change-payment-details")]
        public IActionResult ChangePaymentDetails([FromBody] ChangePaymentDetailsDto dto)
        {
            _adminService.ChangePaymentDetails(dto.ApiKey, dto.KonnectId);
            return NoContent();
        }
    }
>>>>>>> upstream/main
}
