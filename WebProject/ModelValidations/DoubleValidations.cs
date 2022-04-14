using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class DoubleValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;
            if (!IsValidDouble(value))
            {
                errorType = "không hợp lệ !";

            }          
            else
            {
                return ValidationResult.Success;
            }

            ErrorMessage = $"{validationContext.DisplayName} dữ liệu  {errorType}";

            return new ValidationResult(ErrorMessage);

        }

        bool IsValidDouble(object value)
        {
            if (value != null)
            {
                try
                {
                    var db = double.Parse(value.ToString());
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
            
        }


        //(+/-)5.0 x 10-324 tới (+/-)1.7 x 10308
    }
}
