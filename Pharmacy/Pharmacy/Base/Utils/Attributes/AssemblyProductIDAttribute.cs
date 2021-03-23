using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Utils.Attributes
{
    public class AssemblyProductIDAttribute : Attribute
    {
        private string prodID;
        public AssemblyProductIDAttribute() : this(string.Empty) { }

        public AssemblyProductIDAttribute(string id) { prodID = id; }

        public string ProductID
        {
            get
            {
                return prodID;
            }
        }
    }
}
