using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_1.Models;
using API_1.Repositories;
using Newtonsoft.Json.Linq;

namespace API_1.Services
{
    public class PostService
    {
        private PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public List<PostModel> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        public JObject AddPost(PostModel post, string UserId)
        {
            if (UserId == null || post.Content == null)
                return JObject.Parse("{ \"message\" : \"Put all data please!\", \"post\": "+ JObject.FromObject(post) + ",  \"status\" : \"400\" }");
            else
            {
                object response = _postRepository.AddPost(post, UserId);
                if(response == null) return JObject.Parse("{ \"message\" : \"User id doesn't exist! use a valid user id\",  \"status\" : \"404\" }");
                return JObject.Parse("{ \"message\" : \"Post added successfully!\", \"post\" : " + JObject.FromObject(post) + ", \"status\" : \"200\" }");
            }
        }

        public JObject GetPost(String id)
        {
            var post = _postRepository.GetPost(id);
            if (post != null) return JObject.Parse("{ \"message\" : \"Post found!\", post : " + JObject.FromObject(post) + ", \"status\" : \"200\" }");
            else if (post == null) return JObject.Parse("{ \"message\" : \"Couldn´t find current post\", \"status\" : \"404\" }");
            else return JObject.Parse("{ \"message\" : \"Bad request\", \"status\" : \"400\" }");
        }

        public JObject RemovePost(String id)
        {
            var response = _postRepository.RemovePost(id);
            if (response == 200) return JObject.Parse("{ \"message\" : \"Post deleted successfully!\", \"status\" : \"200\" }");
            else if (response == 404) return JObject.Parse("{ \"message\" : \"Couldn´t find current post\", \"status\" : \"404\" }");
            else return JObject.Parse("{ \"message\" : \"Couldn´t delete current post\", \"status\" : \"400\" }");
        }
        
        public JObject UpdatePost(String id, PostModel param, string UserId)
        {
            var response = _postRepository.UpdatePost(id, param, UserId);
            object postUpdated = _postRepository.GetPost(id);
            if (response == 200) return JObject.Parse("{ \"message\" : \"Post updated successfully!\", \"post\": "+ JObject.FromObject(postUpdated) + ", \"status\" : \"200\" }");
            else if (response == 404) return JObject.Parse("{ \"message\" : \"Couldn´t find current post\", \"status\" : \"404\" }");
            else if (response == 401) return JObject.Parse("{ \"message\" : \"This post isn't yours, you can't update it!\", \"status\" : \"401\" }");
            else return JObject.Parse("{ \"message\" : \"Couldn´t update current post, userid doesn't exist!\", \"status\" : \"400\" }");
        }
    }
}
