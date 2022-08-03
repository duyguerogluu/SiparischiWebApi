using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class BusinessDAL : BaseDAL
    {
        BusinessRatingDAL businessRatingDAL = new BusinessRatingDAL();
        public IEnumerable<Business> GetAllBusinessTypes()
        {
            return db.Business.ToList();
        }

        public Business GetBusinessByApiKey(string apiKey)
        {
            return db.Business.FirstOrDefault(x => x.business_key.ToString() == apiKey);
        }

       /*public HttpResponseMessage GetBusinessLocation(string city)
        {
            //return db.Business.Where(x => x.city == city).ToList();
            var result = (from b in db.Business
                         join br in db.BusinessRating on
                         b.id equals br.business_id
                          into rating
                          from r in rating.DefaultIfEmpty()
                          where b.city == city
                          group r by new {b.business_name } into g
                   select new
                   {
                       g.Key,
                       g.Key.business_name,
                       point_value = g.Average(x => x == null ? 0 : x.point_value),
                       point_count = g.Count(),
                   }).ToList();
            IEnumerable<string> query = result as IEnumerable<String>;
            return Request.CreateResponse(HttpStatusCode.OK, result);

            /*select b.business_name, avg(br.point_value) point_value, count(br.point_value) point_count FROM 
                Business b left join BusinessRating br on b.id = br.business_id where b.city ='Mersin'
                group by b.id,b.username,b.password,b.business_name,b.status,b.phone_number,b.email,b.city,
                b.district,b.neighbourhood,b.situation,b.starting_date,b.ending_date,
                b.image_url,b.location,b.business_type_id,b.business_key
        }*/

        public IEnumerable<Business> GetBusinessById(int id)
        {
            return db.Business.Where(x => x.id == id).ToList();
        }

        public Business UpdateBusiness(Business business)
        {
            db.Entry(business).State = EntityState.Modified;
            db.SaveChanges();
            return business;
        }

        public bool IsThereAnyBusiness(int id)
        {
            return db.Business.Any(x => x.id == id);
        }
    }
}