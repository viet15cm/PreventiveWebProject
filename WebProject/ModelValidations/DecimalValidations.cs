using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.ModelValidations
{
    public class DecimalValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;
            if (!IsValidDecimal(value))
            {
                errorType = "không hợp lệ !";

            }
            else if (!IsValidDecimalSize(value))
            {
                errorType = " có độ dài không hợp lệ !";
            }
            else
            {
                return ValidationResult.Success;
            }

            ErrorMessage = $"{validationContext.DisplayName} dữ liệu  {errorType}";

            return new ValidationResult(ErrorMessage);

        }

        bool IsValidDecimal(object value)
        {
            if (value != null)
            {
                try
                {
                    var db = decimal.Parse(value.ToString());
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;

        }

        bool IsValidDecimalSize(object value)
        {
            if (value != null)
            {
                try
                {
                    var db = decimal.Parse(value.ToString());
                    if((double)db > -9999999999999999.99 && (double)db < 9999999999999999.99)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;

        }
    }
}
