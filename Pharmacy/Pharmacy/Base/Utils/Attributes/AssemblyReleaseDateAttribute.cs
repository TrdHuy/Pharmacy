using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Utils.Attributes
{
    public class AssemblyReleaseDateAttribute : Attribute
    {
        private string date;
        public AssemblyReleaseDateAttribute() : this(string.Empty) { }

        public AssemblyReleaseDateAttribute(string date) { this.date = date; }

        public string ReleaseDate
        {
            get
            {
                return date;
            }
        }
    }
}