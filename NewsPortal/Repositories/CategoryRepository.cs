using NewsPortal.Data;
using NewsPortal.Models;
using NewsPortal.Repositories.Interface;

namespace NewsPortal.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public bool VerifySlug(string slug, int id)
        {
            return _context.Categories!.Any(x => x.Slug == slug && x.Id != id);
        }
    }
}