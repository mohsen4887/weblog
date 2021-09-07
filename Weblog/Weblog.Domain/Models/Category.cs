using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weblog.Domain.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
        public List<Category>? Children { get; set; }

        public List<Article> Articles { get; set; }
        public Category()
        {
            
        }
    }
}
