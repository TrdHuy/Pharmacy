using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage
{
    public class MSW_CMP_HistoryButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private CustomerManagementPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as CustomerManagementPageViewModel;
            DataGrid ctrl = dataTransfer[1] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedCustomer = ctrl.SelectedItem as tblCustomer;
            _pageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_TRANSACTION_HISTORY_PAGE);
            return true;
        }
    }
}
