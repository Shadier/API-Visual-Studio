using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_1.Models;
using API_1.Repositories;
using Newtonsoft.Json.Linq;

namespace API_1.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public JObject GetUser(String id)
        {
            var user = _userRepository.GetUser(id);
            if (user != null) return JObject.Parse("{ \"message\" : \"User found!\", user : "+ JObject.FromObject(user)  + ", \"status\" : \"200\" }");
            else if (user == null) return JObject.Parse("{ \"message\" : \"Couldn´t find current user\", \"status\" : \"404\" }");
            else return JObject.Parse("{ \"message\" : \"Bad request\", \"status\" : \"400\" }");
        }
        public List<UserModel> GetUsers()
        {
            return _userRepository.GetUsers();
        }
        public JObject AddUser(UserModel user)
        {
            if (user.Email == null || user.FirstName == null || user.LastName == null)
                return JObject.Parse("{ \"message\" : \"Put all data please!\",  \"status\" : \"400\" }");
            else
            {
                _userRepository.AddUser(user);
                return JObject.Parse("{ \"message\" : \"User added successfully!\", \"user\" : " + JObject.FromObject(user) + ", \"status\" : \"200\" }");
            }
        }
        public JObject RemoveUser(String id)
        {
            var response = _userRepository.RemoveUser(id);
            if (response == 200) return JObject.Parse("{ \"message\" : \"User deleted successfully!\", \"status\" : \"200\" }");
            else if (response == 404) return JObject.Parse("{ \"message\" : \"Couldn´t find current user\", \"status\" : \"404\" }");
            else return JObject.Parse("{ \"message\" : \"Couldn´t delete current user\", \"status\" : \"400\" }");
        }

        public JObject UpdateUser(String id, UserModel param)
        {
            var response = _userRepository.UpdateUser(id, param);
            if (response == 200) return JObject.Parse("{ \"message\" : \"User updated successfully!\", \"status\" : \"200\" }");
            else return JObject.Parse("{ \"message\" : \"Couldn´t update current user\", \"status\" : \"400\" }");
        }

    }
}
