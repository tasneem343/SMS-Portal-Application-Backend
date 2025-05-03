using Microsoft.AspNetCore.Identity;
using SMSPortal.Application.Interfaces.Seeder.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Persistence.Seeder.Role
{
    public class RoleSeeder(
       RoleManager<IdentityRole> roleManager) : IRoleSeeder
    {
        public async Task SeedRoles()
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Viewer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Viewer"));
            }
            if (!await roleManager.RoleExistsAsync("Sender"))
            {
                await roleManager.CreateAsync(new IdentityRole("Sender"));
            }
        }
    }
}
