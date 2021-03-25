using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    internal class MSW_CMP_CTP_CBP_RefreshButtonAction : MSW_CMP_CTP_CBP_ButtonAction
    {
        public MSW_CMP_CTP_CBP_RefreshButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            if (CBPViewModel.IsOrderModified)
            {
                var x = App.Current.ShowApplicationMessageBox("Bạn có muốn hoàn lại (các) tác vụ?",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo!");

                if(x == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
                {
                    CBPViewModel.RefreshListOrder();
                }
            }

            return;
        }
    }
}
