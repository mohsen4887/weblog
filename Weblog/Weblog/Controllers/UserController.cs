using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Weblog.Domain;
using Weblog.Domain.Models;
using Weblog.Requests;
using Weblog.ViewModels;

namespace Weblog.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _db;
        public UserController()
        {
            this._db = new DatabaseContext();
        }
        public IActionResult Index()
        {
            try
            {
                var users = _db.Users
                    .Select(x => new UserVM(x.Id, x.Name, x.Email))
                    .ToList();

                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("{id}")]
        public User Detail(string id)
        {
            var user = this._db.Users.Find(id);
            return user;
        }


        [HttpPost]
        public IActionResult Store([FromBody] CreateUserRequest request)
        {
            try
            {
                var user = new User(request.Name, request.Email);
                this._db.Users.Add(user);
                _db.SaveChanges();
                return Ok(new UserVM(user.Id, user.Name, user.Email));

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("{id}")]
        public User Update(string id)
        {
            var user = _db.Users.Find(id);
            user.Name = "Mohsen";
            _db.Users.Update(user);
            _db.SaveChanges();
            return user;
        }


        [HttpDelete]
        [Route("{id}")]
        public string Delete(string id)
        {
            var user = _db.Users.Find(id);
            _db.Users.Remove(user);
            _db.SaveChanges();

            return "success";
        }

    }
}
