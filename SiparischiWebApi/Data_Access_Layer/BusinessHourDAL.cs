using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class BusinessHourDAL : BaseDAL
    {
        public IEnumerable<BusinessHours> GetAllBusinessHours()
        {
            return db.BusinessHours.ToList();
        }

        public IEnumerable<BusinessHours> GetBusinessHoursById(int id)
        {
            return db.BusinessHours.Where(x => x.id == id).ToList();
        }

        public BusinessHours CreateBusinessHour(BusinessHours businessHour)
        {
            db.BusinessHours.Add(businessHour);
            db.SaveChanges();
            return businessHour;
        }

        public BusinessHours UpdateBusinessHour(BusinessHours businessHour)
        {
            db.Entry(businessHour).State = EntityState.Modified;
            db.SaveChanges();
            return businessHour;
        }

        public void DeleteBusinessHour(int id)
        {
            db.BusinessHours.Remove(db.BusinessHours.Find(id));
            db.SaveChanges();
        }

        public bool IsThereAnyBusinessHour(int id)
        {
            return db.BusinessHours.Any(x => x.id == id);
        }
    }
}