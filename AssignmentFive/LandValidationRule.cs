using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AssignmentFive
{
    class LandValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal landDecimal = 0;
            string landString = value.ToString();
            decimal.TryParse(landString, out landDecimal);
            if ((landDecimal >= 100) || (landDecimal <= 0))

            {
                return new ValidationResult(false, "please enter number less than 100");

            }
                return ValidationResult.ValidResult;
            throw new NotImplementedException();
        }
    }
}
