using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.Utils.ViewModelHelper
{
    interface IVMMHelperExtension
    {

        /// <summary>
        /// This event occur when the ViewModelHelper bind the data context to UI Elements
        /// </summary>
        void OnPreviewBindingDataContextInCache();
    }
}
