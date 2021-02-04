using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    public class MSW_CMP_CTP_CBP_EditEnablerButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private CustomerBillPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as CustomerBillPageViewModel;
            
            _viewModel.IsEnableEdittingBill = !_viewModel.IsEnableEdittingBill;
            
            _viewModel.DeleteColumnVisibility = _viewModel.IsEnableEdittingBill ? 
                System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

            return true;
        }
    }
}
