using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage
{
    public class MSW_CMP_CTP_BillButtonAction : Base.UIEventHandler.Action.IAction
    {
        private CustomerTransactionHistoryPageViewModel _viewModel { get; set; }
        private MSW_PageController _pageHost { get; set; } = MSW_PageController.Instance;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as CustomerTransactionHistoryPageViewModel;
            
            if(_viewModel.CurrentSelectedOrder == null)
            {
                App.Current.ShowApplicationMessageBox("Vui lòng chọn 1 hóa đơn để xem chi tiết!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                return false;
            }

            MSW_DataFlowHost.Current.CurrentSelectedCustomerOrder = _viewModel.CurrentSelectedOrder;
            _pageHost.UpdateCurrentPageSource(PageSource.CustomerBillPage);

            return true;
        }
    }
}
