using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class ProductDAL : BaseDAL
    {
        public IEnumerable<Product> GetAllProducts()
        {
            return db.Product.Where(x => x.status == "Aktif").ToList();
        }

        public IEnumerable<Product> GetProductsById(int id)
        {
            return db.Product.Where(x => x.id == id).ToList();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return db.Product.Where(x => x.category_id == categoryId && x.status == "Aktif").ToList();
        }

        public Product CreateProduct(Product product)
        {
            db.Product.Add(product);
            db.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return product;
        }

        public void DeleteProduct(int id)
        {
            db.Product.Remove(db.Product.Find(id));
            db.SaveChanges();
        }

        public bool IsThereAnyProduct(int id)
        {
            return db.Product.Any(x => x.id == id);
        }
    }
}