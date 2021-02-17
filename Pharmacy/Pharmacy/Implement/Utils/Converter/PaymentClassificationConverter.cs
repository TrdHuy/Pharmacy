using Pharmacy.Base.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.Converter
{
    public class PaymentClassificationConverter : BaseMultiValueConverter<PaymentClassificationConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var val1 = values[0];
            var val2 = values[1];
            var isPaid = System.Convert.ToDecimal(val1) >= System.Convert.ToDecimal(val2);
            return isPaid ? "Trả" : "Nợ";
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
