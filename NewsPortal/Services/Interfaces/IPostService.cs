using NewsPortal.Dtos.CategoryDto;
using NewsPortal.Dtos.PostDto;

namespace NewsPortal.Services.Interfaces
{
    public interface IPostService
    {
        Task CreateAsync(CreatePostDto createPostDto);
        Task EditAsync(EditPostDto editPostDto);
        Task DeleteAsync(int id);
    }
}