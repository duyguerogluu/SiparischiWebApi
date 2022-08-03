using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SiparischiWebApi.Models;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class CampaignDAL : BaseDAL
    {
        public IEnumerable<Campaign> GetAllCampaigns()
        {
            return db.Campaign.ToList();
        }

        public IEnumerable<Campaign> GetCampaignsById(int id)
        {
            return db.Campaign.Where(x => x.id == id).ToList();
        }

        public Campaign CreateCampaign(Campaign campaign)
        {
            db.Campaign.Add(campaign);
            db.SaveChanges();
            return campaign;
        }

        public Campaign UpdateCampaign(Campaign campaign)
        {
            db.Entry(campaign).State = EntityState.Modified;
            db.SaveChanges();
            return campaign;
        }

        public void DeleteCampaign(int id)
        {
            db.Campaign.Remove(db.Campaign.Find(id));
            db.SaveChanges();
        }

        public bool IsThereAnyCampaign(int id)
        {
            return db.Campaign.Any(x => x.id == id);
        }
    }
}