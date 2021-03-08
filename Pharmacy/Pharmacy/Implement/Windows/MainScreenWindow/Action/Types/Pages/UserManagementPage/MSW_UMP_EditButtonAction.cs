using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage
{
    public class MSW_UMP_EditButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private UserManagementPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as UserManagementPageViewModel;
            DataGrid ctrl = dataTransfer[1] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedUser = _viewModel.UserItemSource[ctrl.SelectedIndex];
            _pageHost.UpdateCurrentPageSource(PageSource.USER_MODIFICATION_PAGE);
            return true;
        }
    }
}
