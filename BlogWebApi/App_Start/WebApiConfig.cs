using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using BlogWebApi.Filters;
using WebApiContrib.Formatting.Jsonp;

namespace BlogWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Filters.Add(new GlobalExceptionFilterAttribute());

            //Note: Use below global filter to force the web api to use Https only
            //config.Filters.Add(new RequireHttpsFilterAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Blog",
                routeTemplate: "api/blogs/{id}",
                defaults: new { controller = "blogs", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Post",
                routeTemplate: "api/blogs/{blogId}/posts/{id}",
                defaults: new { controller = "posts", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //To support jsonp
            var formatter = new JsonpMediaTypeFormatter(jsonFormatter);
            config.Formatters.Insert(0, formatter);
        }
    }
}
