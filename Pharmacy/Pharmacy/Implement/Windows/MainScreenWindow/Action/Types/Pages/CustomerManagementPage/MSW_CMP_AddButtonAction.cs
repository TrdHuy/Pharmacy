using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage
{
    public class MSW_CMP_AddButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;

        public bool Execute(object[] dataTransfer)
        {
            _pageHost.UpdateCurrentPageSource(PageSource.CustomerInstantiationPage);

            return true;
        }
    }
}