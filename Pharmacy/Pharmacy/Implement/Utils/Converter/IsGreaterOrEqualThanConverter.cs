using Pharmacy.Base.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.Converter
{
    public class IsGreaterOrEqualThanConverter : BaseMultiValueConverter<IsGreaterOrEqualThanConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var val1 = values[0];
            var val2 = values[1];
            switch (val1)
            {
                case int val:
                    return val >= System.Convert.ToInt32(val2);
                case double val:
                    return val >= System.Convert.ToDouble(val2);
                default:
                    return false;
            }
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
