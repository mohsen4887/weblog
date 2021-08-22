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
        public IActionResult Detail(int id)
        {
            try
            {
                var user = this._db.Users.Where(x => x.Id == id)
                    .Select(x => new UserVM(x.Id,x.Name, x.Email))
                    .FirstOrDefault();
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
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
        public IActionResult Update(int id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var user = this._db.Users.Find(id);
                if (user == null)
                {
                    return NotFound();
                }
                user.Update(request.Name, request.Email);
                _db.SaveChanges();
                return Ok(new UserVM(user.Id, user.Name, user.Email));

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _db.Users.Find(id);
                _db.Users.Remove(user);
                _db.SaveChanges();

                return Ok("Selected user removed successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
