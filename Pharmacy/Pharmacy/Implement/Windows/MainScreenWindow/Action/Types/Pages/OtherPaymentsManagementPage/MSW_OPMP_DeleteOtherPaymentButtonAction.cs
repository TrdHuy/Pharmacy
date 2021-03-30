using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage
{
    internal class MSW_OPMP_DeleteOtherPaymentButtonAction : MSW_OPMP_ButtonAction
    {
        private DataGrid otherPaymentsDataGrid;

        public MSW_OPMP_DeleteOtherPaymentButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            otherPaymentsDataGrid = DataTransfer[0] as DataGrid;

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa thanh toán này?",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                OwnerWindow.MainScreen,
                "Thông báo!");

            if (mesResult == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {

                SQLQueryCustodian sqlQueryObserver = new SQLQueryCustodian((queryResult) =>
                {
                    if (queryResult.MesResult == MessageQueryResult.Done)
                    {
                        App.Current.ShowApplicationMessageBox("Xóa thông tin thanh toán thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                        OwnerWindow.MainScreen,
                        "Thông báo!");

                        OPMPViewModel.OtherPaymentItemSource.RemoveAt(otherPaymentsDataGrid.SelectedIndex);
                    }
                });

                DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_OTHER_PAYMENT_DEACTIVE_CMD_KEY,
                    sqlQueryObserver,
                    OPMPViewModel.OtherPaymentItemSource[otherPaymentsDataGrid.SelectedIndex].PaymentID);

                return;
            }

            return;
        }
    }
}
