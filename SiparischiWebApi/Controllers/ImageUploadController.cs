using SiparischiWebApi.Data_Access_Layer;
using SiparischiWebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace SiparischiWebApi.Controllers
{
    public class ImageUploadController : ApiController
    {
        [Route("api/ImageUpload/UploadImageCategory")]
        [HttpPost]
        public HttpResponseMessage UploadImageCategory(string id)
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Fetch the File Name.
            string fileName = Path.GetFileName(postedFile.FileName);

            //Save the File.
            postedFile.SaveAs(path + fileName);

            string constr = ConfigurationManager.ConnectionStrings["webapi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("update category set image_url=@image_url where id=@id", con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@image_url", "https://crealsoft.com/Uploads/" + fileName);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                //Send OK Response to Client.
                return Request.CreateResponse(HttpStatusCode.OK, fileName);
            }
        }

        [Route("api/ImageUpload/UploadImageProduct")]
        [HttpPost]
        public HttpResponseMessage UploadImageProduct(string id)
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Fetch the File Name.
            string fileName = Path.GetFileName(postedFile.FileName);

            //Save the File.
            postedFile.SaveAs(path + fileName);

            string constr = ConfigurationManager.ConnectionStrings["webapi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("update product set image_url=@image_url where id=@id", con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@image_url", "https://crealsoft.com/Uploads/" + fileName);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                //Send OK Response to Client.
                return Request.CreateResponse(HttpStatusCode.OK, fileName);
            }
        }

        [Route("api/ImageUpload/UploadImageBusiness")]
        [HttpPost]
        public HttpResponseMessage UploadImageBusiness(string id)
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Fetch the File Name.
            string fileName = Path.GetFileName(postedFile.FileName);

            //Save the File.
            postedFile.SaveAs(path + fileName);

            string constr = ConfigurationManager.ConnectionStrings["webapi"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("update business set image_url=@image_url where id=@id", con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@image_url", "https://crealsoft.com/Uploads/" + fileName);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                //Send OK Response to Client.
                return Request.CreateResponse(HttpStatusCode.OK, fileName);
            }
        }

        [HttpPost]
        [Route("api/ImageUpload/GetFiles")]
        public HttpResponseMessage GetFiles()
        {
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");

            //Fetch the Image Files.
            List<string> images = new List<string>();

            //Extract only the File Names to save data.
            foreach (string file in Directory.GetFiles(path))
            {
                images.Add(Path.GetFileName(file));
            }

            return Request.CreateResponse(HttpStatusCode.OK, images);
        }
    }
}
