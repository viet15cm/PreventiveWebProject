using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class NameValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;
            if (!IsValidStringLenght(value))
            {
                errorType = "độ dài phải nhỏ hơn 30 kí tự !";
            }
            else
            {
                return ValidationResult.Success;
            }

            ErrorMessage = $"{validationContext.DisplayName} dữ liệu  {errorType}";

            return new ValidationResult(ErrorMessage);
        }

        bool IsValidStringLenght(object value)
        {
            if(value!= null)
            {
                var stringName = value.ToString();
                if (!(stringName.Length < 31))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
