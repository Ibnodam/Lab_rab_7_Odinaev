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
    class FromVoyageIdToCaptain : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HookContext db = new HookContext();
            return db.Voyages.Where(p => p.VoyageId == (int)value).FirstOrDefault()!.CaptainName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
