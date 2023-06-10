using NewsPortal.Models;

namespace NewsPortal.Repositories.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetAllWithAuthorAndCategoryAsync();
        Task<Post> GetWithAuthorAndCategoryByIdAsync(int id);
    }
}