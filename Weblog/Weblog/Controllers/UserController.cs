using Microsoft.AspNetCore.Mvc;

namespace Weblog.Controllers
{
    [Route("users")]
    public class UserController
    {
        public UserController()
        {

        }
        public string Index()
        {
            return "list of users";
        }

        [Route("{id}")]
        public string Detail(string id)
        {
            return $"user detail: {id}";
        }


        [HttpPost]
        public string Store()
        {
            return "add new user";
        }

        [HttpPost]
        [Route("{id}")]
        public string Update(string id)
        {
            return $"update user {id}";
        }


        [HttpDelete]
        [Route("{id}")]
        public string Delete(string id)
        {
            return $"delete user {id}";
        }

    }
}
