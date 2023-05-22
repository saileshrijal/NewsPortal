using System.ComponentModel.DataAnnotations;

namespace NewsPortal.ViewModels
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}