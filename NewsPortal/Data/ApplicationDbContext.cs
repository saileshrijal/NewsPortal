using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Models;

namespace NewsPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser>? ApplicationUsers {get; set;}
        public DbSet<Category>? Categories {get; set;}
        public DbSet<Post>? Posts {get; set;}
        public DbSet<PostCategory>? PostCategories {get; set;}
    }
}