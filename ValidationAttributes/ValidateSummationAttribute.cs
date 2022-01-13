using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVApplication.ValidationAttributes
{
    public class ValidateSummationAttribute : ValidationAttribute
    {
        private readonly int number1;
        private readonly int number2;

        public ValidateSummationAttribute(int num1, int num2)
        {
            this.number1 = num1;
            this.number2 = num2;
        }

        public override bool IsValid(object value)
        {
            string result = (number1 + number2).ToString();
            return value.ToString().Equals(result);
        }
    }
}
