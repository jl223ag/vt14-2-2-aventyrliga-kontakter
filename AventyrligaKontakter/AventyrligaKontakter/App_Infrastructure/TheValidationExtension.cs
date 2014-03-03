using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public static class TheValidationExtension
{
    public static bool Validate(this object instance, out ICollection<ValidationResult> validationResults)
    {
        var validationContext = new ValidationContext(instance);
        validationResults = new List<ValidationResult>();

        return Validator.TryValidateObject(instance, validationContext, validationResults, true);
    }
}