using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        public string? EmployeeId { get; set; } 
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}