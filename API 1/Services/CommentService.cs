using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_1.Models;
using API_1.Repositories;
using Newtonsoft.Json.Linq;

namespace API_1.Services
{
    public class CommentService
    {
        private CommentRepository _commentRepository;

        public CommentService(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public List<CommentModel> GetComments()
        {
            return _commentRepository.GetComments();
        }

        public JObject GetComment(String id)
        {
            var comment = _commentRepository.GetComment(id);
            if (comment != null) return JObject.Parse("{ \"message\" : \"Comment found!\", post : " + JObject.FromObject(comment) + ", \"status\" : \"200\" }");
            else if (comment == null) return JObject.Parse("{ \"message\" : \"Couldn´t find current comment\", \"status\" : \"404\" }");
            else return JObject.Parse("{ \"message\" : \"Bad request\", \"status\" : \"400\" }");
        }

        public JObject RemoveComment(String id)
        {
            var response = _commentRepository.RemoveComment(id);
            if (response == 200) return JObject.Parse("{ \"message\" : \"Comment deleted successfully!\", \"status\" : \"200\" }");
            else if (response == 404) return JObject.Parse("{ \"message\" : \"Couldn´t find current comment\", \"status\" : \"404\" }");
            else return JObject.Parse("{ \"message\" : \"Couldn´t delete current comment\", \"status\" : \"400\" }");
        }


        public JObject AddComment(CommentModel comment, string UserId, string CommentId)
        {
            if (UserId == null || comment.PostId == null || comment.Message == null || comment.Message == "")
                return JObject.Parse("{ \"message\" : \"Put all data please!\", \"post\": " + JObject.FromObject(comment) + ",  \"status\" : \"400\" }");
            else
            {
                object response = _commentRepository.AddComment(comment, UserId, CommentId);
                if (response == null) return JObject.Parse("{ \"message\" : \"User or Post doesn't exist! use a valid user or post id\",  \"status\" : \"404\" }");
                return JObject.Parse("{ \"message\" : \"Comment added successfully!\", \"comment\" : " + JObject.FromObject(comment) + ", \"status\" : \"200\" }");
            }
        }


        public JObject UpdateComment(String id, CommentModel param, string UserId)
        {
            var response = _commentRepository.UpdateComment(id, param, UserId);
            object commentUpdated = _commentRepository.GetComment(id);
            if (response == 200) return JObject.Parse("{ \"message\" : \"Comment updated successfully!\", \"comment\": " + JObject.FromObject(commentUpdated) + ", \"status\" : \"200\" }");
            else if (response == 404) return JObject.Parse("{ \"message\" : \"Couldn´t find current comment\", \"status\" : \"404\" }");
            else if (response == 401) return JObject.Parse("{ \"message\" : \"This comment isn't yours, you can't update it!\", \"status\" : \"401\" }");
            else return JObject.Parse("{ \"message\" : \"Couldn´t update current comment, userid doesn't exist!\", \"status\" : \"400\" }");
        }
    }
}
