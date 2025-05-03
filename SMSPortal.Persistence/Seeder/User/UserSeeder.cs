using Microsoft.AspNetCore.Identity;
using SMSPortal.Application.Interfaces.Seeder.User;
using SMSPortal.Domain.Enitites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Persistence.Seeder.User
{
    public class UserSeeder: IUserSeeder
    {
    
             private readonly UserManager<ApplicationUser> _userManager;

        public UserSeeder(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        
        public async Task SeedUsers()
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@smsportal.com",
                EmailConfirmed = true
            };
            if (await _userManager.FindByNameAsync("admin") == null)
            {
                var result = await _userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            var senderUser = new ApplicationUser
            {
                UserName = "sender1",
                Email = "sender1@smsportal.com",
                EmailConfirmed = true
            };
            if (await _userManager.FindByNameAsync("sender1") == null)
            {
                var result = await _userManager.CreateAsync(senderUser, "Sender@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(senderUser, "Sender");
                }
                else
                {
                    throw new Exception($"Failed to create sender user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            var viewerUser = new ApplicationUser
            {
                UserName = "viewer1",
                Email = "viewer1@smsportal.com",
                EmailConfirmed = true
            };
            if (await _userManager.FindByNameAsync("viewer1") == null)
            {
                var result = await _userManager.CreateAsync(viewerUser, "Viewer@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(viewerUser, "Viewer");
                }
                else
                {
                    throw new Exception($"Failed to create viewer user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
    
    
}
