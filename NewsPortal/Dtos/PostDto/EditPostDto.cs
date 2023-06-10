
namespace NewsPortal.Dtos.PostDto
{
    public class EditPostDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public bool IsPublished { get; set; }
        public string? ApplicationUserId { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
        public List<int>? CategoryIds { get; set; }
    }
}