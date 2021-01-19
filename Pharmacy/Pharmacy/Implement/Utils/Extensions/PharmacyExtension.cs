using Pharmacy.Implement.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pharmacy.Implement.Utils.Extensions
{
    public static class PharmacyExtension 
    {
        public static string GetStringValue(this Enum value)
        {
            //Get the type
            Type t = value.GetType();

            //Get the field info for this type
            FieldInfo fieldInfo = t.GetField(value.ToString());

            //Get stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            //return the first if there was a match
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource ToImageSource(this Bitmap bitmap)
        {
            var handle = bitmap.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                DeleteObject(handle);
            }
        }

        public static bool IsHavingOnlyNumber(this string text)
        {
            Regex regex = new Regex("^[0-9]*$");
            return regex.IsMatch(text);
        }
    }
}
