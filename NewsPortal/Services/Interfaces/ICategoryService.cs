using NewsPortal.Dtos.CategoryDto;

namespace NewsPortal.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(CreateCategoryDto createCategoryDto);
        Task EditAsync(EditCategoryDto editCategoryDto);
        Task DeleteAsync(int id);
    }
}