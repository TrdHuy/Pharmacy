using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Base.MVVM.Model
{
    public abstract class AbstractBaseModel : ICloneable
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
                    MessageBox.Show(e.Message);
                    return null;
                }

                ;
            }

            set { _parts[key] = value; }
        }

        public abstract object Clone();
    }
}
