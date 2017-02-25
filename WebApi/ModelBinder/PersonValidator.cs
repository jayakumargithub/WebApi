using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using WebApi.Models;

namespace WebApi.ModelBinder
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            //To set the cascade mode globally, you can set the CascadeMode property on the static ValidatorOptions class during your application's startup routine:
            //ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            // CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(person => person.FirstName).NotNull();
                 RuleFor(person => person.LastName).NotNull();
            RuleFor(person => person.Age).GreaterThan(0);
        }
    }
}