using NewsPortal.Models;

namespace NewsPortal.Repositories.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool VerifySlug(string slug, int id);
    }
}