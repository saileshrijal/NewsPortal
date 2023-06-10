using NewsPortal.Data;
using NewsPortal.Models;
using NewsPortal.Repositories.Interface;

namespace NewsPortal.Repositories
{
    public class PostCategoryRepository : Repository<PostCategory>, IPostCategoryRepository
    {
        public PostCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}