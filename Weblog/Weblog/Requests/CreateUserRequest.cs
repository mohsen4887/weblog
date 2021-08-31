using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Requests
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CreateUserRequest()
        {
            
        }
        public CreateUserRequest(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
