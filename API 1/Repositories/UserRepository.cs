using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_1.Models;

namespace API_1.Repositories
{
    public class UserRepository
    {
        private List<UserModel> users = new List<UserModel>();

        public UserRepository()
        {
            UserModel user = new UserModel();
            user.Id = (DateTime.Now).ToString("yyyyMMddTHHmmssZ");
            user.FirstName = "Raúl";
            user.LastName = "Moreno";
            users.Add(user);
        }
        public List<UserModel> GetUsers()
        {
            return users;
        }
        public object GetUser(String id)
        {
            return users.Find(x => x.Id == id);
        }
        public object AddUser(UserModel user)
        {
            user.Id = (DateTime.Now).ToString("yyyyMMddTHHmmssZ"); ;
            users.Add(user);
            return user;
        }
        public int RemoveUser(String id)
        {
            var remove = users.Find(x => x.Id == id);
            if (remove == null) return 404;
            else if (users.Remove(remove)) return 200;
            else return 400;
        }

        public int UpdateUser(String id, UserModel param)
        {
            var update = users.Find(x => x.Id == id);
            if (param.FirstName != null) update.FirstName = param.FirstName;
            if (param.LastName != null) update.LastName = param.LastName;
            if (param.Email != null) update.Email = param.Email;
            if (param.Phone != null) update.Phone = param.Phone;

            if (update == null) return 404;
            else return 200;
        }
    }
}
