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
    public class OrderTypeController : ApiController
    {
        OrderTypeDAL orderTypeDAL = new OrderTypeDAL();

        [Authorize]
        public HttpResponseMessage Get()//https://localhost:44378/api/ordertype?apiKey=1
        {
            var ordertype = orderTypeDAL.GetAllOrderTypes();
            if (ordertype != null)
                return Request.CreateResponse(HttpStatusCode.OK, ordertype);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Get(int id)//https://localhost:44378/api/ordertype/get/1?apiKey=1
        {
            var ordertype = orderTypeDAL.GetOrderTypesById(id);
            if (ordertype != null)
                return Request.CreateResponse(HttpStatusCode.OK, orderTypeDAL.GetOrderTypesById(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Post(OrderType orderType)//https://localhost:44378/api/ordertype?apiKey=1 ---> Content: {"order_type":"Kapıda Odeme"}
        {
            //validation kurallarını sağlamıyorsa
            if (ModelState.IsValid)
            {
                var createdOrderType = orderTypeDAL.CreateOrderType(orderType);
                return Request.CreateResponse(HttpStatusCode.Created, createdOrderType);
            }
            //OK
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int id, OrderType orderType)//https://localhost:44378/api/ordertype/put/3?apiKey=1 ---> Content: {"order_type":"Kapıda Odeme"}
        {
            //id ye ait kayıt yoksa
            if (!orderTypeDAL.IsThereAnyOrderType(id))
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
                return Request.CreateResponse(HttpStatusCode.OK, orderTypeDAL.UpdateOrderType(orderType));
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)//https://localhost:44378/api/ordertype/delete/3?apiKey=1
        {
            //id ye ait kayıt yoksa
            if (orderTypeDAL.IsThereAnyOrderType(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //OK
            else
            {
                orderTypeDAL.DeleteOrderType(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }

    }
}
