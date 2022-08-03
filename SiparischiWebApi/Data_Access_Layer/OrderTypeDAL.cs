using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class OrderTypeDAL : BaseDAL
    {
        public IEnumerable<OrderType> GetAllOrderTypes()
        {
            return db.OrderType.ToList();
        }

        public IEnumerable<OrderType> GetOrderTypesById(int id)
        {
            return db.OrderType.Where(x => x.id == id).ToList();
        }

        public OrderType CreateOrderType(OrderType orderType)
        {
            db.OrderType.Add(orderType);
            db.SaveChanges();
            return orderType;
        }

        public OrderType UpdateOrderType(OrderType orderType)
        {
            db.Entry(orderType).State = EntityState.Modified;
            db.SaveChanges();
            return orderType;
        }

        public void DeleteOrderType(int id)
        {
            db.OrderType.Remove(db.OrderType.Find(id));
            db.SaveChanges();
        }

        public bool IsThereAnyOrderType(int id)
        {
            return db.OrderType.Any(x => x.id == id);
        }
    }
}