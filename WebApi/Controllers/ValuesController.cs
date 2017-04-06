using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using WebApi.Models;

namespace WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [ResponseType(typeof(Color))]
        //[Authorize] 
       
        public string[] Get()
        {
          //  return InternalServerError();
            return new string[] { "value1", "value2" };
        }

        
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        /// <remarks>This will remove from 1st of August</remarks>
        /// </summary>
        /// <param name="value"></param>
        [Obsolete]
        
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
