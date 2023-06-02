using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Dtos.CategoryDto;
using NewsPortal.Repositories.Interface;
using NewsPortal.Services.Interfaces;
using NewsPortal.ViewModels.CategoryViewModel;

namespace NewsPortal.Controllers
{
    [Area("cms")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly ICategoryService _categoryService;
        private readonly INotyfService _notyfService;

        public CategoryController(ICategoryRepository categoryRepository, ICategoryService categoryService, INotyfService notyfService)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var vm = categories.Select(x => new CategoryVm()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Slug = x.Slug,
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new CreateCategoryVm());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var dto = new CreateCategoryDto()
                {
                    Title = vm.Title,
                    Slug = vm.Slug,
                    Description = vm.Description
                };
                await _categoryService.CreateAsync(dto);
                _notyfService.Success("Category added successfully");
                return RedirectToAction(nameof(Add));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByAsync(x => x.Id == id);
                var vm = new EditCategoryVm()
                {
                    Id = category.Id,
                    Title = category.Title,
                    Description = category.Description,
                    Slug = category.Slug
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var dto = new EditCategoryDto()
                {
                    Id = vm.Id,
                    Title = vm.Title,
                    Description = vm.Description,
                    Slug = vm.Slug
                };
                await _categoryService.EditAsync(dto);
                _notyfService.Success("Category updated successfully");
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByAsync(x => x.Id == id);
                var vm = new CategoryDetailsVm()
                {
                    Id = category.Id,
                    Title = category.Title,
                    Description = category.Description,
                    Slug = category.Slug,
                    CreatedDate = category.CreatedDate,
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                _notyfService.Success("Category deleted successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        public IActionResult IsSlugUnique(string slug, int id)
        {
            var slugResult = slug.Trim().ToLower().Replace(" ", "-");
            var result = _categoryRepository.VerifySlug(slugResult, id);
            if (!result)
            {
                return Json(true);
            }
            else
            {
                return Json($"Slug {slug} is already in use. Please choose another.");
            }
        }
    }
}