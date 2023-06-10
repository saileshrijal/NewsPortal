using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NewsPortal.ViewModels.PostViewModel
{
    public class CreatePostVm
    {
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
        public IFormFile? Thumbnail { get; set; }

        [Required(ErrorMessage = "At least one category is required")]
        public List<int>? CategoryIds { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}