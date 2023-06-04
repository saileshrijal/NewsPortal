using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.ViewModels.UserViewModel
{
    public class UpdateRoleVm
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string? UserRole { get; set; }
    }
}