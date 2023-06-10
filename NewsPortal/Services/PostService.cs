using NewsPortal.Dtos.PostDto;
using NewsPortal.Models;
using NewsPortal.Repositories.Interface;
using NewsPortal.Services.Interfaces;

namespace NewsPortal.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreatePostDto createPostDto)
        {
            var post = new Post
            {
                Title = createPostDto.Title,
                ShortDescription = createPostDto.ShortDescription,
                Description = createPostDto.Description,
                Slug = createPostDto.Slug,
                PublishedDate = DateTime.UtcNow,
                IsPublished = createPostDto.IsPublished,
                MetaDescription = createPostDto.MetaDescription,
                MetaKeywords = createPostDto.MetaKeywords,
                ThumbnailUrl = createPostDto.ThumbnailUrl,
                IsBreakingNews = createPostDto.IsBreakingNews,
                IsTicker = createPostDto.IsTicker,
                ApplicationUserId = createPostDto.ApplicationUserId
            };
            await _unitOfWork.CreateAsync(post);
            await _unitOfWork.SaveAsync();

            var postCategories = createPostDto.CategoryIds!.Select(categoryId => new PostCategory
            {
                PostId = post.Id,
                CategoryId = categoryId
            }).ToList();

            post.PostCategories = postCategories;
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _postRepository.GetByAsync(x=>x.Id == id);
            await _unitOfWork.DeleteAsync(post);
            await _unitOfWork.SaveAsync();
        }

        public Task EditAsync(EditPostDto editPostDto)
        {
            throw new NotImplementedException();
        }
    }
}