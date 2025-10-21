using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sample.Validation
{
 public   class SelectionValidationStringRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // Проверяем, пусто ли значение
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult(false, "Please select one");
            }

            return ValidationResult.ValidResult;
        }
    }
}
