using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.InvoiceManagementPage
{
    internal class MSW_IMP_DeleteButtonAction : MSW_IMP_ButtonAction
    {
        private SQLQueryCustodian _sqlQueryObserver;

        public MSW_IMP_DeleteButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        public override void ExecuteCommand()
        {
            if(IMPViewModel.CurrentSelectedOrderOV != null)
            {
                var x = App.Current.ShowApplicationMessageBox("Bạn có muốn xóa hóa đơn này?",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo");
                if(x == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
                {
                    _sqlQueryObserver = new SQLQueryCustodian(SetDeactiveCustomerOrderQueryCallback);
                    DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_CUSTOMER_ORDER_DEACTIVE_CMD_KEY,
                        _sqlQueryObserver,
                        IMPViewModel.CurrentSelectedOrderOV.Order
                        );
                }
            }
        }

        private void SetDeactiveCustomerOrderQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Xóa hóa đơn thành công!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                OwnerWindow.MainScreen,
                "Thông báo!!");

                IMPViewModel.CustomerOrdersItemSource.Remove(IMPViewModel.CurrentSelectedOrderOV);
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi xóa hóa đơn!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                OwnerWindow.MainScreen,
                "Thông báo!!");
            }
        }
    }
}
