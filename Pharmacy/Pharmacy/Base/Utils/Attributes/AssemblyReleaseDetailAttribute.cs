using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Utils.Attributes
{
    public class AssemblyReleaseDetailAttribute : Attribute
    {
        private string detail;
        public AssemblyReleaseDetailAttribute() : this(string.Empty) { }

        public AssemblyReleaseDetailAttribute(string detail) { this.detail = detail; }

        public string ReleaseDetail
        {
            get
            {
                return detail;
            }
        }
    }
}
