using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weblog.Domain;
using Weblog.Domain.Models;
using Weblog.Requests;
using Weblog.Responses;
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
        public IActionResult Index(UsersListRequest request)
        {
            try
            {

                var users = _db.Users.AsNoTracking();

                if (request.Query != null)
                {
                    users = users.Where(x =>
                        x.Name.Contains(request.Query) || x.Email.Contains(request.Query));
                }

                users = request.Sort switch
                {
                    "oldest" => users.OrderBy(x => x.Id),
                    "latest" => users.OrderByDescending(x => x.Id),
                    _ => users.OrderByDescending(x => x.Id)
                };


                var usersCount = users.Count();

                var result = users.Skip((request.Page - 1) * request.PerPage).Take(request.PerPage)
                    .Select(x => new UserVM(x.Id, x.Name, x.Email))
                    .ToList();

                return Ok(new UsersListResponse(result, usersCount));
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
                var user = new User(request.Name, request.Email, request.Password);
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
                user.UpdateByAdmin(request.Name, request.Email, request.Password);
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
