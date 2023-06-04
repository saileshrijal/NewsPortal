using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NewsPortal.ViewModels.UserViewModel
{
    public class CreateUserVm
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Remote(action: "VerifyUserName", controller: "User")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Remote(action: "VerifyEmail", controller: "User")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "User Role is required")]
        public string? UserRole { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}