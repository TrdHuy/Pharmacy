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
        public double DesignHeight { get; set; }
        public double DesignWidth { get; set; }

        public void OnBeginInit()
        {
        }

        public void OnEndInit()
        {
        }

        public void OnLoaded()
        {
        }

        public void OnUnloaded()
        {
        }
    }
}
