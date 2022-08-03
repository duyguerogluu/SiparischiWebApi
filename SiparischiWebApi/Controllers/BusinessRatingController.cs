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
    public class BusinessRatingController : ApiController
    {
        BusinessRatingDAL businessRatingDAL = new BusinessRatingDAL();

        [Authorize]
        public HttpResponseMessage Get()//https://localhost:44378/api/businessrating?apiKey=1
        {
            var businessRating = businessRatingDAL.GetAllBusinessRatings();
            if (businessRating != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessRating);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Get(int id)//https://localhost:44378/api/businessrating/get/1?apiKey=1
        {
            var businessRating = businessRatingDAL.GetBusinessRatingsById(id);
            if (businessRating != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessRatingDAL.GetBusinessRatingsById(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage GetAvarage(int businessId)//https://localhost:44378/api/businessrating/getavarage/1?apiKey=1
        {
            var businessRating = businessRatingDAL.GetBusinessRatingsByBusinessIdAverage(businessId);
            if (businessRating != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessRatingDAL.GetBusinessRatingsByBusinessIdAverage(businessId));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage GetAvarageCount(int businessId)//https://localhost:44378/api/businessrating/getavaragecount/1?apiKey=1
        {
            var businessRating = businessRatingDAL.GetBusinessRatingsByBusinessIdAverageCount(businessId);
            if (businessRating != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessRatingDAL.GetBusinessRatingsByBusinessIdAverageCount(businessId));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Post(BusinessRating businessRating)//https://localhost:44378/api/businessrating?apiKey=1 ---> Content: {"point_value":"10", "business_id":"1", "user_id":"3", "business_work_type_id":"1"}
        {
            //validation kurallarını sağlamıyorsa
            if (ModelState.IsValid)
            {
                var createdBusinessRating = businessRatingDAL.CreateBusinessRating(businessRating);
                return Request.CreateResponse(HttpStatusCode.Created, createdBusinessRating);
            }
            //OK
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int id, BusinessRating businessRating)//https://localhost:44378/api/businessrating/put/3?apiKey=1 ---> Content: {"point_value":"10", "business_id":"1", "user_id":"3", "business_work_type_id":"1"}
        {
            //id ye ait kayıt yoksa
            if (!businessRatingDAL.IsThereAnyBusinessRating(id))
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
                return Request.CreateResponse(HttpStatusCode.OK, businessRatingDAL.UpdateBusinessRating(businessRating));
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)//https://localhost:44378/api/businessrating/delete/3?apiKey=1
        {
            //id ye ait kayıt yoksa
            if (businessRatingDAL.IsThereAnyBusinessRating(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //OK
            else
            {
                businessRatingDAL.DeleteBusinessRating(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }


    }
}
