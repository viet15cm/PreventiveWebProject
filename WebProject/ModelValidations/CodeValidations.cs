using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class CodeValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;
            if (!IsValidCode(value))
            {
                errorType = "không chứa các kí tự đặt biệt và dấu !";
            }
            else if (!IsValidStringLenght(value))
            {
                errorType = "độ dài phải 5 kí tự !";
            }
            else
            {
                return ValidationResult.Success;
            }

            ErrorMessage = $"{validationContext.DisplayName} dữ liệu  {errorType}";

            return new ValidationResult(ErrorMessage);
        }

        bool IsValidCode(object value )
        {
            if (value != null)
            {
                var c = Regex.IsMatch(value.ToString(), @"^[a-zA-Z0-9.]+$");

                if (c)
                {
                    return true;
                }

                return false;
            }

            return true; 
        }

        bool IsValidStringLenght(object value)
        {

            if (value != null)
            {
                if ((value.ToString()).Length == 5)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
