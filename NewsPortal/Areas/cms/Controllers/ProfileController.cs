using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Helpers.Interface;
using NewsPortal.Models;
using NewsPortal.ViewModels.ProfileViewModel;

namespace NewsPortal.Areas.cms.Controllers
{
    [Area("cms")]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;

        public ProfileController(UserManager<ApplicationUser> userManager, INotyfService notyfService, IFileHelper fileHelper)
        {
            _userManager = userManager;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var vm = new ProfileVm
                {
                    Id = user!.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FacebookUrl = user.FacebookUrl,
                    TwitterUrl = user.TwitterUrl,
                    InstagramUrl = user.InstagramUrl,
                    LinkedInUrl = user.LinkedInUrl,
                    YouTubeUrl = user.YouTubeUrl,
                    WebsiteUrl = user.WebsiteUrl,
                    About = user.About,
                    ImageUrl = user.ImageUrl
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        public async Task<IActionResult> Edit()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var vm = new EditProfileVm
                {
                    Id = user!.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FacebookUrl = user.FacebookUrl,
                    TwitterUrl = user.TwitterUrl,
                    InstagramUrl = user.InstagramUrl,
                    LinkedInUrl = user.LinkedInUrl,
                    YouTubeUrl = user.YouTubeUrl,
                    WebsiteUrl = user.WebsiteUrl,
                    About = user.About,
                    ImageUrl = user.ImageUrl
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                {
                    var user = await _userManager.GetUserAsync(User);
                    user!.FirstName = vm.FirstName;
                    user.LastName = vm.LastName;
                    user.FacebookUrl = vm.FacebookUrl;
                    user.TwitterUrl = vm.TwitterUrl;
                    user.InstagramUrl = vm.InstagramUrl;
                    user.LinkedInUrl = vm.LinkedInUrl;
                    user.YouTubeUrl = vm.YouTubeUrl;
                    user.WebsiteUrl = vm.WebsiteUrl;
                    user.About = vm.About;

                    if (vm.Image != null)
                    {
                        var fileName = await _fileHelper.UploadFile(vm.Image, "user");
                        if (fileName != null)
                        {
                            _fileHelper.DeleteFile(user.ImageUrl!, "user");
                        }
                        user.ImageUrl = fileName;
                    }

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        _notyfService.Error("Something went wrong");
                        return View(vm);
                    }
                    _notyfService.Success("Profile updated successfully");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordVm());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var verifyPassword = await _userManager.CheckPasswordAsync(user!, vm.OldPassword!);
                if (!verifyPassword)
                {
                    _notyfService.Error("Old password is incorrect");
                    return View(vm);
                }

                var result = await _userManager.ChangePasswordAsync(user!, vm.OldPassword!, vm.NewPassword!);
                if (!result.Succeeded)
                {
                    _notyfService.Error("Something went wrong");
                    return View(vm);
                }
                _notyfService.Success("Password changed successfully");
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }
    }
}