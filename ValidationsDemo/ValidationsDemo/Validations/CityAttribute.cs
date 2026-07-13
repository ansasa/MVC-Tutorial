using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ValidationsDemo.Models;

namespace ValidationsDemo.Validations
{
    public class CityAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {

            var ud = (UserDetails)validationContext.ObjectInstance;

            if (ud.City == null)
                return new ValidationResult("City cannot be null");

            if (ud.City.ToLower().Equals("hyderabad") || ud.City.ToLower().Equals("cyberabad"))
                return ValidationResult.Success;

            return new ValidationResult("City can only be either Hyderabad or Cyberabad");
        }



    }
}
