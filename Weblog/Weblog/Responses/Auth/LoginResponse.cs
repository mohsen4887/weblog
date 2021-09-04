﻿using System.Collections.Generic;
using Weblog.ViewModels;

namespace Weblog.Responses
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public LoginResponse()
        {
        }

        public LoginResponse(int id, string name, string email, string token)
        {
            Id = id;
            Name = name;
            Email = email;
            Token = token;
        }
    }
}
