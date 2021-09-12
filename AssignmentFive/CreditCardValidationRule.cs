using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AssignmentFive
{
    class CreditCardValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool isCreditCardNumberValid = false;
            string creditCard = value.ToString();
            if ((creditCard != null) && (creditCard.Length == 19))
            {
                string[] ccNumArr = creditCard.Split(' ');
                char[] ccNumChar = creditCard.ToCharArray();
                if (ccNumArr.Length == 4)
                {
                    for (int i = 0; i < ccNumChar.Length; i++)
                    {
                        if (char.IsDigit(ccNumChar[i]) || (ccNumChar[i] == ' '))
                        {
                            isCreditCardNumberValid = true;
                        }
                        else
                        {
                            isCreditCardNumberValid = false;
                            break;
                        }
                    }
                }

            }
            if (!isCreditCardNumberValid)
            {
                return new ValidationResult(false, "please enter credit card format 0000 0000 0000 0000");
            }
            return ValidationResult.ValidResult;
        }
    }
}
