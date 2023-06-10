using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsPortal.Dtos.PostDto;
using NewsPortal.Helpers.Interface;
using NewsPortal.Models;
using NewsPortal.Repositories.Interface;
using NewsPortal.Services.Interfaces;
using NewsPortal.ViewModels.PostViewModel;

namespace NewsPortal.Areas.cms.Controllers
{
    [Area("cms")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileHelper _fileHelper;
        private readonly INotyfService _notyfService;
        private readonly UserManager<ApplicationUser> _userManager;
        public PostController(IPostService postService, IPostRepository postRepository, ICategoryRepository categoryRepository, IFileHelper fileHelper, INotyfService notyfService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _fileHelper = fileHelper;
            _notyfService = notyfService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAllWithAuthorAndCategoryAsync();
            var vm = posts.Select(x => new PostVm()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.ApplicationUser!.FullName,
                Categories = x.PostCategories!.Select(x => x.Category!.Title).ToList()!,
                Status = x.IsPublished,
                PublishedDate = x.PublishedDate
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                var vm = new CreatePostVm()
                {
                    Categories = categories.Select(x => new SelectListItem()
                    {
                        Text = x.Title,
                        Value = x.Id.ToString()
                    }).ToList()
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
        public async Task<IActionResult> Add(CreatePostVm vm, bool isPublish)
        {
            try
            {
                if (!ModelState.IsValid) return View(vm);
                var currentUser = await _userManager.GetUserAsync(User);
                var dto = new CreatePostDto
                {
                    Title = vm.Title,
                    Slug = vm.Slug,
                    ShortDescription = vm.ShortDescription,
                    Description = vm.Description,
                    IsPublished = isPublish,
                    MetaKeywords = vm.MetaKeywords,
                    MetaDescription = vm.MetaDescription,
                    CategoryIds = vm.CategoryIds,
                    IsBreakingNews = vm.IsBreakingNews,
                    IsTicker = vm.IsTicker,
                    ApplicationUserId = currentUser!.Id
                };

                if (vm.Thumbnail != null)
                {
                    var fileName = await _fileHelper.UploadFile(vm.Thumbnail, "thumbnail");
                    dto.ThumbnailUrl = fileName;
                }

                await _postService.CreateAsync(dto);
                _notyfService.Success("Post created successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                var post = await _postRepository.GetWithAuthorAndCategoryByIdAsync(id);
                var vm = new EditPostVm()
                {
                    Categories = categories.Select(x => new SelectListItem()
                    {
                        Text = x.Title,
                        Value = x.Id.ToString()
                    }).ToList(),
                    Id = post.Id,
                    Title = post.Title,
                    Slug = post.Slug,
                    ShortDescription = post.ShortDescription,
                    Description = post.Description,
                    MetaKeywords = post.MetaKeywords,
                    MetaDescription = post.MetaDescription,
                    CategoryIds = post.PostCategories!.Select(x => x.CategoryId).ToList(),
                    IsBreakingNews = post.IsBreakingNews,
                    IsTicker = post.IsTicker,
                    ThumbnailUrl = post.ThumbnailUrl
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
                await _postService.DeleteAsync(id);
                _notyfService.Success("Post deleted successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}