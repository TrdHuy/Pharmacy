using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    public class MSW_CMP_CTP_CBP_RefreshButtonAction : Base.UIEventHandler.Action.IAction
    {
        private CustomerBillPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as CustomerBillPageViewModel;

            if (_viewModel.IsOrderModified)
            {
                var x = App.Current.ShowApplicationMessageBox("Bạn có muốn hoàn lại (các) tác vụ?",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo!");

                if(x == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
                {
                    _viewModel.RefreshListOrder();
                }
            }

            return true;
        }
    }
}
