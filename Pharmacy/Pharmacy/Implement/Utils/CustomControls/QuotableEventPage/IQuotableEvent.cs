using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.CustomControls.QuotableEventPage
{
    public interface IQuotableEvent
    {
        void OnUnloaded();


        void OnLoaded();


        void OnBeginInit();


        void OnEndInit();

    }
}
