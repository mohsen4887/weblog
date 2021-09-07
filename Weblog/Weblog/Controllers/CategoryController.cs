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
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private readonly DatabaseContext _db;
        public CategoryController()
        {
            this._db = new DatabaseContext();
        }
        public IActionResult Index(UsersListRequest request)
        {
            try
            {
                var categories = _db.Categories
                    .OrderBy(c => c.Order)
                    .ToList();

                return Ok(categories.Where(x => x.ParentId == null).ToList());
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
                return BadRequest(new NotImplementedException());

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
                return BadRequest(new NotImplementedException());

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
                var category = _db.Categories.Find(id);
                return Ok(category);
                _db.Categories.Remove(category);
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
