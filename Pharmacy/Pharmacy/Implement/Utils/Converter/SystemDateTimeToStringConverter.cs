using Pharmacy.Base.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.Converter
{
    public class SystemDateTimeToStringConverter : BaseValueConverter<SystemDateTimeToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            string type = parameter?.ToString();

            switch (type)
            {
                case "DATE":
                    return dateTime.ToString("dd/MM/yyyy");
                case "TIME":
                    return dateTime.ToString("HH:mm");
                default:
                    return dateTime.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
