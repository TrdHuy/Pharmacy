using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils
{
    public static class UtilMethods
    {
        public static bool IsHavingOnlyNumber(string text)
        {
            Regex regex = new Regex("^[0-9]*$");
            return regex.IsMatch(text);
        }
    }
}
