using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return  Ok(new { info = "Access any 404 path and check /elmah.axd to see the 404 error in the log." });
        }
    }
}
