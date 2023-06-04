using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.ViewModels.UserViewModel
{
    public class UserVm
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? ImageUrl { get; set; }
        public bool Status { get; set; }
        public string? UserRole { get; set; }
        public int NumberOfPosts { get; set; }
    }
}