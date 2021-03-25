using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    internal class MSW_CMP_CTP_CBP_CancelButtonAction : MSW_CMP_CTP_CBP_ButtonAction
    {
        public MSW_CMP_CTP_CBP_CancelButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            if (CBPViewModel.IsOrderModified)
            {
                var x = App.Current.ShowApplicationMessageBox("Vui lòng lưu các thay đổi hoặc hoàn tác trước khi rời khỏi trang này!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
            }
            else
            {
                PageHost.UpdateCurrentPageSource(PageHost.PreviousePageSource);
            }

            return;
        }
    }
}
