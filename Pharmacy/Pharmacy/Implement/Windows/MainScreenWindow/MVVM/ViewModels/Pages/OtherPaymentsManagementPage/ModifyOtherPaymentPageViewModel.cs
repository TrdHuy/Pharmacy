using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage
{
    public class ModifyOtherPaymentPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("ModifyOtherPaymentPageViewModel");

        public RunInputCommand CancelButtonCommand { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand BrowseInvoiceButtonCommand { get; set; }
        public int PaymentDetailCheckingStatus { get; set; } = 1; //-1:Invalid 0:Checking 1:Valid
        public int PaymentPriceCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
        public bool IsSaveButtonRunning
        {
            get
            {
                return _isSaveButtonRunning;
            }
            set
            {
                _isSaveButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, FactoryStatus.Unlock);
                }
                InvalidateOwn();
            }
        }
        public bool IsSaveButtonCanPerform
        {
            get
            {
                return PaymentDetailCheckingStatus == 1
                    && PaymentPriceCheckingStatus == 1;
            }
        }
        public DateTime PaymentTime { get; set; }
        public int PaymentType { get; set; }
        public string PaymentDetail
        {
            get
            {
                return _paymentDetail;
            }
            set
            {
                _paymentDetail = value;
                InvalidateOwn();
                CheckPaymentDetail();
            }
        }
        public decimal PaymentPrice
        {
            get
            {
                return _paymentPrice;
            }
            set
            {
                _paymentPrice = value;
                InvalidateOwn();
                CheckPaymentPrice();
            }
        }
        public string InvoiceImageURL
        {
            get
            {
                return _invoiceImageURL;
            }
            set
            {
                _invoiceImageURL = value;
                InvalidateOwn();
            }
        }
        public tblOtherPayment OtherPaymentDetail { get; set; }

        private string _paymentDetail = "";
        private string _invoiceImageURL = "";
        private decimal _paymentPrice;
        private bool _isSaveButtonRunning;
        private KeyActionListener _keyActionListener = KeyActionListener.Current;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            CancelButtonCommand = new RunInputCommand(CancelButtonClickEvent);
            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            BrowseInvoiceButtonCommand = new RunInputCommand(BrowseInvoiceButtonClickEvent);
            UpdateData();
        }

        protected override void OnInitialized()
        {
        }

        private void UpdateData()
        {
            OtherPaymentDetail = MSW_DataFlowHost.Current.CurrentSelectedOtherPayment;
            PaymentPrice = OtherPaymentDetail.TotalPrice;
            PaymentTime = OtherPaymentDetail.PaymentTime;
            PaymentType = OtherPaymentDetail.PaymentType;
            PaymentDetail = OtherPaymentDetail.PaymentContent;
        }

        private void BrowseInvoiceButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_BROWSE_INVOICE_IMAGE_BUTTON
                , dataTransfer);
        }

        private void SaveButtonClickEvent(object paramaters)
        {
            IsSaveButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_SAVE_BUTTON
                , dataTransfer
                , new FactoryLocker(FactoryStatus.TaskHandling, true));
        }

        private void CancelButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_CANCEL_BUTTON
                , dataTransfer);
        }

        private void CheckPaymentDetail()
        {
            if (PaymentDetail.Trim().Length > 0)
                PaymentDetailCheckingStatus = 1;
            else
                PaymentDetailCheckingStatus = -1;
            Invalidate("PaymentDetailCheckingStatus");
        }

        private void CheckPaymentPrice()
        {
            if (PaymentPrice > 0)
                PaymentPriceCheckingStatus = 1;
            else
                PaymentPriceCheckingStatus = -1;
            Invalidate("PaymentPriceCheckingStatus");
        }
    }

}
