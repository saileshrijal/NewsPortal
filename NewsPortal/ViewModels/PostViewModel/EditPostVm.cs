
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NewsPortal.ViewModels.PostViewModel
{
    public class EditPostVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        public string? Slug { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
        public bool IsBreakingNews { get; set; }
        public bool IsTicker { get; set; }
        public string? ThumbnailUrl { get; set; }
        public IFormFile? Thumbnail { get; set; }

        [Required(ErrorMessage = "At least one category is required")]
        public List<int>? CategoryIds { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}