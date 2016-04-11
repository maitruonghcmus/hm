using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace HM.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
               name: "ActionApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "ApiById",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional },
            //    constraints: new { id = @"^[0-9]+$" }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "ApiByName",
            //    routeTemplate: "api/{controller}/{action}/{name}",
            //    defaults: null,
            //    constraints: new { name = @"^[a-z]+$" }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "ApiByAction",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    //defaults: new { action = "Get" }
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
