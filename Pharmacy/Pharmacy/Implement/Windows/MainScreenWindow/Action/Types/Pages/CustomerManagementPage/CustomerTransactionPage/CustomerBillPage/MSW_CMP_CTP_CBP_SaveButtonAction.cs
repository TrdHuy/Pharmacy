using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using System.Linq;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    internal class MSW_CMP_CTP_CBP_SaveButtonAction : MSW_CMP_CTP_CBP_ButtonAction
    {
        private SQLQueryCustodian _sqlQueryObserver;

        public MSW_CMP_CTP_CBP_SaveButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            if (CBPViewModel.CurrentOrderDetails.Count == 0 && CBPViewModel.MedicineOV.PaidAmount == 0)
            {
                DeleteCurrentCustomerOrder();
            }
            else
            {
                SaveCurrentCustomerOrder();
            }
            return;
        }

        private void SaveCurrentCustomerOrder()
        {
            if (CBPViewModel.IsOrderModified)
            {
                var mbRes = App.Current.ShowApplicationMessageBox("Bạn có đồng ý lưu hóa đơn này?",
               HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
               HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
               OwnerWindow.MainScreen,
               "Thông báo!!");

                if (mbRes == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
                {
                    _sqlQueryObserver = new SQLQueryCustodian(UpdateCustomerOrderDetailQueryCallback);

                    UpdateCustomerOderDetails();

                    DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.UPDATE_CUSTOMER_ORDER_DEATAIL_CMD_KEY,
                        PharmacyDefinitions.UPDATE_CUSTOMER_ORDER_DEATAIL_DELAY_TIME,
                        _sqlQueryObserver,
                        CBPViewModel.CurrentCustomerOrder
                        );
                }

                CBPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
            }
            else
            {
                CBPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
            }
        }

        private void UpdateCustomerOderDetails()
        {
            // Cập nhật lại order detail trong order của customer dựa theo thay đổi (OrderDetailOV)
            // từ phía người dùng
            foreach (tblOrderDetail orderDetail in CBPViewModel.CurrentCustomerOrder.tblOrderDetails)
            {
                var tempList = CBPViewModel.CurrentOrderDetails.Where(ov => ov.OrderDetailID == orderDetail.OrderDetailID).ToList();
                if (tempList.Count > 0)
                {
                    orderDetail.Quantity = tempList.First().Quantity;
                    orderDetail.TotalPrice = tempList.First().TotalPrice;
                }
                else
                {
                    orderDetail.IsActive = false;
                }
            }

            // Thêm mới order detail trong OrderDetailOV vào trong order của customer
            foreach (OrderDetailOV ov in CBPViewModel.CurrentOrderDetails)
            {
                if (ov.OrderDetailID == -1)
                {
                    var newOD = new tblOrderDetail()
                    {
                        IsActive = true,
                        Quantity = Convert.ToDouble(ov.Quantity),
                        TotalPrice = ov.TotalPrice,
                        UnitPrice = ov.UnitPrice,
                        MedicineID = ov.MedicineID,
                        PromoPercent = ov.PromoPercent,
                        UnitBidPrice = ov.UnitBidPrice
                    };
                    CBPViewModel.CurrentCustomerOrder.tblOrderDetails.Add(newOD);
                    CBPViewModel.CurrentCustomerOrder.TotalPrice += newOD.TotalPrice;
                }
            }

            CBPViewModel.CurrentCustomerOrder.PurchasePrice = CBPViewModel.MedicineOV.PaidAmount;
        }

        private void UpdateCustomerOrderDetailQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Cập nhật hóa đơn thành công!",
               HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
               HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
               OwnerWindow.MainScreen,
               "Thông báo!!");

                if (PageHost.PreviousePageSource != PageSource.NONE)
                {
                    PageHost.UpdateCurrentPageSource(PageHost.PreviousePageSource);
                }
                else
                {
                    PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_TRANSACTION_HISTORY_PAGE);
                }
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi cập nhật hóa đơn!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                OwnerWindow.MainScreen,
                "Thông báo!!");
            }

            CBPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;

        }

        private void DeleteCurrentCustomerOrder()
        {
            var mbRes = App.Current.ShowApplicationMessageBox("Danh sách sản phẩm hiện tại đang trống.\nBạn có muốn xóa hóa đơn này?",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                OwnerWindow.MainScreen,
                "Thông báo!!");

            if (mbRes == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {
                _sqlQueryObserver = new SQLQueryCustodian(SetDeactiveCustomerOrderQueryCallback);
                DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.SET_CUSTOMER_ORDER_DEACTIVE_CMD_KEY,
                    PharmacyDefinitions.SET_CUSTOMER_ORDER_DEACTIVE_DELAY_TIME,
                    _sqlQueryObserver,
                    CBPViewModel.CurrentCustomerOrder
                    );
            }
            else
            {
                CBPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
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

                if (PageHost.PreviousePageSource != PageSource.NONE)
                {
                    PageHost.UpdateCurrentPageSource(PageHost.PreviousePageSource);
                }
                else
                {
                    PageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_TRANSACTION_HISTORY_PAGE);
                }
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi xóa hóa đơn!",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                OwnerWindow.MainScreen,
                "Thông báo!!");
            }

            CBPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
        }
    }
}
