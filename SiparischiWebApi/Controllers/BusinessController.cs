using SiparischiWebApi.Data_Access_Layer;
using SiparischiWebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace SiparischiWebApi.Controllers
{
    public class BusinessController : ApiController
    {
        BusinessDAL businessDAL = new BusinessDAL();
        SiparischiEntities db = new SiparischiEntities();

        public string GetLogin(string username, string password)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["webapi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    {
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select id, business_key from business where " +
                            "(username='" + username + "') and " +
                            "(password='" + password + "')", con);
                        DataTable dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            return dataTable.Rows[0].ItemArray[0].ToString() + "-" + dataTable.Rows[0].ItemArray[1].ToString();
                        }
                        else
                        {
                            return "0";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public string GetRegister(string id, string username, string password, string business_name, string status, string phone_number, string email, string city, string district, string neighbourhood, bool situation, string starting_date, string ending_date, string image_url, string location, string business_type_id)
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
                                if (id == "0")
                                {
                                    try
                                    {
                                        SqlConnection FDataConnect = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi"].ToString());
                                        FDataConnect.Open();
                                        SqlDataAdapter FDataAdapter = new SqlDataAdapter(string.Format("select username from business where rtrim(username) = '" + username.Trim() + "'"), FDataConnect);
                                        DataTable dataTable = new DataTable();
                                        FDataAdapter.Fill(dataTable);
                                        if (dataTable.Rows.Count > 0)
                                        {
                                            return dataTable.Rows[0].ItemArray[0].ToString().Trim() + " kullanıcı adı alındı...";
                                        }
                                        else
                                        {
                                        String APIKey;
                                        using (var cryptoProvider = new RNGCryptoServiceProvider())
                                        {
                                            byte[] secretKeyByteArray = new byte[32];
                                            cryptoProvider.GetBytes(secretKeyByteArray);
                                            APIKey = Convert.ToBase64String(secretKeyByteArray);
                                        }
                                        using (SqlCommand cmd = new SqlCommand("insert into business (username,password,business_name,status,phone_number,email,city,district,neighbourhood,situation,starting_date,ending_date,image_url,location,business_type_id,business_key) values (@username,@password,@business_name,@status,@phone_number,@email,@city,@district,@neighbourhood,@situation,@starting_date,@ending_date,@image_name,@location,@business_type_id,@business_key)", con))
                                        {
                                            cmd.Parameters.AddWithValue("@username", username);
                                            cmd.Parameters.AddWithValue("@password", password);
                                            cmd.Parameters.AddWithValue("@business_name", business_name);
                                            cmd.Parameters.AddWithValue("@status", status);
                                            cmd.Parameters.AddWithValue("@phone_number", phone_number);
                                            cmd.Parameters.AddWithValue("@email", email);
                                            cmd.Parameters.AddWithValue("@city", city);
                                            cmd.Parameters.AddWithValue("@district", district);
                                            cmd.Parameters.AddWithValue("@neighbourhood", neighbourhood);
                                            cmd.Parameters.AddWithValue("@situation", situation);
                                            cmd.Parameters.AddWithValue("@starting_date", starting_date);
                                            cmd.Parameters.AddWithValue("@ending_date", ending_date);
                                            cmd.Parameters.AddWithValue("@image_url", image_url);
                                            cmd.Parameters.AddWithValue("@location", location);
                                            cmd.Parameters.AddWithValue("@business_type_id", business_type_id);
                                            cmd.Parameters.AddWithValue("@business_key", APIKey);
                                            int i = cmd.ExecuteNonQuery();
                                            con.Close();
                                            if (i == 1)
                                                return "Kayıt eklendi";
                                            else
                                                return "Kayıt eklenemedi";
                                        }
                                    }
                                }
                                    catch (Exception)
                                    {
                                        return "Kayıt eklenemedi";
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        using (SqlCommand cmd = new SqlCommand("update business set username=@username, password=@password, business_name=@business_name, status=@status, phone_number=@phone_number, email=@email, city=@city, district=@district, neighbourhood=@neighbourhood, situation=@situation, starting_date=@starting_date, ending_date=@ending_date, image_url=@image_url, location=@location, business_type_id=@business_type_id  where id=@id", con))
                                        {
                                            cmd.Parameters.AddWithValue("@id", id);
                                            cmd.Parameters.AddWithValue("@username", username);
                                            cmd.Parameters.AddWithValue("@password", password);
                                            cmd.Parameters.AddWithValue("@business_name", business_name);
                                            cmd.Parameters.AddWithValue("@status", status);
                                            cmd.Parameters.AddWithValue("@phone_number", phone_number);
                                            cmd.Parameters.AddWithValue("@email", email);
                                            cmd.Parameters.AddWithValue("@city", city);
                                            cmd.Parameters.AddWithValue("@district", district);
                                            cmd.Parameters.AddWithValue("@neighbourhood", neighbourhood);
                                            cmd.Parameters.AddWithValue("@situation", situation);
                                            cmd.Parameters.AddWithValue("@starting_date", starting_date);
                                            cmd.Parameters.AddWithValue("@ending_date", ending_date);
                                            cmd.Parameters.AddWithValue("@image_url", image_url);
                                            cmd.Parameters.AddWithValue("@location", location);
                                            cmd.Parameters.AddWithValue("@business_type_id", business_type_id);
                                            int i = cmd.ExecuteNonQuery();
                                            con.Close();
                                            if (i == 1)
                                                return "Güncellendi";
                                            else
                                                return "Güncellenemedi";
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        return "Güncellenemedi";
                                    }
                                }
                            
                        }
                        catch (Exception)
                        {
                            return "Hatalı kullanıcı adı girdiniz";
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
        public string GetBusinessOnOff(string id, bool situation)
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
                            SqlDataAdapter FDataAdapter = new SqlDataAdapter(string.Format("select username from business where id = " + id), FDataConnect);
                            DataTable dataTable = new DataTable();
                            FDataAdapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                using (SqlCommand cmd = new SqlCommand("update business set situation=@situation where id=@id", con))
                                {
                                    cmd.Parameters.AddWithValue("@id", id);
                                    cmd.Parameters.AddWithValue("@situation", situation);
                                    int i = cmd.ExecuteNonQuery();
                                    con.Close();
                                    if (i == 1)
                                        if (situation) return "İşletme kullanıma açıldı";
                                        else return "İşletme kullanıma kapatıldı";
                                    else return "0";
                                }
                            }
                            else
                            {
                                return dataTable.Rows[0].ItemArray[0].ToString().Trim() + " kullanıcısı bulunamadı...";
                            }
                        }
                        catch (Exception)
                        {
                            return "0";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public string GetPasswordReset(string id, string oldpassword, string newpassword)
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
                            SqlDataAdapter FDataAdapter = new SqlDataAdapter(string.Format("select password from business where id=" + id), FDataConnect);
                            DataTable dataTable = new DataTable();
                            FDataAdapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                if (dataTable.Rows[0].ItemArray[0].ToString() == oldpassword) //eski şifreyle veri tabanındaki şifre aynı ise
                                {
                                    using (SqlCommand cmd = new SqlCommand("update business set password=@password where id=@id", con))
                                    {
                                        cmd.Parameters.AddWithValue("@id", id);
                                        cmd.Parameters.AddWithValue("@password", newpassword);
                                        int i = cmd.ExecuteNonQuery();
                                        con.Close();
                                        if (i == 1)
                                            return "Şifre güncellendi";
                                        else
                                            return "Şifre güncellenemedi";
                                    }
                                }
                                else
                                {
                                    return "Eski şifrenizi yanlış girdiniz";
                                }
                            }
                            else
                            {
                                return "Bu kullanıcı bulunamadı";
                            }
                        }
                        catch (Exception)
                        {
                            return "Şifre yenilenemedi";
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
        public HttpResponseMessage GetBusiness(int businessId)//https://localhost:44378/api/business/getbusiness?businessId=1&apiKey=1
        {
            var business = businessDAL.GetBusinessById(businessId);
            if (business != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessDAL.GetBusinessById(businessId));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage GetBusinessLocation(string city)//https://localhost:44378/api/business/getbusinesslocation?city=Mersin&apiKey=1
        {
            var result = (from b in db.Business
                          join br in db.BusinessRating on
                          b.id equals br.business_id
                           into rating
                          from r in rating.DefaultIfEmpty()
                          where b.city == city
                          group r by new {
                              b.id,
                              b.username,
                              b.password,
                              b.business_name,
                              b.status,
                              b.phone_number,
                              b.email,
                              b.city,
                              b.district,
                              b.neighbourhood,
                              b.situation,
                              b.starting_date,
                              b.ending_date,
                              b.image_url,
                              b.location,
                              b.business_type_id,
                              b.business_key
                          } into g
                          select new
                          {
                              g.Key.id,
                              g.Key.username,
                              g.Key.password,
                              g.Key.business_name,
                              g.Key.status,
                              g.Key.phone_number,
                              g.Key.email,
                              g.Key.city,
                              g.Key.district,
                              g.Key.neighbourhood,
                              g.Key.situation,
                              g.Key.starting_date,
                              g.Key.ending_date,
                              g.Key.image_url,
                              g.Key.location,
                              g.Key.business_type_id,
                              g.Key.business_key,
                              point_value = g.Average(x => x.point_value),
                              point_count = g.Count(),
                          }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
            /*
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
                            SqlDataAdapter FDataAdapter = new SqlDataAdapter(string.Format(
                            "select b.business_name, avg(br.point_value) point_value, count(br.point_value) point_count from " +
                            "Business b left join BusinessRating br on b.id = br.business_id where b.city = 'Mersin' " +
                            "group by b.id, b.username, b.password, b.business_name, b.status, b.phone_number, b.email, b.city," +
                            "b.district, b.neighbourhood, b.situation, b.starting_date, b.ending_date," +
                            "b.image_url, b.location, b.business_type_id, b.business_key"), FDataConnect);
                            DataTable dataTable = new DataTable();
                            FDataAdapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {

                                return DataTableToJSONWithStringBuilder(dataTable);
                            }
                            else
                            {
                                return "Bu kullanıcı bulunamadı";
                            }
                        }
                        catch (Exception)
                        {
                            return "Şifre yenilenemedi";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "İşlem başarısız";
            }

            var business = businessDAL.GetBusinessLocation(city);
            if (business != null)
                return Request.CreateResponse(HttpStatusCode.OK, businessDAL.GetBusinessLocation(city));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");*/
        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        public HttpResponseMessage Put(int id, Business business)//https://localhost:44378/api/business/put/3?apiKey=1 ---> Content: {"opening_hour":"08:00:00", "closing_hour":"16:00:00", "business_id":"1", "business_work_type_id":"1"}
        {
            //id ye ait kayıt yoksa
            if (!businessDAL.IsThereAnyBusiness(id))
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
                return Request.CreateResponse(HttpStatusCode.OK, businessDAL.UpdateBusiness(business));
            }
        }
    }
}
