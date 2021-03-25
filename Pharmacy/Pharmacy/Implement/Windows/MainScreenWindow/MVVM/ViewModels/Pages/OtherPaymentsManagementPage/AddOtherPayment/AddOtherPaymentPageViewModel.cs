using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage.AddOtherPayment.OVs;
using System;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage.AddOtherPayment
{
    internal class AddOtherPaymentPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("AddOtherPaymentPageViewModel");

        public MSW_OPMP_AOPP_ButtonCommandOV ButtonCommandOV { get; set; }
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

        private string _paymentDetail = "";
        private string _invoiceImageURL = "";
        private decimal _paymentPrice;
        private bool _isSaveButtonRunning;
        private KeyActionListener _keyActionListener = KeyActionListener.Current;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_OPMP_AOPP_ButtonCommandOV(this);
            PaymentPrice = 0;
            PaymentTime = DateTime.Now;
            PaymentType = 0;
            PaymentDetail = "";
        }

        protected override void OnInitialized()
        {
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
