using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_1.Services;
using API_1.Models;
using Newtonsoft.Json.Linq;

namespace API_1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: User
        [HttpGet]
        public List<UserModel> Get()
        {
            return _userService.GetUsers();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public ActionResult Get(String id)
        {
            JObject objResponse = _userService.GetUser(id);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (404 == (int)objResponse["status"]) return new NotFoundObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);
        }

        // POST: api/User
        [HttpPost]
        public ActionResult Post(UserModel user)
        {
            JObject objResponse = _userService.AddUser(user);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public ActionResult Put(String id, UserModel param)
        {
            JObject objResponse = _userService.UpdateUser(id, param);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(String id)
        {
            JObject objResponse = _userService.RemoveUser(id);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (404 == (int)objResponse["status"]) return new NotFoundObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);
        }
    }
}