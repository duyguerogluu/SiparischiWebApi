using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class CategoryDAL : BaseDAL
    {
        public IEnumerable<Category> GetAllCategories()
        {
            return db.Category.Where(x => x.status == "Aktif").ToList();
        }

        public IEnumerable<Category> GetCategoriesById(int id)
        {
            return db.Category.Where(x => x.id == id).ToList();
        }

        public IEnumerable<Category> GetCategoriesByBusinessId(int businessId)
        {
            return db.Category.Where(x => x.business_id == businessId && x.status == "Aktif").ToList();
        }

        public Category CreateCategory(Category category)
        {
            db.Category.Add(category);
            db.SaveChanges();
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return category;
        }

        public void DeleteCategory(int id)
        {
            db.Category.Remove(db.Category.Find(id));
            db.SaveChanges();
        }

        public bool IsThereAnyCategory(int id)
        {
            return db.Category.Any(x => x.id == id);
        }

    }
}