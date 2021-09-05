using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weblog.Domain;
using Weblog.Domain.Models;
using Weblog.Requests;
using Weblog.Responses;
using Weblog.ViewModels;

namespace Weblog.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _db;
        public AuthController()
        {
            this._db = new DatabaseContext();
        }


        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(u => u.Email == request.Email);
                if (user == null)
                {
                    return NotFound("کاربری با این آدرس ایمیل یافت نشد");
                }
                user.Login(request.Password);
                _db.SaveChanges();

                return Ok(new LoginResponse(user.Id, user.Name, user.Email, user.Token, user.IsAdmin));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (request.Password != request.PasswordConfirm)
                {
                    return BadRequest("تکرار رمز عبور مطابقت ندارد");
                }

                var user = new User(request.Name, request.Email, request.Password);
                user.Login(request.Password);
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                return Ok(new LoginResponse(user.Id, user.Name, user.Email, user.Token, user.IsAdmin));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
