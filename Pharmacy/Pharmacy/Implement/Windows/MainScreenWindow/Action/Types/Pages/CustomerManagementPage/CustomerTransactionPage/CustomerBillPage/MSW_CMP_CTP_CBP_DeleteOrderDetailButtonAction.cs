using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    public class MSW_CMP_CTP_CBP_DeleteOrderDetailButtonAction : Base.UIEventHandler.Action.IAction
    {
        private CustomerBillPageViewModel _viewModel;
        private DataGrid ctrl;


        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as CustomerBillPageViewModel;
            ctrl = dataTransfer[1] as DataGrid;

            _viewModel.CurrentOrderDetails.RemoveAt(ctrl.SelectedIndex);

            return true;
        }
    }
}
