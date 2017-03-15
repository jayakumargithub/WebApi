using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using WebApi.ModelBinder;
using FluentValidation.WebApi;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            System.Net.Http.Formatting.JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
            JsonSerializerSettings settings = new JsonSerializerSettings();
            //Json indenting    
            settings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //enable fluent api
            //  FluentValidationModelValidatorProvider.Configure(config); 
           

            //Note:  to avoid object reference preserved error
            // jsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;

           // config.MessageHandlers.Add(new ResponseWrappingHandler());
            config.Filters.Add(new ValidateModelAttribute());
            config.Formatters.JsonFormatter.SerializerSettings = settings;
            // Web API routes
            config.MapHttpAttributeRoutes();



            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
