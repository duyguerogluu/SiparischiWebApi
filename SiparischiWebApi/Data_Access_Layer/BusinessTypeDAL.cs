using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class BusinessTypeDAL : BaseDAL
    {
        public IEnumerable<BusinessType> GetAllBusinessTypes()
        {
            return db.BusinessType.ToList();
        }

        public IEnumerable<BusinessType> GetBusinessTypesById(int id)
        {
            return db.BusinessType.Where(x => x.id == id).ToList();
        }

        public BusinessType CreateBusinessType(BusinessType businessType)
        {
            db.BusinessType.Add(businessType);
            db.SaveChanges();
            return businessType;
        }

        public BusinessType UpdateBusinessType(BusinessType businessType)
        {
            db.Entry(businessType).State = EntityState.Modified;
            db.SaveChanges();
            return businessType;
        }

        public void DeleteBusinessType(int id)
        {
            db.BusinessType.Remove(db.BusinessType.Find(id));
            db.SaveChanges();
        }

        public bool IsThereAnyBusinessType(int id)
        {
            return db.BusinessType.Any(x => x.id == id);
        }

    }
}