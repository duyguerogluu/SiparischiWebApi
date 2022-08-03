using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class UserAddressDAL : BaseDAL
    {
        public IEnumerable<UserAddress> GetAllUserAddresses()
        {
            return db.UserAddress.ToList();
        }

        public IEnumerable<UserAddress> GetUserAddressesById(int id)
        {
            return db.UserAddress.Where(x => x.user_id == id).ToList();
        }

        public IEnumerable<UserAddress> GetUserSelectedAddress(int id)
        {
            return db.UserAddress.Where(x => x.user_id == id & x.selected_address == true).ToList();
        }

        public UserAddress CreateUserAddress(UserAddress userAddress)
        {
            db.UserAddress.Add(userAddress);
            db.SaveChanges();
            return userAddress;
        }

        public UserAddress UpdateUserAddress(UserAddress userAddress)
        {
            db.Entry(userAddress).State = EntityState.Modified;
            db.SaveChanges();
            return userAddress;
        }

        public void DeleteUserAddress(int id)
        {
            db.UserAddress.Remove(db.UserAddress.Find(id));
            db.SaveChanges();
        }

        public bool IsThereAnyUserAddress(int id)
        {
            return db.UserAddress.Any(x => x.id == id);
        }
    }
}