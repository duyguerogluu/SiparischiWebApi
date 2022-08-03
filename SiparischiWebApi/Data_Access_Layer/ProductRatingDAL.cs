using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class ProductRatingDAL : BaseDAL
    {
        public IEnumerable<ProductRating> GetAllProductRatings()
        {
            return db.ProductRating.ToList();
        }

        public IEnumerable<ProductRating> GetProductRatingsById(int id)
        {
            return db.ProductRating.Where(x => x.id == id).ToList();
        }

        public String GetProductRatingsByCategoryIdAverage(int productId)
        {
            var rating = db.ProductRating.Where(x => x.product_id == productId).Select(x => x.point_value).Average();
            return rating.ToString();
        }

        public ProductRating CreateProductRating(ProductRating productRating)
        {
            db.ProductRating.Add(productRating);
            db.SaveChanges();
            return productRating;
        }

        public ProductRating UpdateProductRating(ProductRating productRating)
        {
            db.Entry(productRating).State = EntityState.Modified;
            db.SaveChanges();
            return productRating;
        }

        public void DeleteProductRating(int id)
        {
            db.ProductRating.Remove(db.ProductRating.Find(id));
            db.SaveChanges();
        }

        public bool IsThereAnyProductRating(int id)
        {
            return db.ProductRating.Any(x => x.id == id);
        }

    }
}