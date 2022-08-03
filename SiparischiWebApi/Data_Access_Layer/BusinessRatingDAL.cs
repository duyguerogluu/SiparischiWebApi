using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class BusinessRatingDAL : BaseDAL
    {
        public IEnumerable<BusinessRating> GetAllBusinessRatings()
        {
            return db.BusinessRating.ToList();
        }

        public IEnumerable<BusinessRating> GetBusinessRatingsById(int id)
        {
            return db.BusinessRating.Where(x => x.id == id).ToList();
        }

        public String GetBusinessRatingsByBusinessIdAverage(int businessId)
        {
            var rating = db.BusinessRating.Where(x => x.business_id == businessId).Select(x => x.point_value).Average();
            return rating.ToString();
        }
        public String GetBusinessRatingsByBusinessIdAverageCount(int businessId)
        {
            var count = db.BusinessRating.Where(x => x.business_id == businessId).Count();
            return count.ToString();
        }

        public BusinessRating CreateBusinessRating(BusinessRating businessRating)
        {
            db.BusinessRating.Add(businessRating);
            db.SaveChanges();
            return businessRating;
        }

        public BusinessRating UpdateBusinessRating(BusinessRating businessRating)
        {
            db.Entry(businessRating).State = EntityState.Modified;
            db.SaveChanges();
            return businessRating;
        }

        public void DeleteBusinessRating(int id)
        {
            db.BusinessRating.Remove(db.BusinessRating.Find(id));
            db.SaveChanges();
        }

        public bool IsThereAnyBusinessRating(int id)
        {
            return db.BusinessRating.Any(x => x.id == id);
        }

    }
}