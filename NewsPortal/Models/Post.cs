using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public bool IsPublished { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public DateTime PublishedDate { get; set; }

        public bool IsBreakingNews { get; set; }
        public bool IsTicker { get; set; }

        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }

        public List<PostCategory>? PostCategories { get; set; }
    }
}