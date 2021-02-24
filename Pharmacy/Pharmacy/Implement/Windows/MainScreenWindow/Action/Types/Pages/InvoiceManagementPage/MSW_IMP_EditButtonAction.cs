using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InvoiceManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.InvoiceManagementPage
{
    public class MSW_IMP_EditButtonAction : Base.UIEventHandler.Action.IAction
    {
        private InvoiceManagementPageViewModel _viewModel;
        private MSW_PageController _pageHost = MSW_PageController.Instance;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as InvoiceManagementPageViewModel;

            MSW_DataFlowHost.Current.CurrentSelectedCustomerOrder = _viewModel.CurrentSelectedOrderOV.Order;
            _pageHost.UpdateCurrentPageSource(PageSource.CustomerBillPage);

            return true;
        }
    }
}
