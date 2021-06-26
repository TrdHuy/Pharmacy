using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage
{
    internal class MSW_SP_RefreshPageAction : MSW_SP_ButtonAction
    {
        public MSW_SP_RefreshPageAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            if (SPViewModel.CustomerOrderDetailItemSource.Count > 0)
            {
                var x = App.Current.ShowApplicationMessageBox("Bạn có chắc tạo mới?",
                 HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                 HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                 OwnerWindow.MainScreen,
                 "Thông báo");
                if (x == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultNo)
                {
                    return;
                }
            }

            bool refreshCustomer = true;
            bool refreshBill = true;
            SPViewModel.RefreshViewModel(refreshCustomer, refreshBill);

            return;
        }
    }
}
