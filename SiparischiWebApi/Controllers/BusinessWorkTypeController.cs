using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using SiparischiWebApi.Data_Access_Layer;
using SiparischiWebApi.Models;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace SiparischiWebApi.Controllers
{
    public class BusinessWorkTypeController : ApiController
    {
        BusinessWorkTypeDAL businessWorkTypeDAL = new BusinessWorkTypeDAL();

        [Authorize]
        public HttpResponseMessage Get()//https://localhost:44378/api/BusinessWorkType?apiKey=1
        {
            var businessworktype = businessWorkTypeDAL.GetAllBusinessWorkTypes();
            if (businessworktype != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessworktype);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Get(int id)//https://localhost:44378/api/BusinessWorkType/get/1?apiKey=1
        {
            var businessworktype = businessWorkTypeDAL.GetBusinessWorkTypesById(id);
            if (businessworktype != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessWorkTypeDAL.GetBusinessWorkTypesById(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Post(BusinessWorkType businessWorkType)//https://localhost:44378/api/BusinessWorkType?apiKey=1 ---> Content: {"work_type":"Hergün"}
        {
            //validation kurallarını sağlamıyorsa
            if (ModelState.IsValid)
            {
                var createdBusinessWorkType = businessWorkTypeDAL.CreateBusinessWorkType(businessWorkType);
                return Request.CreateResponse(HttpStatusCode.Created, createdBusinessWorkType);
            }
            //OK
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int id, BusinessWorkType businessWorkType)//https://localhost:44378/api/businessworktype/put/3?apiKey=1 ---> Content: {"work_type":"Hergün"}
        {
            //id ye ait kayıt yoksa
            if (!businessWorkTypeDAL.IsThereAnyBusinessWorkType(id))
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
                return Request.CreateResponse(HttpStatusCode.OK, businessWorkTypeDAL.UpdateBusinessWorkType(businessWorkType));
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)//https://localhost:44378/api/businessworktype/delete/3?apiKey=1
        {
            //id ye ait kayıt yoksa
            if (businessWorkTypeDAL.IsThereAnyBusinessWorkType(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //OK
            else
            {
                businessWorkTypeDAL.DeleteBusinessWorkType(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }
    }
}
