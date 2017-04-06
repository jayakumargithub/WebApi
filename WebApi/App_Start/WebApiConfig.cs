using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using WebApi.ModelBinder;
using FluentValidation.WebApi;
using Elmah.Contrib.WebApi;
using Swashbuckle.Application;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());
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
            //https://docs.microsoft.com/en-us/aspnet/web-api/overview/releases/whats-new-in-aspnet-web-api-21#global-error
           // config.Services.Add(typeof(IExceptionLogger),new TraceSourceExceptionLogger(new TraceSource("MyTraceSource", SourceLevels.All)));


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional},
                // constraint required so this route only matches valid controller names
                constraints:  new {controller = GetControllerNames()},
               handler: new ResponseWrappingHandler(GlobalConfiguration.Configuration)
            );
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr); 
            //config.MessageHandlers.Add(new ResponseWrappingHandler());
            config.Routes.MapHttpRoute(
                name: "NotFound",
                routeTemplate: "{*path}",
                defaults: new { controller = "Error", action = "NotFound" });

            config.Routes.MapHttpRoute(
            name: "InternalServerError",
            routeTemplate: "{*path}",
            defaults: new { controller = "Error", action = "InternalServerError" }
           
);
        }

        // helper method that returns a string of all api controller names 
        // in this solution, to be used in route constraints above
        private static string GetControllerNames()
        {
            var controllerNames = Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(x =>
                    x.IsSubclassOf(typeof(ApiController)) &&
                    x.FullName.StartsWith(MethodBase.GetCurrentMethod().DeclaringType.Namespace + ".Controllers"))
                .ToList()
                .Select(x => x.Name.Replace("Controller", ""));

            return string.Join("|", controllerNames);
        }
    }
}
