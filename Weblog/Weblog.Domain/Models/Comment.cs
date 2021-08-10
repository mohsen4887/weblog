using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weblog.Domain.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Body { get; set; }
        public bool IsPublished { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Required]
        public int ArticleId { get; set; }
        [Required]
        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
        public Comment()
        {
            
        }
    }
}
