using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.ViewModels.ProfileViewModel
{
    public class ChangePasswordVm
    {
        [Required(ErrorMessage = "Old Password is required")]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        //regex
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character.")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password does not match")]
        public string? ConfirmPassword { get; set; }
    }
}