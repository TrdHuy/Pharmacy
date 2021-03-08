using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    public class MSW_CMP_CTP_CBP_SaveButtonAction : Base.UIEventHandler.Action.IAction
    {
        private CustomerBillPageViewModel _viewModel;
        private SQLQueryCustodian _sqlQueryObserver;
        private MSW_PageController _pageHost = MSW_PageController.Instance;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as CustomerBillPageViewModel;

            if (_viewModel.CurrentOrderDetails.Count == 0)
            {
                DeleteCurrentCustomerOrder();
            }
            else
            {
                SaveCurrentCustomerOrder();
            }
            return true;
        }

        private void SaveCurrentCustomerOrder()
        {
            if (_viewModel.IsOrderModified)
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
                        _viewModel.CurrentCustomerOrder
                        );
                }
            }
            else
            {
                _viewModel.ButtonCommandOV.IsSaveButtonRunning = false;
            }
        }

        private void UpdateCustomerOderDetails()
        {
            // Cập nhật lại order detail trong order của customer dựa theo thay đổi (OrderDetailOV)
            // từ phía người dùng
            foreach (tblOrderDetail orderDetail in _viewModel.CurrentCustomerOrder.tblOrderDetails)
            {
                var tempList = _viewModel.CurrentOrderDetails.Where(ov => ov.OrderDetailID == orderDetail.OrderDetailID).ToList();
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
            foreach (OrderDetailOV ov in _viewModel.CurrentOrderDetails)
            {
                if (ov.OrderDetailID == -1)
                {
                    var newOD = new tblOrderDetail()
                    {
                        IsActive = true,
                        Quantity = Convert.ToDouble(ov.Quantity),
                        TotalPrice = ov.TotalPrice,
                        UnitPrice = ov.UnitPrice,
                        MedicineID = ov.MedicineID
                    };
                    _viewModel.CurrentCustomerOrder.tblOrderDetails.Add(newOD);
                    _viewModel.CurrentCustomerOrder.TotalPrice += newOD.TotalPrice;
                }
            }

            _viewModel.CurrentCustomerOrder.PurchasePrice = _viewModel.MedicineOV.PaidAmount;
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

                if (_pageHost.PreviousePageSource != PageSource.NONE)
                {
                    _pageHost.UpdateCurrentPageSource(_pageHost.PreviousePageSource);
                }
                else
                {
                    _pageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_TRANSACTION_HISTORY_PAGE);
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

            _viewModel.ButtonCommandOV.IsSaveButtonRunning = false;

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
                    _viewModel.CurrentCustomerOrder
                    );
            }
            else
            {
                _viewModel.ButtonCommandOV.IsSaveButtonRunning = false;
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

                if (_pageHost.PreviousePageSource != PageSource.NONE)
                {
                    _pageHost.UpdateCurrentPageSource(_pageHost.PreviousePageSource);
                }
                else
                {
                    _pageHost.UpdateCurrentPageSource(PageSource.CUSTOMER_TRANSACTION_HISTORY_PAGE);
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

            _viewModel.ButtonCommandOV.IsSaveButtonRunning = false;
        }
    }
}
