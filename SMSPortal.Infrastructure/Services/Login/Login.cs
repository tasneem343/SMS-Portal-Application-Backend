

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SMSPortal.Application.Services.GenerateJwtToken;
using SMSPortal.Application.Services.Login;
using SMSPortal.Domain.Enitites.User;


namespace SMSPortal.Infrastructure.Services.Login
{
    public class LoginService(UserManager<ApplicationUser> _userManager, IGenerateJwtToken _generateToken, ILogger<LoginService> logger) : ILoginService
    {
        public async Task<string> GetUserId(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            return user.Id;
        }

        public async Task<string?> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                logger.LogInformation("User {Email} logged in successfully.", email);
                  var token=  await _generateToken.GenerateJwtTokenAsync(user);
                return token;

            }
            logger.LogWarning("Invalid login attempt for user {Email}.", email);
            throw new Exception("Invalid login attempt.");
        }
    }
}
