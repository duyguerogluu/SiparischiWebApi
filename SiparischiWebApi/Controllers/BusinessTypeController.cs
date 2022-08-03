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
    public class BusinessTypeController : ApiController
    {
        BusinessTypeDAL businessTypeDAL = new BusinessTypeDAL();

        [Authorize]
        public HttpResponseMessage Get()//https://localhost:44378/api/businesstype?apiKey=1
        {
            var businessType = businessTypeDAL.GetAllBusinessTypes();
            if (businessType != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessType);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Get(int id)//https://localhost:44378/api/businesstype/get/1?apiKey=1
        {
            var businessType = businessTypeDAL.GetBusinessTypesById(id);
            if (businessType != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessTypeDAL.GetBusinessTypesById(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Post(BusinessType businessType)//https://localhost:44378/api/businesstype?apiKey=1 ---> Content: {"type_name":"Fırın"}
        {
            //validation kurallarını sağlamıyorsa
            if (ModelState.IsValid)
            {
                var createdBusinessType = businessTypeDAL.CreateBusinessType(businessType);
                return Request.CreateResponse(HttpStatusCode.Created, createdBusinessType);
            }
            //OK
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int id, BusinessType businessType)//https://localhost:44378/api/businesstype/put/3?apiKey=1 ---> Content: {"type_name":"Fırın"}
        {
            //id ye ait kayıt yoksa
            if (!businessTypeDAL.IsThereAnyBusinessType(id))
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
                return Request.CreateResponse(HttpStatusCode.OK, businessTypeDAL.UpdateBusinessType(businessType));
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)//https://localhost:44378/api/businesstype/delete/3?apiKey=1
        {
            //id ye ait kayıt yoksa
            if (businessTypeDAL.IsThereAnyBusinessType(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //OK
            else
            {
                businessTypeDAL.DeleteBusinessType(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }


    }
}
