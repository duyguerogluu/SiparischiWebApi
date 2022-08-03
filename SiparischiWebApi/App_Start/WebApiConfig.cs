using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SiparischiWebApi.Security;

namespace SiparischiWebApi  
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new APIKeyHandler());
            HttpConfiguration config2 = GlobalConfiguration.Configuration;

            config2.Formatters.JsonFormatter
                        .SerializerSettings
                        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Api",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "IdApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ProductStatusApi",
                routeTemplate: "api/{controller}/{id}/{status}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "GetAvarageBusiness",
                routeTemplate: "api/{controller}/{businessId}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "GetAvarageProduct",
                routeTemplate: "api/{controller}/{productId}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "adminLogin",
                routeTemplate: "api/{controller}/{username}/{password}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "adminRegister",
                routeTemplate: "api/{controller}/{id}/{username}/{password}/{status}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "adminPasswordReset",
                routeTemplate: "api/{controller}/{id}/{oldpassword}/{newpassword}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "businessworktypeList",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "userRegister",
                routeTemplate: "api/{controller}/{id}/{username}/{password}/{status}/{phone_number}/{email}/{address}/{creation_date}/{location}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "bussinessLogin",
                routeTemplate: "api/{controller}/{business_name}/{password}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "businessRegister",
                routeTemplate: "api/{controller}/{id}/{business_name}/{password}/{status}/{phone_number}/{email}/{city}/{district}/{neighbourhood}/{situation}/{starting_date}/{ending_date}/{image_name}/{location}/{business_type_id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "businessPasswordReset",
                routeTemplate: "api/{controller}/{id}/{oldpassword}/{newpassword}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
