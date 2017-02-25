using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using FluentValidation;
using WebApi.ModelBinder;


namespace WebApi.Controllers
{
    public class PersonController : ApiController
    {

        private static readonly List<Person> persons = new List<Person>()
        {
            new Person {FirstName = "Jayakumar",LastName="Mogenahalli", Age=42, Brother = new Person {FirstName="Chandru", Age = 40 } },
            new Person {FirstName="Pavan", LastName="Kumar", Age=35, Brother = new Person {FirstName="Kiran", Age=25 } }
        };

        public IEnumerable<Person> Get()
        {
            return persons;
        }

        [HttpPost]  
        public IHttpActionResult Post( Person person)
        {
           

            if (!ModelState.IsValid)
            {
                // validation failed; throw HttpResponseException
                //throw new HttpResponseException(
                //    new HttpResponseMessage
                //    {
                //        StatusCode = HttpStatusCode.BadRequest,
                //        ReasonPhrase = "Validation failed."
                //    });

                return BadRequest("Something went wrong");
            }

            return Ok("Everything went well");
        }


    }
}
