using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;
using System.Data.Entity;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class BusinessWorkTypeDAL : BaseDAL
    {
        public IEnumerable<BusinessWorkType> GetAllBusinessWorkTypes()
        {
            return db.BusinessWorkType.ToList();
        }

        public IEnumerable<BusinessWorkType> GetBusinessWorkTypesById(int id)
        {
            return db.BusinessWorkType.Where(x => x.id == id).ToList();
        }

        public BusinessWorkType CreateBusinessWorkType(BusinessWorkType businessWorkType)
        {
            db.BusinessWorkType.Add(businessWorkType);
            db.SaveChanges();
            return businessWorkType;
        }

        public BusinessWorkType UpdateBusinessWorkType(BusinessWorkType businessWorkType)
        {
            db.Entry(businessWorkType).State = EntityState.Modified;
            db.SaveChanges();
            return businessWorkType;
        }

        public void DeleteBusinessWorkType(int id)
        {
            db.BusinessWorkType.Remove(db.BusinessWorkType.Find(id));
            db.SaveChanges();
        }

        public bool IsThereAnyBusinessWorkType(int id)
        {
            return db.BusinessWorkType.Any(x => x.id == id);
        }
    }
}