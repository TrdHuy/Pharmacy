using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SettingPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SettingPage
{
    public class MSW_SeP_CancleButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SettingPageViewModel _viewModel;
        private MSW_PageController _pageHost = MSW_PageController.Instance;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as SettingPageViewModel;
            _pageHost.UpdateCurrentPageSource(PageSource.HomePage);

            return true;
        }
    }
}
