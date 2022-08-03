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
    public class BusinessHourController : ApiController
    {
        BusinessHourDAL businessHourDAL = new BusinessHourDAL();

        [Authorize]
        public HttpResponseMessage Get()//https://localhost:44378/api/businesshour?apiKey=1
        {
            var businesshour = businessHourDAL.GetAllBusinessHours();
            if (businesshour != null)
                return Request.CreateResponse(HttpStatusCode.OK, businesshour);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Get(int id)//https://localhost:44378/api/businesshour/get/1?apiKey=1
        {
            var businesshour = businessHourDAL.GetBusinessHoursById(id);
            if (businesshour != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessHourDAL.GetBusinessHoursById(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Post(BusinessHours businessHour)//https://localhost:44378/api/businesshour?apiKey=1 ---> Content: {"opening_hour":"08:00:00", "closing_hour":"16:00:00", "business_id":"1", "business_work_type_id":"1"}
        {
            //validation kurallarını sağlamıyorsa
            if (ModelState.IsValid)
            {
                var createdBusinessHour = businessHourDAL.CreateBusinessHour(businessHour);
                return Request.CreateResponse(HttpStatusCode.Created, createdBusinessHour);
            }
            //OK
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int id, BusinessHours businessHour)//https://localhost:44378/api/businesshour/put/3?apiKey=1 ---> Content: {"opening_hour":"08:00:00", "closing_hour":"16:00:00", "business_id":"1", "business_work_type_id":"1"}
        {
            //id ye ait kayıt yoksa
            if (!businessHourDAL.IsThereAnyBusinessHour(id))
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
                return Request.CreateResponse(HttpStatusCode.OK, businessHourDAL.UpdateBusinessHour(businessHour));
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)//https://localhost:44378/api/businesshour/delete/3?apiKey=1
        {
            //id ye ait kayıt yoksa
            if (businessHourDAL.IsThereAnyBusinessHour(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //OK
            else
            {
                businessHourDAL.DeleteBusinessHour(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }
    }
}
