using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weblog.Domain;
using Weblog.Domain.Models;
using Weblog.Requests;
using Weblog.Responses;
using Weblog.Services;
using Weblog.ViewModels;

namespace Weblog.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _db;
        private readonly FileHandlerService _fs;
        private readonly IHostingEnvironment _env;
        public UserController(IHostingEnvironment env)
        {
            this._db = new DatabaseContext();
            this._fs = new FileHandlerService(env);
            _env = env;
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
                    .Select(x => new UserVM(x.Id, x.Name, x.Email, x.IsAdmin))
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
                    .Select(x => new UserVM(x.Id,x.Name, x.Email, x.IsAdmin))
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
                var user = new User(request.Name, request.Email, request.Password, request.IsAdmin);
                this._db.Users.Add(user);
                _db.SaveChanges();
                return Ok(new UserVM(user.Id, user.Name, user.Email, user.IsAdmin));

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
                user.UpdateByAdmin(request.Name, request.Email, request.Password, request.IsAdmin);
                _db.SaveChanges();
                return Ok(new UserVM(user.Id, user.Name, user.Email, user.IsAdmin));

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


        [HttpPost]
        [Route("image")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            try
            {
                var result = await _fs.Store(request.Image);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
