﻿using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.AddOtherPaymentPage
{
    public class MSW_OPMP_AOPP_SaveButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private AddOtherPaymentPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as AddOtherPaymentPageViewModel;
            if (!_viewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                _viewModel.IsSaveButtonRunning = false;
                return false;
            }

            tblOtherPayment payment = new tblOtherPayment();
            payment.PaymentTime = _viewModel.PaymentTime;
            payment.PaymentType = _viewModel.PaymentType;
            payment.PaymentContent = _viewModel.PaymentDetail;
            payment.TotalPrice = _viewModel.PaymentPrice;
            payment.IsActive = true;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.ADD_OTHER_PAYMENT_CMD_KEY,
                PharmacyDefinitions.ADD_NEW_OTHER_PAYMENT_DELAY_TIME,
                _sqlCmdObserver,
                payment,
                _viewModel.InvoiceImageURL);

            return true;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Thêm thanh toán thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi thêm thanh toán. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            _viewModel.IsSaveButtonRunning = false;
            _pageHost.UpdateCurrentPageSource(PageSource.OTHER_PAYMENT_MANAGEMENT_PAGE);
        }
    }
}