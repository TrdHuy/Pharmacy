using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.CustomControls.QuotableEventPage
{
    public interface IQuotableEvent
    {
        /// <summary>
        /// This event occurs every time the page unloaded
        /// </summary>
        void OnUnloaded();

        /// <summary>
        /// This event occurs evert time page loaded
        /// </summary>
        void OnLoaded();

        /// <summary>
        /// This event occurs every time the page start initting
        /// </summary>
        void OnBeginInit();

        /// <summary>
        /// This event occurs every time the page finish initting
        /// </summary>
        void OnEndInit();

    }
}
