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
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage.ModifyOtherPayment.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage.ModifyOtherPayment
{
    internal class ModifyOtherPaymentPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("ModifyOtherPaymentPageViewModel");

        public MSW_OPMP_MOPP_ButtonCommandOV ButtonCommandOV { get; set; }
        public int PaymentDetailCheckingStatus { get; set; } = 1; //-1:Invalid 0:Checking 1:Valid
        public int PaymentPriceCheckingStatus { get; set; } = -1; //-1:Invalid 0:Checking 1:Valid
       
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

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_OPMP_MOPP_ButtonCommandOV(this);
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
