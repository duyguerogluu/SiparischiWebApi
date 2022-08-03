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
    public class CampaignController : ApiController
    {
        CampaignDAL campaignDAL = new CampaignDAL();

        [Authorize]
        public HttpResponseMessage Get()//https://localhost:44378/api/campaign?apiKey=1
        {
            var campaign = campaignDAL.GetAllCampaigns();
            if (campaign != null)
                return Request.CreateResponse(HttpStatusCode.OK, campaign);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Get(int id)//https://localhost:44378/api/campaign/get/1?apiKey=1
        {
            var campaign = campaignDAL.GetCampaignsById(id);
            if (campaign != null)
                return Request.CreateResponse(HttpStatusCode.OK, campaignDAL.GetCampaignsById(id));
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
        }

        [Authorize]
        public HttpResponseMessage Post(Campaign campaign)//https://localhost:44378/api/campaign?apiKey=1 ---> Content: {"campaign_name":"İkiliMenü", "starting_date":"11.11.2022", "ending_date":"11.12.2022", "business_id":"2"}
        {
            //validation kurallarını sağlamıyorsa
            if (ModelState.IsValid)
            {
                var createdCampaign = campaignDAL.CreateCampaign(campaign);
                return Request.CreateResponse(HttpStatusCode.Created, createdCampaign);
            }
            //OK
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        public HttpResponseMessage Put(int id, Campaign campaign)//https://localhost:44378/api/campaign/put/3?apiKey=1 ---> Content: {"campaign_name":"İkiliMenü", "starting_date":"11.11.2022", "ending_date":"11.12.2022", "business_id":"2", "business_work_type_id":"1"}
        {
            //id ye ait kayıt yoksa
            if (!campaignDAL.IsThereAnyCampaign(id))
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
                return Request.CreateResponse(HttpStatusCode.OK, campaignDAL.UpdateCampaign(campaign));
            }
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)//https://localhost:44378/api/campaign/delete/3?apiKey=1
        {
            //id ye ait kayıt yoksa
            if (campaignDAL.IsThereAnyCampaign(id) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıt bulunamadı");
            }
            //OK
            else
            {
                campaignDAL.DeleteCampaign(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }

    }
}
