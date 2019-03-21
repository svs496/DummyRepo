using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Core.CustomValidators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MinimumAgeAllowedAttribute : ValidationAttribute, IClientModelValidator
    {
        private int _minAge;

        public MinimumAgeAllowedAttribute(int MinAge)
            : base("The value of the {0} field is not valid")
        {
            this._minAge = MinAge;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-verifyage", GetErrorMessage());
            MergeAttribute(context.Attributes, "data-val-minimumAge", Convert.ToString(_minAge));
        }

        bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date;

            if(DateTime.TryParse(value.ToString(), out date))
            {
                if (date.AddYears(_minAge) > DateTime.Now)
                {
                    return new ValidationResult(GetErrorMessage());
                }
                
            }

            return ValidationResult.Success;

        }

        private string GetErrorMessage()
        {
            return $"Minimum Age for registration is {_minAge}";
        }
    }
}


