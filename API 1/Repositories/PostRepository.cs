using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_1.Models;

namespace API_1.Repositories
{
    public class PostRepository
    {
        private List<PostModel> posts = new List<PostModel>();
        private UserRepository _userRepository;

        public PostRepository(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        
        public object AddPost(PostModel post, string UserId)
        {
            UserModel user = (UserModel) _userRepository.GetUser(UserId);
            if (user == null) return null;
            post.User = user;
            post.Id = (DateTime.Now).ToString("yyyyMMddTHHmmssZP");
            post.Date = DateTime.Now;
            posts.Add(post);
            return post;
        }

        public List<PostModel> GetPosts()
        {
            return posts;
        }

        public object GetPost(String id)
        {
            return posts.Find(x => x.Id == id);
        }

        public int RemovePost(String id)
        {
            var remove = posts.Find(x => x.Id == id);
            if (remove == null) return 404;
            else if (posts.Remove(remove)) return 200;
            else return 400;
        }
        public int UpdatePost(String id, PostModel param, string UserId)
        {
            UserModel user = (UserModel)_userRepository.GetUser(UserId);
            //No existe el usuario asi que ni siquiera hacemos el update
            if (user == null) return 400;

            //buscamos el post
            var update = posts.Find(x => x.Id == id);
            if (update == null) return 404;
            if (update.User.Id != UserId) return 401;
            update.Content = param.Content;
            update.Date = DateTime.Now;
            return 200;
        }
    }
}
