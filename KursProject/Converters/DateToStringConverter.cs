using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sample.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString("MM/dd/yyyy"); // Формат даты, который вам нужен
            }
            return string.Empty; // Если значение null или другое, возвращаем пустую строку
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateString && DateTime.TryParse(dateString, out DateTime dateTime))
            {
                return dateTime; // Преобразуем обратно в DateTime
            }
            return null; // В случае ошибки, возвращаем null
        }
    }
}
