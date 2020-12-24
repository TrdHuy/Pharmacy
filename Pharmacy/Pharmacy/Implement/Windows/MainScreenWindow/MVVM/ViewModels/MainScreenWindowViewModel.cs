using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Config;
using Pharmacy.Implement.Utils.CustomControls;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels
{
    public class MainScreenWindowViewModel : AbstractViewModel
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private PageSourceWatcher _pageSourceWatcher;

        public Uri CurrentPageSource
        {
            get
            {
                return _pageHost.CurrentPageOV.PageUri;
            }
            set
            {
                _pageHost.UpdatePageOVUri(value);
                InvalidateOwn();
            }
        }

        public long PageLoadingDelayTime
        {
            get
            {
                return _pageHost.CurrentPageOV.LoadingDelayTime;
            }
        }

        public DashboardWindowContentType ContentType
        {
            get
            {
                return RUNE.IS_SUPPORT_WINDOW_NAVIGATION_BUTTON_PANEL ?
                    DashboardWindowContentType.PageType : DashboardWindowContentType.NormalContentType;
            }
        }

        protected override void InitPropertiesRegistry()
        {
            PropRegister("PageLoadingDelayTime");
            PropRegister("CurrentPageSource");
        }

        public MainScreenWindowViewModel()
        {
            _pageSourceWatcher = new PageSourceWatcher(OnPageSourceChange);
            _pageHost.Subcribe(_pageSourceWatcher);
        }

        private void OnPageSourceChange(PageOV newSource)
        {
            Invalidate("PageLoadingDelayTime");
            Invalidate("CurrentPageSource");

            //Rollback the manipulation to the entity data
            DbManager.Instance.RollBack();

            //Every time navigate to new source, the view model of those source page will be instantiated again
            //_mainScreenWindow.Navigate(newSource);
        }

    }

    internal class PageSourceWatcher : Base.Observable.ObserverPattern.IObserver<PageOV>
    {
        private Action<PageOV> OnPageSourceChange;

        internal PageSourceWatcher(Action<PageOV> onSourceChange)
        {
            OnPageSourceChange = onSourceChange;
        }
        public void Update(PageOV value)
        {
            OnPageSourceChange?.Invoke(value);
        }
    }
}
