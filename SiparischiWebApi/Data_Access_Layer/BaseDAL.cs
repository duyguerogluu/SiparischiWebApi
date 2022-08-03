using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiparischiWebApi.Data_Access_Layer
{
    public class BaseDAL
    {
        protected Models.SiparischiEntities db;
        public BaseDAL()
        {
            db = new Models.SiparischiEntities();
        }
    }
}