using System.ComponentModel.DataAnnotations;

namespace NewsPortal.ViewModels.UserViewModel
{
    public class ResetPasswordVm
    {
        public string? Id { get; set; }

        public string? FullName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //regex
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character.")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Password and confirm password does not match")]
        public string? ConfirmPassword { get; set; }
    }
}