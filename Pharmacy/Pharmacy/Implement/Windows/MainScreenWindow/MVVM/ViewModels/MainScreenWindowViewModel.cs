using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Config;
using Pharmacy.Implement.Utils.CustomControls;
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
        private DashboardWindow _mainScreenWindow;
        private Uri _currenPageHost;
        private PageSourceWatcher _pageSourceWatcher;

        public Uri CurrentPageSource
        {
            get { return _currenPageHost; }
            set
            {
                _currenPageHost = value;
                InvalidateOwn();
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
        }

        public MainScreenWindowViewModel(DashboardWindow mainScreenWindow)
        {
            _mainScreenWindow = mainScreenWindow;
            _pageSourceWatcher = new PageSourceWatcher(OnPageSourceChange);
            CurrentPageSource = _pageHost.CurrentPageSource;
            _pageHost.Subcribe(_pageSourceWatcher);

        }

        private void OnPageSourceChange(Uri newSource)
        {
            CurrentPageSource = newSource;

            //Every time navigate to new source, the view model of those source page will be instantiated again
            _mainScreenWindow.Navigate(newSource);
        }

    }

    internal class PageSourceWatcher : Base.Observable.ObserverPattern.IObserver<Uri>
    {
        private Action<Uri> OnPageSourceChange;

        internal PageSourceWatcher(Action<Uri> onSourceChange)
        {
            OnPageSourceChange = onSourceChange;
        }
        public void Update(Uri value)
        {
            OnPageSourceChange?.Invoke(value);
        }
    }
}
