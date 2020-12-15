using Pharmacy.Implement.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

    }
}
