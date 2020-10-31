using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.MVVM.Model
{
    abstract class AbstractBaseModel
    {
        private Dictionary<string, object> _parts = new Dictionary<string, object>();

        public object this[string key]
        {
            get
            {
                try
                {
                    return _parts[key];
                }
                catch (Exception e)
                {
                    return null;
                }

                ;
            }

            set { _parts[key] = value; }
        }

    }
}
