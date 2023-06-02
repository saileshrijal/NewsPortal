using NewsPortal.Dtos.CategoryDto;
using NewsPortal.Models;
using NewsPortal.Repositories.Interface;
using NewsPortal.Services.Interfaces;

namespace NewsPortal.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category()
            {
                Title = createCategoryDto.Title,
                Description = createCategoryDto.Description,
                Slug = createCategoryDto.Slug!.Trim().ToLower().Replace(" ", "-"),
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.CreateAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByAsync(x => x.Id == id);
            await _unitOfWork.DeleteAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditCategoryDto editCategoryDto)
        {
            var category = new Category()
            {
                Id = editCategoryDto.Id,
                Title = editCategoryDto.Title,
                Description = editCategoryDto.Description,
                Slug = editCategoryDto.Slug!.Trim().ToLower().Replace(" ", "-"),
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
        }
    }
}