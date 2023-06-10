using Microsoft.EntityFrameworkCore;
using NewsPortal.Data;
using NewsPortal.Models;
using NewsPortal.Repositories.Interface;

namespace NewsPortal.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Post>> GetAllWithAuthorAndCategoryAsync()
        {
            return await _context.Posts!.Include(x => x.ApplicationUser).Include(x => x.PostCategories)!.ThenInclude(x => x.Category).ToListAsync();
        }

        public async Task<Post> GetWithAuthorAndCategoryByIdAsync(int id)
        {
            return await _context.Posts!.Include(x => x.ApplicationUser).Include(x => x.PostCategories)!.ThenInclude(x => x.Category).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Post not found");
        }
    }
}