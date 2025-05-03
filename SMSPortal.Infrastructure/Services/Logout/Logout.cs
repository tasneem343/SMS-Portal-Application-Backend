



using Microsoft.AspNetCore.Identity;
using SMSPortal.Application.Services.Logout;
using SMSPortal.Domain.Enitites.User;

namespace SMSPortal.Infrastructure.Services.Logout
{
    public class LogoutService(SignInManager<ApplicationUser> _signInManager) : ILogoutService
    {
        public async Task Logout(string userId)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            await _signInManager.SignOutAsync();
        }
    }
}
