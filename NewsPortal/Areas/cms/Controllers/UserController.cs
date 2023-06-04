using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Constants;
using NewsPortal.Helpers.Interface;
using NewsPortal.Models;
using NewsPortal.ViewModels.UserViewModel;

namespace NewsPortal.Areas.cms.Controllers
{
    [Area("cms")]
    public class UserController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileHelper _fileHelper;
        private readonly INotyfService _notyfService;


        public UserController(UserManager<ApplicationUser> userManager, INotyfService notyfService, IFileHelper fileHelper)
        {
            _userManager = userManager;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var vm = users.Select(x => new UserVm()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                FullName = x.FullName,
                ImageUrl = x.ImageUrl,
                Status = x.Status,
                UserRole = _userManager.GetRolesAsync(x).Result[0]
            }).ToList();
            return View(vm);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View(new CreateUserVm());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var verifyUserName = await _userManager.FindByNameAsync(vm.UserName!);
                if (verifyUserName != null)
                {
                    _notyfService.Error("Username already exists");
                    return View(vm);
                }

                var verifyEmail = await _userManager.FindByEmailAsync(vm.Email!);
                if (verifyEmail != null)
                {
                    _notyfService.Error("Email already exists");
                    return View(vm);
                }

                var user = new ApplicationUser()
                {
                    UserName = vm.UserName,
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Status = true,
                    CreatedDate = DateTime.UtcNow,
                };

                var result = await _userManager.CreateAsync(user, vm.Password!);
                if (!result.Succeeded)
                {
                    _notyfService.Error("User added failed");
                    return View(vm);
                }
                await _userManager.AddToRoleAsync(user, vm.UserRole!);
                _notyfService.Success("User added successfully");
                return RedirectToAction(nameof(Add));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                var vm = new EditUserVm()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FacebookUrl = user.FacebookUrl,
                    TwitterUrl = user.TwitterUrl,
                    InstagramUrl = user.InstagramUrl,
                    LinkedInUrl = user.LinkedInUrl,
                    YouTubeUrl = user.YouTubeUrl,
                    WebsiteUrl = user.WebsiteUrl,
                    About = user.About,
                    ImageUrl = user.ImageUrl,
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
        public async Task<IActionResult> Edit(EditUserVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var user = await _userManager.FindByIdAsync(vm.Id!);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }

                user.FirstName = vm.FirstName;
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
                    var imageUrl = await _fileHelper.UploadFile(vm.Image, "user");
                    if (imageUrl != null)
                    {
                        if (user.ImageUrl != null)
                        {
                            _fileHelper.DeleteFile(user.ImageUrl!, "user");
                        }
                        user.ImageUrl = imageUrl;
                    }
                }

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    _notyfService.Error("User update failed");
                    return View(vm);
                }
                _notyfService.Success("User updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                user.Status = !user.Status;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    _notyfService.Error("User status update failed");
                    return RedirectToAction(nameof(Index));
                }
                _notyfService.Success("User status updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> ResetPassword(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                var vm = new ResetPasswordVm()
                {
                    Id = user.Id,
                    FullName = user.FullName,
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
        public async Task<IActionResult> ResetPassword(ResetPasswordVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var user = await _userManager.FindByIdAsync(vm.Id!);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, vm.NewPassword!);
                if (!result.Succeeded)
                {
                    _notyfService.Error("Password reset failed");
                    return View(vm);
                }
                _notyfService.Success("Password reset successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                var vm = new UpdateRoleVm()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
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
        public async Task<IActionResult> UpdateRole(UpdateRoleVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var user = await _userManager.FindByIdAsync(vm.Id!);
                if (user == null)
                {
                    _notyfService.Error("User not found");
                    return RedirectToAction(nameof(Index));
                }
                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                var result = await _userManager.AddToRoleAsync(user, vm.UserRole!);
                if (!result.Succeeded)
                {
                    _notyfService.Error("Role update failed");
                    return View(vm);
                }
                _notyfService.Success("Role updated successfully");
                tx.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }



        public async Task<IActionResult> VerifyEmail(string email)
        {
            var verifyEmail = await _userManager.FindByEmailAsync(email);
            if (verifyEmail != null)
            {
                return Json($"Email {email} already exists");
            }
            return Json(true);
        }

        public async Task<IActionResult> VerifyUserName(string userName)
        {
            var verifyUserName = await _userManager.FindByNameAsync(userName);
            if (verifyUserName != null)
            {
                return Json($"Username {userName} already exists");
            }
            return Json(true);
        }
    }
}