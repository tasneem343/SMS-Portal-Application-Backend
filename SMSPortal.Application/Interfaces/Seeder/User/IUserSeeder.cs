using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Application.Interfaces.Seeder.User
{
    public interface IUserSeeder
    {
        Task SeedUsers();
    }
}
