using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ColorController : ApiController
    {
        //public IHttpActionResult Get(double red, double green, double blue)
        //{
        //    return Ok(new Color { Red = red, Green = green, Blue = blue });
        //}

        /// <summary>
        /// http://localhost:62870/api/color/?color=10,10,10 - invoke TypeConverter
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string color)
        {
            if(color ==null)
            {
                return BadRequest("object is null");
            }
            else
            {
                return Ok(color);
            }
        }
    }
}
