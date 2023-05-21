using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Seeder.Interface
{
    public interface IUserSeeder
    {
        Task SeedAdminUserAsync();
    }
}