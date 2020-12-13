using Newtonsoft.Json;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages
{
    public class HomePageViewModel : AbstractViewModel
    {
        private IActionListener _keyActionListener = KeyActionListener.Instance;

        private string _currentTime;
        private PropertyObserver _userObserver;

        #region public properties

        public tblUser CurrentUser { get { return (tblUser)_userObserver.Value; } }
        public RunInputCommand SellingCommand { get; set; }
        public RunInputCommand UserManagementCommand { get; set; }
        public RunInputCommand CustomerManagementCommand { get; set; }
        public RunInputCommand MedicineManagementCommand { get; set; }
        public RunInputCommand SupplierManagementCommand { get; set; }
        public RunInputCommand WarehouseManagementCommand { get; set; }
        public RunInputCommand InvoiceManagementCommand { get; set; }
        public RunInputCommand OtherPaymentsManagementCommand { get; set; }
        public RunInputCommand ReportCommand { get; set; }
        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                InvalidateOwn();
            }
        }

        #endregion


        public HomePageViewModel()
        {

            _userObserver = new PropertyObserver(this,typeof(tblUser),"CurrentUser");
            App.Current.SubcribeProperty(_userObserver);

            ClockIntansiation();
            SellingCommand = new RunInputCommand(SellingButtonClickEvent);
            UserManagementCommand = new RunInputCommand(UserManagementButtonClickEvent);
            CustomerManagementCommand = new RunInputCommand(CustomerManagementButtonClickEvent);
            MedicineManagementCommand = new RunInputCommand(MedicineManagementButtonClickEvent);
            SupplierManagementCommand = new RunInputCommand(SupplierManagementButtonClickEvent);
            WarehouseManagementCommand = new RunInputCommand(WarehouseManagementButtonClickEvent);
            InvoiceManagementCommand = new RunInputCommand(InvoiceManagementButtonClickEvent);
            OtherPaymentsManagementCommand = new RunInputCommand(OtherPaymentsManagementButtonClickEvent);
            ReportCommand = new RunInputCommand(ReportButtonClickEvent);


        }
        protected override void InitPropertiesRegistry()
        {
            PropRegister("CurrentUser");
        }

        private void ClockIntansiation()
        {
            DispatcherTimer clock = new DispatcherTimer();
            clock.Interval = TimeSpan.FromMilliseconds(500);
            clock.Tick += new EventHandler(UpdateClock);
            clock.Start();
        }

        private void UpdateClock(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("hh:mm:ss");
        }

        #region PageSourceClickEvent
        private void SellingButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SELLING_MANAGEMENT
                , dataTransfer);
        }
        private void MedicineManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_MEDICINE_MANAGEMENT
                , dataTransfer);
        }
        private void UserManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_USER_MANAGEMENT
                , dataTransfer);
        }
        private void CustomerManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_CUSTOMER_MANAGEMENT
                , dataTransfer);
        }
        private void SupplierManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SUPPLIER_MANAGEMENT
                , dataTransfer);
        }
        private void WarehouseManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_WAREHOUSE_MANAGEMENT
                , dataTransfer);
        }
        private void InvoiceManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_INVOICE_MANAGEMENT
                , dataTransfer);
        }
        private void OtherPaymentsManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_OTHER_PAYMENTS_MANAGEMENT
                , dataTransfer);
        }
        private void ReportButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_REPORT
                , dataTransfer);
        }
        #endregion

    }
}
