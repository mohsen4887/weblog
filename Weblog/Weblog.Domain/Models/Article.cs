using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weblog.Domain.Models
{
    [Table("Articles")]
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        public string Image { get; set; }

        [Required] 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; }
        public string Status { get; set; }

        public Article()
        {
            
        }
    }
}
