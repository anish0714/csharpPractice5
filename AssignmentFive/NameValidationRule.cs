using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AssignmentFive
{
    class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string custName = value.ToString();
            string[] splitCustName = custName.Split(' ');
            int falseCount = 0;
            if (custName.Length == 0)
            {
                falseCount++;
            }
            else
            {
                for (int i = 0; i < splitCustName.Length; i++)
                {
                    char[] custNameArrChar = splitCustName[i].ToCharArray();
                    for (int j = 0; j < custNameArrChar.Length; j++)
                    {
                        if (!char.IsLetter(custNameArrChar[j]))
                        {
                            falseCount++;
                        }
                    }
                }
            }
            if (falseCount > 0)
            {
                return new ValidationResult(false, "please enter name in proper format");
            }
            return ValidationResult.ValidResult;
        }
    }
}
