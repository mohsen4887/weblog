using System.Collections.Generic;
using System.Linq;
using Weblog.Domain.Models;

namespace Weblog.ViewModels
{
    public class CategoryWithChildrenVM
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public List<CategoryWithChildrenVM>? Children { get; set; }

        public CategoryWithChildrenVM(Category category)
        {
            Id = category.Id;
            ParentId = category.ParentId;
            Title = category.Title;
            Order = category.Order;
            if (category.Children != null)
            {
                Children = category.Children.Select(c => new CategoryWithChildrenVM(c)).ToList();
            }
        }
    }
}
