﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using Newtonsoft.Json.Linq;

namespace WebApi.ModelBinder
{

    /// <summary>
    /// Code from http://arcware.net/logging-web-api-requests/
    /// TO BE COMPLETE - NOT WORKING
    /// </summary>
    public class ApiLogHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiLogEntry = CreateApiLogEntryWithRequestData(request);
            if (request.Content != null)
            {
                await request.Content.ReadAsStringAsync()
                    .ContinueWith(task =>
                    {
                        apiLogEntry.RequestContentBody = task.Result;
                    }, cancellationToken);
            }

            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;

                    // Update the API log entry with response info
                    apiLogEntry.ResponseStatusCode = (int) response.StatusCode;
                    apiLogEntry.ResponseTimestamp = DateTime.Now;

                    if (response.Content != null)
                    {
                        var byteResponse = response.Content.ReadAsByteArrayAsync().Result;
                        apiLogEntry.ResponseContentBody = System.Text.Encoding.UTF8.GetString(byteResponse);  
                        apiLogEntry.ResponseContentType = response.Content.Headers.ContentType.MediaType;
                        apiLogEntry.ResponseHeaders = SerializeHeaders(response.Content.Headers);
                    }



                    var jsonData = JsonConvert.SerializeObject(apiLogEntry);
                    var now = DateTime.Now;
                    var path =
                        $"{AppDomain.CurrentDomain.BaseDirectory}/{@"\log\" + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString() + now.Millisecond.ToString() + ".log"}";
                    System.IO.File.WriteAllText(path, jsonData);



                    return response;
                }, cancellationToken);
             // return base.SendAsync(request, cancellationToken);
        }


        private ApiLogEntry CreateApiLogEntryWithRequestData(HttpRequestMessage request)
        {
            var context = ((HttpContextBase)request.Properties["MS_HttpContext"]);
            var routeData = request.GetRouteData();

            return new ApiLogEntry
            {
                Application = "WebApi", // "[insert-calling-app-here]",
                User = context.User.Identity.Name,
                Machine = Environment.MachineName,
                RequestContentType = context.Request.ContentType,
                RequestRouteTemplate = routeData.Route.RouteTemplate,
                RequestRouteData = SerializeRouteData(routeData),
                RequestIpAddress = context.Request.UserHostAddress,
                RequestMethod = request.Method.Method,
                RequestHeaders = SerializeHeaders(request.Headers),
                RequestTimestamp = DateTime.Now,
                RequestUri = request.RequestUri.ToString()
            };
        }

        private string SerializeRouteData(IHttpRouteData routeData)
        {
            return JsonConvert.SerializeObject(routeData, Formatting.Indented);
        }

        private string SerializeHeaders(HttpHeaders headers)
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in headers.ToList())
            {
                if (item.Value != null)
                {
                    var header = String.Empty;
                    foreach (var value in item.Value)
                    {
                        header += value + " ";
                    }

                    // Trim the trailing space and add item to the dictionary
                    header = header.TrimEnd(" ".ToCharArray());
                    dict.Add(item.Key, header);
                }
            }

            return JsonConvert.SerializeObject(dict, Formatting.Indented);
        }
    }
}