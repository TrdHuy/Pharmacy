using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils.ViewModelHelper;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.CustomControls;
using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM

{
    public abstract class MSW_BasePageViewModel : BaseViewModel, IQuotableEvent, IVMMHelperExtension
    {
        protected abstract Logger logger { get; }

        public MSW_FontSizeOV FontSizeOV { get; set; }
        public bool IsInitialized { get; private set; }

        public MSW_BasePageViewModel()
        {
            logger.I("Initializing base page view model");
            IsInitialized = false;

            FontSizeOV = new MSW_FontSizeOV(this);

            OnInitializing();

            logger.I("Initialized base page view model");
            OnInitialized();

            IsInitialized = true;
        }

        /// <summary>
        /// Occur once when the view model was created
        /// </summary>
        protected abstract void OnInitializing();

        /// <summary>
        /// Occur once when finish initting view model
        /// </summary>
        protected abstract void OnInitialized();

        public virtual void OnUnloaded(object sender)
        {
        }

        public virtual void OnLoaded(object sender)
        {
            RefreshViewModel();
        }

        public virtual void OnEndInit(object sender)
        {
        }

        public virtual void OnSizeChanged(object sender)
        {
        }

        public virtual void OnApplyTemplate(object sender)
        {
        }

        public virtual void OnPreviewBindingDataContextInCache()
        {
        }
    }
}
