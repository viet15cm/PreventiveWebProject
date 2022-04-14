using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class IntValidations : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;
            if (!IsValidInt(value))
            {
                errorType = "không hợp lệ !";
            }
            else if (!IsValidIntSize(value))
            {
                errorType = "độ dài không hợp lệ !";
            }           
            else
            {
                return ValidationResult.Success;
            }

            ErrorMessage = $"{validationContext.DisplayName} dữ liệu  {errorType}";

            return new ValidationResult(ErrorMessage);

        }


        bool IsValidInt(object value)
        {
            if (value != null)
            {
                try
                {
                    var number = (int)value;

                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }

            return true;
        }

        bool IsValidIntSize(object value)
        {
            if (value != null)
            {
                int number = (int)value;

                if (number > -2147483647 && number < 2147483646)
                {
                    return true;
                }

                return false;
            }

            return true;
        }
    }
}
