using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

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
            if (name == null)
            {
                throw new Exception("نام کاربر را وارد کنید");
            }

            if (email == null)
            {
                throw new Exception("ایمیل کاربر را وارد کنید");
            }

            if (name.Length >= 50)
            {
                throw new Exception("نام کاربر نمی تاند بیشتر از 50 کاراکتر باشد");
            }

            if (email.Length >= 200)
            {
                throw new Exception("ایمیل کاربر نمی تاند بیشتر از 200 کاراکتر باشد");
            }

            var validEmail = Regex.Match(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            if (!validEmail.Success)
            {
                throw new Exception("آدرس ایمیل وارد شده معتبر نمی باشد");
            }

            this.Name = name;
            this.Email = email;
        }
    }

}
