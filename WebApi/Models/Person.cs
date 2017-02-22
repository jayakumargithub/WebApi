using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{

    /// <summary>
    /// This class has a self reference, which throw an error ' object reference preserved error' 
    /// to resolve this in the web.config need to add 'jsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;'
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person Brother { get; set; }
    }


}