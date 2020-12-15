using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.Attributes
{
    ///<summary>
    ///This attribute is used to represent a string value
    ///for a value enum.
    ///</summary>
    public class StringValueAttribute : Attribute
    {
        ///<summary>
        ///Holds the string value for an enum
        ///</summary>
        public string StringValue { get; protected set; }

        ///<summary>
        ///Constructor for StringValue Attribute
        ///</summary>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
    }


}
