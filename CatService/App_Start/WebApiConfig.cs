﻿using System.Web.Http;
using CatService.Filters;
using Microsoft.Owin.Security.OAuth;
using MultipartDataMediaFormatter;

namespace CatService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Filters.Add(new ApiCatAuthorizationFilterAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			config.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter());
        }
    }
}
