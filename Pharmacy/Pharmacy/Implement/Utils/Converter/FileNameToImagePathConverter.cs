using Pharmacy.Base.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.Converter
{
    public class FileNameToImagePathConverter : BaseValueConverter<FileNameToImagePathConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageName = value.ToString();
            string folderName = parameter?.ToString();

            return FileIOUtil.
                GetImagePathFromName(imageName, folderName);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}