using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sample.Validation
{
    public class ComboboxValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
           if (value is null)
            {
                return new ValidationResult(false, "Выберите 1 из элементов");
            }
            return ValidationResult.ValidResult;
        }
    }
}
