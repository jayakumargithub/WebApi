using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PersonController : ApiController
    {

        private static readonly List<Person> persons = new List<Person>()
        {
            new Person {Name = "Jayakumar", Age=42, Brother = new Person {Name="Chandru", Age = 40 } },
            new Person {Name="Pavan", Age=35, Brother = new Person {Name="Kiran", Age=25 } }
        };

        public IEnumerable<Person> Get()
        {
            return persons;
        }

        [HttpPost] 
        public IHttpActionResult Post([FromBody]Person person)
        {
            return Ok();
        }


    }
}
