using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SiparischiWebApi.Models;
using SiparischiWebApi.Data_Access_Layer;

namespace SiparischiWebApi.Controllers
{
    public class UserAddressController : ApiController
    {
        UserAddressDAL userAddressDAL = new UserAddressDAL();

        [Authorize]
        public HttpResponseMessage Get()//https://localhost:44378/api/useraddress?apiKey=1
        {
            var userAddress = userAddressDAL.GetAllUserAddresses();
            if (userAddress != null)
                return Request.CreateResponse(HttpStatusCode.OK, userAddress);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Get(int id)//https://localhost:44378/api/useraddress/get/1?apiKey=1
        {
            var userAddress = userAddressDAL.GetUserAddressesById(id);
            if (userAddress != null)
                return Request.CreateResponse(HttpStatusCode.OK, userAddressDAL.GetUserAddressesById(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage GetSelectedAddress(int id)//https://localhost:44378/api/useraddress/getselectedaddress/1?apiKey=1
        {
            var userSelectedAddress = userAddressDAL.GetUserSelectedAddress(id);
            if (userSelectedAddress != null)
                return Request.CreateResponse(HttpStatusCode.OK, userAddressDAL.GetUserSelectedAddress(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Post(UserAddress userAddress)//https://localhost:44378/api/useraddress?apiKey=1 ---> Content: {"address_name":"İşyeri", "address_content":"Mersin Toroslar Mithattoroğlu Mah.", "user_id":"1"}
        {
            //validation kurallarını sağlamıyorsa
            if (ModelState.IsValid)
            {
                var createdUserAddress = userAddressDAL.CreateUserAddress(userAddress);
                return Request.CreateResponse(HttpStatusCode.Created, createdUserAddress);
            }
            //OK
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int id, UserAddress userAddress)//https://localhost:44378/api/useraddress/put/3?apiKey=1 ---> Content: {"address_name":"İşyeri", "address_content":"Mersin Toroslar Mithattoroğlu Mah.", "user_id":"1"}
        {
            //id ye ait kayıt yoksa
            if (!userAddressDAL.IsThereAnyUserAddress(id))
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
                return Request.CreateResponse(HttpStatusCode.OK, userAddressDAL.UpdateUserAddress(userAddress));
            }
        }

        [Authorize]
        public string GetUpdateSelectedAddress(string user_id, string address_id)//https://localhost:44378/api/updateselectedaddress?user_id=1&address_id=1&apiKey=1 ---> Content: {"address_name":"İşyeri", "address_content":"Mersin Toroslar Mithattoroğlu Mah.", "user_id":"1"}
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
                            SqlDataAdapter FDataAdapter = new SqlDataAdapter(string.Format("select address_name from useraddress where id=" + address_id), FDataConnect);
                            DataTable dataTable = new DataTable();
                            FDataAdapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand("update useraddress set selected_address=0 where user_id=@user_id", con))
                                {
                                    cmd.Parameters.AddWithValue("@user_id", user_id);
                                    int i = cmd.ExecuteNonQuery();
                                }
                                using (SqlCommand cmd = new SqlCommand("update useraddress set selected_address=1 where user_id=" + user_id + " and id=" + address_id, con))
                                {
                                    int i = cmd.ExecuteNonQuery();
                                    con.Close();
                                    if (i == 1)
                                        return "Kullanılan adres değiştirildi";
                                    else
                                        return "Kullanılan adres değiştirilemedi";
                                }
                            }
                            else
                            {
                                return dataTable.Rows[0].ItemArray[0].ToString() + " adresi bulunamadı";
                            }
                        }
                        catch (Exception)
                        {
                            return "İşlem başarısız";
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
        public HttpResponseMessage Delete(int id)//https://localhost:44378/api/useraddress/delete/3?apiKey=1
        {
            //id ye ait kayıt yoksa
            if (userAddressDAL.IsThereAnyUserAddress(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //OK
            else
            {
                userAddressDAL.DeleteUserAddress(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }
    }
}
