﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using FluentValidation;
using WebApi.ModelBinder;
using FluentValidation.Attributes;

namespace WebApi.Models
{

    /// <summary>
    /// This class has a self reference, which throw an error ' object reference preserved error' 
    /// to resolve this in the web.config need to add 'jsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;'
    /// </summary>
    //[Validator(typeof(PersonValidator))]
    //[ModelBinder(typeof(PersonModelBinder))]
    public class Person
    {
        //public Person()
        //{
        //    Brother = new Person();
        //}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
       // public Person Brother { get; set; }
    }


}