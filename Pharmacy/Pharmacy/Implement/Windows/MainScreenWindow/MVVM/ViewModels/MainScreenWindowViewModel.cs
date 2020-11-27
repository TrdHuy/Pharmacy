using Pharmacy.Base.MVVM.ViewModels;
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
    public class MainScreenWindowViewModel : AbstractViewModel,
        Base.Observable.ObserverPattern.IObserver<Uri>
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private DashboardWindow _mainScreenWindow;
        private Uri _currenPageHost;

        public Uri CurrentPageSource
        {
            get { return _currenPageHost; }
            set
            {
                _currenPageHost = value;
                InvalidateOwn();
            }
        }


        protected override void InitPropertiesRegistry()
        {
        }

        public MainScreenWindowViewModel(DashboardWindow mainScreenWindow)
        {
            _mainScreenWindow = mainScreenWindow;
            CurrentPageSource = _pageHost.CurrentPageSource;
            _pageHost.Subcribe(this);
        }

        public void Update(Uri value)
        {
            CurrentPageSource = value;
            _mainScreenWindow.Navigate(value);
        }
    }
}
