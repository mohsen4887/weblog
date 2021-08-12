using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weblog.Domain.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get;  set; }
        [Required]
        [MaxLength(200)]
        public string Email { get;  set; }
        public  List<Article> Articles { get; }
        public  List<Comment> Comments { get;  }
        public User()
        {
            
        }

        public User(string name, string email)
        {
            if (name.Length > 50)
            {
                throw new Exception("نام کاربر نمی تاند بیشتر از 50 کاراکتر باشد");
            }

            this.Name = name;
            this.Email = email;
        }
    }

}
