using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.ViewModels.CategoryViewModel
{
    public class CategoryDetailsVm
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}