using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SiparischiWebApi.Data_Access_Layer;

namespace SiparischiWebApi.Security
{
    public class APIKeyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var queryString = request.RequestUri.ParseQueryString();
            var apiKey = queryString["apiKey"];
            AdminDAL adminDAL = new AdminDAL();
            UserDAL userDAL = new UserDAL();
            BusinessDAL businessDAL = new BusinessDAL();
            var admin = adminDAL.GetAdminByApiKey(apiKey);
            var user = userDAL.GetUserByApiKey(apiKey);
            var business = businessDAL.GetBusinessByApiKey(apiKey);
            if (admin != null)
            {
                var principal = new ClaimsPrincipal(new GenericIdentity(admin.username, "APIKey"));
                HttpContext.Current.User = principal;
            }
            if (user != null)
            {
                var principal = new ClaimsPrincipal(new GenericIdentity(user.username, "APIKey"));
                HttpContext.Current.User = principal;
            }
            if (business != null)
            {
                var principal = new ClaimsPrincipal(new GenericIdentity(business.username, "APIKey"));
                HttpContext.Current.User = principal;
            }
            return base.SendAsync(request, cancellationToken);
        }

    }
}