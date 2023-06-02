using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace NewsPortal.ViewModels.CategoryViewModel
{
    public class EditCategoryVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required"), MaxLength(50, ErrorMessage = "Title must be less than 50 characters")]
        public string? Title { get; set; }
        public string? Description { get; set; }

        [Remote(action: "IsSlugUnique", controller: "Category", AdditionalFields = nameof(Id))]
        [Required(ErrorMessage = "Slug is required"), MaxLength(50, ErrorMessage = "Slug must be less than 50 characters")]
        public string? Slug { get; set; }
    }
}