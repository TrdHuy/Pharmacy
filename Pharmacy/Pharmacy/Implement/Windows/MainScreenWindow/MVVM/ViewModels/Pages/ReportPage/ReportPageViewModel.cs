using Microsoft.Reporting.WinForms;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.Integration;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage
{
    public class ReportPageViewModel : MSW_BasePageViewModel
    {
        public RunInputCommand InitSellingReportButtonCommand { get; set; }
        public RunInputCommand InitComprehensiveReportButtonCommand { get; set; }
        public bool IsInitSellingReportButtonRunning
        {
            get
            {
                return _isInitSellingReportButtonRunning;
            }
            set
            {
                _isInitSellingReportButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }
        public bool IsInitComprehensiveReportButtonRunning
        {
            get
            {
                return _isInitComprehensiveReportButtonRunning;
            }
            set
            {
                _isInitComprehensiveReportButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }
        public DateTime SellingReportStartDate { get; set; }
        public DateTime SellingReportEndDate { get; set; }
        public DateTime ComprehensiveReportStartDate { get; set; }
        public DateTime ComprehensiveReportEndDate { get; set; }

        private static Logger L = new Logger("ReportPageViewModel");
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private bool _isInitSellingReportButtonRunning;
        private bool _isInitComprehensiveReportButtonRunning;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            SellingReportStartDate = DateTime.Today;
            SellingReportEndDate = DateTime.Today.AddDays(1);
            ComprehensiveReportStartDate = DateTime.Today;
            ComprehensiveReportEndDate = DateTime.Today.AddDays(1);
            InitSellingReportButtonCommand = new RunInputCommand(InitSellingReportButtonClickEvent);
            InitComprehensiveReportButtonCommand = new RunInputCommand(InitComprehensiveReportButtonClickEvent);
        }

        private void InitComprehensiveReportButtonClickEvent(object paramaters)
        {
            IsInitComprehensiveReportButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_RP_INIT_COMPREHENSIVE_REPORT_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
        }

        private void InitSellingReportButtonClickEvent(object paramaters)
        {
            IsInitSellingReportButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_RP_INIT_SELLING_REPORT_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
        }

        protected override void OnInitialized()
        {
        }

    }
}
