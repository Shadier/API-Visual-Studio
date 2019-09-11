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
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }
        // GET: api/
        [HttpGet]
        public List<CommentModel> Get()
        {
            return _commentService.GetComments();
        }

        [HttpGet("{id}")]
        public ActionResult Get(String id)
        {
            JObject objResponse = _commentService.GetComment(id);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (404 == (int)objResponse["status"]) return new NotFoundObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(String id)
        {
            JObject objResponse = _commentService.RemoveComment(id);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (404 == (int)objResponse["status"]) return new NotFoundObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);
        }

        // POST: api/Post - PARA UN COMENTARIO NUEVO
        [HttpPost]
        public ActionResult Post(CommentModel comment)
        {
            string UserId = Request.Headers["UserId"];

            JObject objResponse = _commentService.AddComment(comment, UserId, null);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (404 == (int)objResponse["status"]) return new NotFoundObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);

        }

        // POST: api/Post - PARA CONTESTAR A OTRO COMENTARIO
        [HttpPost("{CommentId}")]
        public ActionResult Post(CommentModel comment, string CommentId)
        {
            string UserId = Request.Headers["UserId"];

            JObject objResponse = _commentService.AddComment(comment, UserId, CommentId);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (404 == (int)objResponse["status"]) return new NotFoundObjectResult(objResponse);
            else return new BadRequestObjectResult(objResponse);

        }

        [HttpPut("{id}")]
        public ActionResult Put(String id, CommentModel param)
        {
            string UserId = Request.Headers["UserId"];
            JObject objResponse = _commentService.UpdateComment(id, param, UserId);
            if (200 == (int)objResponse["status"]) return new ObjectResult(objResponse);
            else if (401 == (int)objResponse["status"]) return new UnauthorizedResult();
            else return new BadRequestObjectResult(objResponse);
        }

        

    } 
}