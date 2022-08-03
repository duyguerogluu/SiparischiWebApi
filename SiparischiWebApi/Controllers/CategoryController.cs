using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SiparischiWebApi.Models;
using SiparischiWebApi.Data_Access_Layer;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SiparischiWebApi.Controllers
{
    public class CategoryController : ApiController
    {
        CategoryDAL categoryDAL = new CategoryDAL();

        [Authorize]
        public HttpResponseMessage Get()//https://localhost:44378/api/category?apiKey=1
        {
            var category = categoryDAL.GetAllCategories();
            if (category != null)
                return Request.CreateResponse(HttpStatusCode.OK, category);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Get(int id)//https://localhost:44378/api/category/get/1?apiKey=1
        {
            var category = categoryDAL.GetCategoriesById(id);
            if (category != null)
                return Request.CreateResponse(HttpStatusCode.OK, categoryDAL.GetCategoriesById(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage GetBusinessCategory(int businessId)//https://localhost:44378/api/category/getbusinesscategory/1?apiKey=1
        {
            var category = categoryDAL.GetCategoriesByBusinessId(businessId);
            if (category != null)
                return Request.CreateResponse(HttpStatusCode.OK, categoryDAL.GetCategoriesByBusinessId(businessId));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Post(Category category)//https://localhost:44378/api/category?apiKey=1 ---> Content: {"category_name":"Pizzalar", "image_name":"pizza.jpeg", "status":"Aktif", "creation_date":"1", "business_id":"1"}
        {
            //validation kurallarını sağlamıyorsa
            if (ModelState.IsValid)
            {
                var createdCategory = categoryDAL.CreateCategory(category);
                return Request.CreateResponse(HttpStatusCode.Created, createdCategory);
            }
            //OK
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int id, Category category)//https://localhost:44378/api/category/put/3?apiKey=1 ---> Content: {"category_name":"Pizzalar", "image_name":"pizza.jpeg", "status":"Aktif", "creation_date":"1", "business_id":"1"}
        {
            //id ye ait kayıt yoksa
            if (!categoryDAL.IsThereAnyCategory(id))
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
                return Request.CreateResponse(HttpStatusCode.OK, categoryDAL.UpdateCategory(category));
            }
        }

        [Authorize]
        public string GetCategoryId(String category_name, String business_id)//https://localhost:44378/api/category/getcategoryid?category_name=Yemekler&business_id=1&apiKey=1
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["webapi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    {
                        try
                        {
                            SqlConnection FDataConnect = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi"].ToString());
                            FDataConnect.Open();
                            SqlDataAdapter FDataAdapter = new SqlDataAdapter(string.Format("select id from category where category_name='" + category_name + "' and business_id=" + business_id + " and status='Aktif'"), FDataConnect);
                            DataTable dataTable = new DataTable();
                            FDataAdapter.Fill(dataTable);
                            if(dataTable.Rows.Count > 0)
                            {
                                return dataTable.Rows[0].ItemArray[0].ToString();
                            }
                        }
                        catch (Exception)
                        {
                            return "0";
                        }
                    }
                }
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }

        [Authorize]
        public string PutStatus(int id, String status)//https://localhost:44378/api/category/putstatus/3?apiKey=1 ---> Content: {"product_name":"BüyükPizza", "image_name":"pizza.jpeg", "detail":"4-6 Kişilik", "price":"80", "discounted_price":"70", "campaign_status":"1", "creation_date":"01.01.2022", "status":"Aktif", "category_id":"1"}
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["webapi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    {
                        try
                        {
                            SqlConnection FDataConnect = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi"].ToString());
                            FDataConnect.Open();
                            SqlDataAdapter FDataAdapter = new SqlDataAdapter(string.Format("select category_name from category where id=" + id), FDataConnect);
                            DataTable dataTable = new DataTable();
                            FDataAdapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand("update category set status=@status where id=@id", con))
                                {
                                    cmd.Parameters.AddWithValue("@id", id);
                                    cmd.Parameters.AddWithValue("@status", status);
                                    int i = cmd.ExecuteNonQuery();
                                    con.Close();
                                    if (i == 1)
                                        return "Kategori silindi";
                                    else
                                        return "Kategori silinemedi";
                                }
                            }
                            else
                            {
                                return dataTable.Rows[0].ItemArray[0].ToString() + " kategorisi bulunamadı";
                            }
                        }
                        catch (Exception)
                        {
                            return "Kategori silinemedi";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "İşlem başarısız";
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)//https://localhost:44378/api/category/delete/3?apiKey=1
        {
            //id ye ait kayıt yoksa
            if (categoryDAL.IsThereAnyCategory(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //OK
            else
            {
                categoryDAL.DeleteCategory(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }

    }
}
