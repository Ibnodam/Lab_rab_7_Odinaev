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
    public class FromVisitIdToDepDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HookContext db = new HookContext();
            return db.SpotVisits.Where(p => p.VisitId == (int)value).FirstOrDefault()!.DepartureDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
