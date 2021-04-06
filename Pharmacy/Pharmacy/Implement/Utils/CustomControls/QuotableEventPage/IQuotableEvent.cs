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
        void OnUnloaded(object sender);

        /// <summary>
        /// This event occurs evert time page loaded
        /// </summary>
        void OnLoaded(object sender);

        /// <summary>
        /// This event occurs every time the page start initting
        /// </summary>
        void OnBeginInit(object sender);

        /// <summary>
        /// This event occurs every time the page finish initting
        /// </summary>
        void OnEndInit(object sender);

        /// <summary>
        /// This event occurs every time the page changed its own size
        /// </summary>
        void OnSizeChanged(object sender);

        /// <summary>
        /// This event occurs every time the page apply its template
        /// </summary>
        void OnApplyTemplate (object sender);
    }
}
