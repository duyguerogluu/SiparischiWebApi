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
    public class ProductController : ApiController
    {
        ProductDAL productDAL = new ProductDAL();

        [Authorize]
        public HttpResponseMessage Get()//https://localhost:44378/api/product?apiKey=1
        {
            var product = productDAL.GetAllProducts();
            if (product != null)
                return Request.CreateResponse(HttpStatusCode.OK, product);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Get(int id)//https://localhost:44378/api/product/get/1?apiKey=1
        {
            var product = productDAL.GetProductsById(id);
            if (product != null)
                return Request.CreateResponse(HttpStatusCode.OK, productDAL.GetProductsById(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage GetCategoryProduct(int categoryId)//https://localhost:44378/api/category/getcategoryproduct/1?apiKey=1
        {
            var product = productDAL.GetProductsByCategoryId(categoryId);
            if (product != null)
                return Request.CreateResponse(HttpStatusCode.OK, productDAL.GetProductsByCategoryId(categoryId));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Post(Product product)//https://localhost:44378/api/product?apiKey=1 ---> Content: {"product_name":"BüyükPizza", "image_name":"pizza.jpeg", "detail":"4-6 Kişilik", "price":"80", "discounted_price":"70", "campaign_status":"1", "creation_date":"01.01.2022", "status":"Aktif", "category_id":"1"}
        {
            //validation kurallarını sağlamıyorsa
            if (ModelState.IsValid)
            {
                var createdProduct = productDAL.CreateProduct(product);
                return Request.CreateResponse(HttpStatusCode.Created, createdProduct);
            }
            //OK
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int id, Product product)//https://localhost:44378/api/product/put/3?apiKey=1 ---> Content: {"product_name":"BüyükPizza", "image_name":"pizza.jpeg", "detail":"4-6 Kişilik", "price":"80", "discounted_price":"70", "campaign_status":"1", "creation_date":"01.01.2022", "status":"Aktif", "category_id":"1"}
        {
            //id ye ait kayıt yoksa
            if (!productDAL.IsThereAnyProduct(id))
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
                return Request.CreateResponse(HttpStatusCode.OK, productDAL.UpdateProduct(product));
            }
        }

        [Authorize]
        public string PutStatus(int id, String status)//https://localhost:44378/api/product/putstatus/3?apiKey=1 ---> Content: {"product_name":"BüyükPizza", "image_name":"pizza.jpeg", "detail":"4-6 Kişilik", "price":"80", "discounted_price":"70", "campaign_status":"1", "creation_date":"01.01.2022", "status":"Aktif", "category_id":"1"}
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
                            SqlDataAdapter FDataAdapter = new SqlDataAdapter(string.Format("select product_name from product where id=" + id), FDataConnect);
                            DataTable dataTable = new DataTable();
                            FDataAdapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                    using (SqlCommand cmd = new SqlCommand("update product set status=@status where id=@id", con))
                                    {
                                        cmd.Parameters.AddWithValue("@id", id);
                                    cmd.Parameters.AddWithValue("@status", status);
                                    int i = cmd.ExecuteNonQuery();
                                        con.Close();
                                        if (i == 1)
                                            return "Ürün silindi";
                                        else
                                            return "Ürün silinemedi";
                                    }
                            }
                            else
                            {
                                return dataTable.Rows[0].ItemArray[0].ToString() + " ürünü bulunamadı";
                            }
                        }
                        catch (Exception)
                        {
                            return "Ürün silinemedi";
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
        public HttpResponseMessage Delete(int id)//https://localhost:44378/api/product/delete/3?apiKey=1
        {
            //id ye ait kayıt yoksa
            if (productDAL.IsThereAnyProduct(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //OK
            else
            {
                productDAL.DeleteProduct(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }
    }
}
