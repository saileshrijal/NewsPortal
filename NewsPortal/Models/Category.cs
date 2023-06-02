namespace NewsPortal.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}