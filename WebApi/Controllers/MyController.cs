using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using WebApi.ModelBinder;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class MyController : ApiController
    {
        public string[] Get([ModelBinder(typeof(MyBinder))]Marketing marketing)
        {
            string[] data = new string[2];
            data[0] = "Jayakumar";
            return data;
        }
    }
}
