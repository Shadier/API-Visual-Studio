using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_1.Models;

namespace API_1.Repositories
{
    public class CommentRepository
    {
        private UserRepository _userRepository;
        private PostRepository _postRepository;
        private List<CommentModel> comments = new List<CommentModel>();


        public List<CommentModel> GetComments()
        {
            return comments;
        }
        public CommentModel GetComment(string CommentId)
        {
            return comments.Find(x => x.Id == CommentId);
        }

        public int RemoveComment(String id)
        {
            var remove = comments.Find(x => x.Id == id);
            if (remove == null) return 404;
            else if (comments.Remove(remove)) return 200;
            else return 400;
        }
        public CommentRepository(UserRepository userRepository, PostRepository postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }
        public object AddComment(CommentModel comment, string UserId, string CommentId)
        {
            UserModel user = (UserModel)_userRepository.GetUser(UserId);
            if (user == null) return null;
            PostModel post = (PostModel)_postRepository.GetPost(comment.PostId);
            if (post == null) return null;
            CommentModel commentToResponse = GetComment(CommentId);
            comment.Comment = commentToResponse;
            comment.User = user;
            comment.Post = post;
            comment.Id = (DateTime.Now).ToString("yyyyMMddTHHmmssZC");
            comment.Date = DateTime.Now;
            comments.Add(comment);
            return comment;
        }
        public int UpdateComment(String id, CommentModel param, string UserId)
        {
            UserModel user = (UserModel)_userRepository.GetUser(UserId);
            //No existe el usuario asi que ni siquiera hacemos el update
            if (user == null) return 400;

            //buscamos el comment
            var update = comments.Find(x => x.Id == id);
            if (update == null) return 404;
            if (update.User.Id != UserId) return 401;
            update.Message = param.Message;
            update.Date = DateTime.Now;
            return 200;
        }

    }
}
