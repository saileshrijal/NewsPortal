using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? YouTubeUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? About { get; set; }
    }
}