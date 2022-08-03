using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SiparischiWebApi.Models;
using SiparischiWebApi.Data_Access_Layer;

namespace SiparischiWebApi.Controllers
{
    public class ProductRatingController : ApiController
    {
        ProductRatingDAL productRatingDAL = new ProductRatingDAL();

        [Authorize]
        public HttpResponseMessage Get()//https://localhost:44378/api/productrating?apiKey=1
        {
            var productRating = productRatingDAL.GetAllProductRatings();
            if (productRating != null)
                return Request.CreateResponse(HttpStatusCode.OK, productRating);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Get(int id)//https://localhost:44378/api/productrating/get/1?apiKey=1
        {
            var productRating = productRatingDAL.GetProductRatingsById(id);
            if (productRating != null)
                return Request.CreateResponse(HttpStatusCode.OK, productRatingDAL.GetProductRatingsById(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage GetAvarage(int categoryId)//https://localhost:44378/api/productrating/getavarage/1?apiKey=1
        {
            var productRating = productRatingDAL.GetProductRatingsByCategoryIdAverage(categoryId);
            if (productRating != null)
                return Request.CreateResponse(HttpStatusCode.OK, productRatingDAL.GetProductRatingsByCategoryIdAverage(categoryId));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Post(ProductRating productRating)//https://localhost:44378/api/productrating?apiKey=1 ---> Content: {"point_value":"10", "product_id":"1", "user_id":"3"}
        {
            //validation kurallarını sağlamıyorsa
            if (ModelState.IsValid)
            {
                var createdProductRating = productRatingDAL.CreateProductRating(productRating);
                return Request.CreateResponse(HttpStatusCode.Created, createdProductRating);
            }
            //OK
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int id, ProductRating productRating)//https://localhost:44378/api/productrating/put/3?apiKey=1 ---> Content: {"point_value":"10", "product_id":"1", "user_id":"3"}
        {
            //id ye ait kayıt yoksa
            if (!productRatingDAL.IsThereAnyProductRating(id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //validation kurallarını sağlamıyorsa
            else if (ModelState.IsValid == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            //OK
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, productRatingDAL.UpdateProductRating(productRating));
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)//https://localhost:44378/api/productrating/delete/3?apiKey=1
        {
            //id ye ait kayıt yoksa
            if (productRatingDAL.IsThereAnyProductRating(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //OK
            else
            {
                productRatingDAL.DeleteProductRating(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }

    }
}
