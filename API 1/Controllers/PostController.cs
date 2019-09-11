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
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        // GET: api/
        [HttpGet]
        public List<PostModel> Get()
        {
            return _postService.GetPosts();
        }

        // POST: api/Post
        [HttpPost]
        public ActionResult Post(PostModel post)
        {
            string id = Request.Headers["UserId"];

            JObject objResponse = _postService.AddPost(post, id);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (404 == (int)objResponse["status"]) return new NotFoundObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public ActionResult Get(String id)
        {
            JObject objResponse = _postService.GetPost(id);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (404 == (int)objResponse["status"]) return new NotFoundObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(String id)
        {
            JObject objResponse = _postService.RemovePost(id);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (404 == (int)objResponse["status"]) return new NotFoundObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public ActionResult Put(String id, PostModel param)
        {
            string UserId = Request.Headers["UserId"];
            JObject objResponse = _postService.UpdatePost(id, param, UserId);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (401 == (int)objResponse["status"]) return new UnauthorizedResult();
            else return new BadRequestObjectResult(objResponse);
        }
    }
}