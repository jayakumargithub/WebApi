using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Marketing
    {
        public string RequestType { get; set; }
        public string Customer { get; set; }
        public Address Address { get; set; }
        public Product Product { get; set; }

    }


    public class Customer
    {
        public string Name { get; set; }
       
    }

    public class Address
    {
        public string Address1 { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
    }
}