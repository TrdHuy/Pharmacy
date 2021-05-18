using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.BaseWindow.MVVM.ViewModel
{
    internal abstract class BasePageViewModel : BaseViewModel, IQuotableEvent
    {
        protected abstract Logger logger { get; }

        public bool IsInitialized { get; private set; }

        public BasePageViewModel()
        {
            logger.I("Initializing base page view model");
            IsInitialized = false;

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
        }

        public virtual void OnBeginInit(object sender)
        {
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
    }
}

