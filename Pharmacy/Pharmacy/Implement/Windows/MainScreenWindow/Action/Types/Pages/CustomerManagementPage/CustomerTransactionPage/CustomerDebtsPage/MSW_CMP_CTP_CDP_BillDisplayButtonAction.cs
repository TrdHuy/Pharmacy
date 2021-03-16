using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerDebtsPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerDebtsPage
{
    public class MSW_CMP_CTP_CDP_BillDisplayButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost { get; set; } = MSW_PageController.Instance;
        private CustomerDebtsPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as CustomerDebtsPageViewModel;
            DataGrid ctrl = dataTransfer[1] as DataGrid;

            MSW_DataFlowHost.Current.CurrentSelectedCustomerOrder = _viewModel.OrderItemSource.Where(o => o.OrderID == _viewModel.DebtItemSource[ctrl.SelectedIndex].OrderID).FirstOrDefault();
            _pageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_BILL_PAGE);

            return true;
        }
    }
}
