using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using WebApi.Models;
using FluentValidation;
using Newtonsoft.Json;
using WebApi.ModelBinder;


namespace WebApi.Controllers
{
    public class PersonController : ApiController
    {

        private static readonly List<Person> persons = new List<Person>()
        {
            new Person {FirstName = "Jayakumar",LastName="Mogenahalli", Age=42 },
            new Person {FirstName="Pavan", LastName="Kumar", Age=35 }
        };

        public IEnumerable<Person> Get()
        {
            return persons;
        }

        [HttpPost]  
        public IHttpActionResult Post( [ModelBinder(typeof(PersonModelBinder))]Person person)
        {
           

            if (!ModelState.IsValid)
            {  
                return BadRequest();
            }
            var data = JsonConvert.SerializeObject(person);
            return Ok(data);
        }

        [Route("HttpError")]
        [HttpGet]
        public HttpResponseMessage HttpError()
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, "You cannot access this method at this time.");
        }


    }
}
