using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class ErrorController : ApiController
    {
        [System.Web.Http.HttpGet, System.Web.Http.HttpPost, System.Web.Http.HttpPut, System.Web.Http.HttpDelete, System.Web.Http.HttpHead, System.Web.Http.HttpOptions]
 //       [System.Web.Http.Route("HttpN")]
        public HttpResponseMessage NotFound(string path)
        {
            // log error to ELMAH
             Elmah.ErrorSignal.FromCurrentContext().Raise(new HttpException(404, "404 Not Found: /" + path));

            // return 404
            return Request.CreateResponse(HttpStatusCode.Forbidden, "You Can't access this");
        }

        [System.Web.Http.HttpGet, System.Web.Http.HttpPost, System.Web.Http.HttpPut, System.Web.Http.HttpDelete, System.Web.Http.HttpHead, System.Web.Http.HttpOptions]
        public HttpResponseMessage InternalServerError(string path)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new HttpException(401, "you don't authorisation for the : /" + path));
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "You are not authorized");
        }

        [System.Web.Http.HttpGet, System.Web.Http.HttpPost, System.Web.Http.HttpPut, System.Web.Http.HttpDelete, System.Web.Http.HttpHead, System.Web.Http.HttpOptions]
        public IHttpActionResult Ok(object path)
        {
            // log error to ELMAH
            // Elmah.ErrorSignal.FromCurrentContext().Raise(new HttpException(404, "404 Not Found: /" + path));

            // return 404
            return Ok();
        }

        /// <summary>Creates a <see cref="T:System.Web.Http.Results.NotFoundResult" />.</summary>
        /// <returns>A <see cref="T:System.Web.Http.Results.NotFoundResult" />.</returns>
        ///[Route("HttpError")]
        //[HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions]
        //protected override NotFoundResult NotFound()
        //{
        //    return  new NotFoundResult();
        //}
    }
}
