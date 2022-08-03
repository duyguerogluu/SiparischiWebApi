using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class UserDAL : BaseDAL
    {
        public User GetUserByApiKey(string apiKey)
        {
            return db.User.FirstOrDefault(x => x.user_key.ToString() == apiKey);
        }

        public IEnumerable<User> GetUserById(int id)
        {
            return db.User.Where(x => x.id == id).ToList();
        }

    }
}