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

        public CreateUserRequest()
        {
            
        }
        public CreateUserRequest(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
