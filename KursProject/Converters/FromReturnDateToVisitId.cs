using Hook.Model;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sample.Converters
{
    class FromReturnDateToVisitId : IValueConverter
    {
        public object Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            HookContext db = new HookContext();
            return db.SpotVisits.Where(p => p.DepartureDate==value).FirstOrDefault()!.VisitId;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
