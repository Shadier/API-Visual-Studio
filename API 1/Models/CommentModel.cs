using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_1.Models;

namespace API_1.Models
{
    public class CommentModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string PostId { get; set; }
        public PostModel Post { get; set; }
        public UserModel User { get; set; }

        //en caso de ser una respuesta a otro comentario
        public CommentModel Comment { get; set; }
    }
}
