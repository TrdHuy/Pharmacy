using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages
{
    public abstract class PSW_BasePageViewModel : BaseViewModel, IQuotableEvent
    {
        public virtual void OnApplyTemplate(object sender)
        {
        }

        public virtual void OnBeginInit(object sender)
        {
        }

        public virtual void OnEndInit(object sender)
        {
        }

        public virtual void OnLoaded(object sender)
        {
        }

        public virtual void OnSizeChanged(object sender)
        {
        }

        public virtual void OnUnloaded(object sender)
        {
        }
    }
}
