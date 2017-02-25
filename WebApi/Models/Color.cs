using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using WebApi.ModelBinder;

namespace WebApi.Models
{
    [TypeConverter(typeof(ColorTypeConverter))]
    public class Color
    {
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }

        
    }
}