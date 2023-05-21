using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Seeder.Interface;

namespace NewsPortal.Controllers
{

    [AllowAnonymous]
    public class SeedController : Controller
    {
        private readonly IUserSeeder _userSeeder;
        public SeedController(IUserSeeder userSeeder)
        {
            _userSeeder = userSeeder;
        }

        public async Task<IActionResult> SeedAdminUser()
        {
            try
            {
                await _userSeeder.SeedAdminUserAsync();
                return Ok("Admin user created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}