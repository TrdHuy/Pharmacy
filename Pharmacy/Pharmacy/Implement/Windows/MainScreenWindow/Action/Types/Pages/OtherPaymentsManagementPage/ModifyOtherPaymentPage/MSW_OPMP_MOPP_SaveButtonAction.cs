using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.ModifyOtherPaymentPage
{
    internal class MSW_OPMP_MOPP_SaveButtonAction : MSW_OPMP_MOPP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;

        public MSW_OPMP_MOPP_SaveButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            if (!MOPPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                MOPPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
                return;
            }

            tblOtherPayment payment = new tblOtherPayment();
            payment.PaymentID = MOPPViewModel.OtherPaymentDetail.PaymentID;
            payment.PaymentTime = MOPPViewModel.PaymentTime;
            payment.PaymentType = MOPPViewModel.PaymentType;
            payment.PaymentContent = MOPPViewModel.PaymentDetail;
            payment.TotalPrice = MOPPViewModel.PaymentPrice;
            payment.IsActive = true;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.MODIFY_OTHER_PAYMENT_CMD_KEY,
                PharmacyDefinitions.MODIFY_OTHER_PAYMENT_DELAY_TIME,
                _sqlCmdObserver,
                payment,
                MOPPViewModel.InvoiceImageURL);

            return;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Chỉnh sửa thanh toán thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi chỉnh sửa thanh toán. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            MOPPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
            PageHost.UpdateCurrentPageSource(PageSource.OTHER_PAYMENT_MANAGEMENT_PAGE);
        }
    }
}