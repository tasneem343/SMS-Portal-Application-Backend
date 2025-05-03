using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMSPortal.Application.Auth.Login.Commands;
using SMSPortal.Application.Auth.logout.Commands;
using System.Security.Claims;




namespace SMS_Portal_Application.Controllers.Auth
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("logout/{userId}")]
        public async Task<IActionResult> Logout(string userId)
        {
            await mediator.Send(new LogoutCommand { UserId=userId});
            return Ok(new { message = "Logout successful" });
        }

       

    }
}
