using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sample.Validation
{
   public class DateValidationRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.Now)
                {
                    return new ValidationResult(false, "Дата не может быть в будущем.");
                }
            }
            else if(value is null)
            {
                return new ValidationResult(false, "Поле пусто");
            }
            else
            {
                return new ValidationResult(false, "Введите дату в правильном формате");

            }
            return ValidationResult.ValidResult;
        }
    }
}
