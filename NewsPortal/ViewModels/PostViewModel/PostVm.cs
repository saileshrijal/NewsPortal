namespace NewsPortal.ViewModels.PostViewModel
{
    public class PostVm
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public List<string>? Categories { get; set; }
        public bool Status { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}