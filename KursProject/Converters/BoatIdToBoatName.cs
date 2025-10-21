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
    public class BoatIdToBoatName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HookContext db = new HookContext();
            return db.Boats.Where(p => p.BoatId == (int)value).FirstOrDefault()!.BoatName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
