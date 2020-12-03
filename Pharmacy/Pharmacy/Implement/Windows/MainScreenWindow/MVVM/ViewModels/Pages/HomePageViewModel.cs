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
        public RunInputCommand PersonalInfoCommand { get; set; }
        public RunInputCommand BusinessManagementCommand { get; set; }
        public RunInputCommand StaffManagementCommand { get; set; }
        public RunInputCommand CustomerManagementCommand { get; set; }
        public RunInputCommand VendorManagementCommand { get; set; }
        public RunInputCommand ReportCommand { get; set; }
        public RunInputCommand SaleManagementCommand { get; set; }
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
            PersonalInfoCommand = new RunInputCommand(PersonalInfoButtonClickEvent);
            BusinessManagementCommand = new RunInputCommand(BussinessManagementButtonClickEvent);
            StaffManagementCommand = new RunInputCommand(StaffManagementButtonClickEvent);
            CustomerManagementCommand = new RunInputCommand(CustomerManagementButtonClickEvent);
            VendorManagementCommand = new RunInputCommand(VendorManagementButtonClickEvent);
            SaleManagementCommand = new RunInputCommand(SaleManagementButtonClickEvent);
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
        private void PersonalInfoButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO
                , dataTransfer);
        }
        private void BussinessManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_BUSINESS_MANAGEMENT
                , dataTransfer);
        }
        private void StaffManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_STAFF_MANAGEMENT
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
        private void VendorManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_VENDOR_MANAGEMENT
                , dataTransfer);
        }
        private void SaleManagementButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SALE_MANAGEMENT
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
