using System.Collections.Generic;
using Weblog.ViewModels;

namespace Weblog.Responses
{
    public class LoginResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public LoginResponse()
        {
        }

        public LoginResponse(string name, string email, string token)
        {
            Name = name;
            Email = email;
            Token = token;
        }
    }
}
