using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_1.Models;

namespace API_1.Models
{
    public class PostModel
    {
        public string Id { get; set; }
        public UserModel User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
