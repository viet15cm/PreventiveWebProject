using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class UrlImgValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;
            if (!IsValidUrImg(value))
            {
                errorType = "không chứa các kí tự đặt biệt và dấu !";
            }           
            else
            {
                return ValidationResult.Success;
            }

            ErrorMessage = $"{validationContext.DisplayName} dữ liệu  {errorType}";

            return new ValidationResult(ErrorMessage);
        }

        bool IsValidUrImg(object value)
        {
            if (value != null)
            {
                var IsName = Regex.IsMatch(value.ToString(), @"^[\w\-. ]+$");

                if (IsName)
                {
                    return true;
                }

                return false;
            }
            return true;
        }
    }
}
