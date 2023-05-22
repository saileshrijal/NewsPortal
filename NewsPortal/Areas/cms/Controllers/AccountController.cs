using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models;
using NewsPortal.ViewModels;

namespace NewsPortal.Areas.cms.Controllers
{
    [Area("cms")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotyfService _notyfService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notyfService = notyfService;
        }

        [HttpGet("cms/login")]
        public IActionResult Login(string? ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard", new { area = "cms" });
            return View(new LoginVm());
        }

        [HttpPost("cms/login")]
        public async Task<IActionResult> Login(LoginVm vm, string? ReturnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.FindByNameAsync(vm.UserName!);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "User not found");
                return View(vm);
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, vm.Password!);
            if (!checkPassword)
            {
                ModelState.AddModelError("Password", "Invalid password");
                return View(vm);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, vm.Password!, vm.RememberMe, false);
            if (!signInResult.Succeeded)
            {
                _notyfService.Error("Invalid login attempt");
                return View(vm);
            }

            _notyfService.Success("Login successfull");
            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _notyfService.Success("Logout successfull");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}