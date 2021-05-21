using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage.OVs;
using System;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage
{
    internal class ReportPageViewModel : MSW_BasePageViewModel
    {
        public MSW_RP_ButtonCommandOV ButtonCommandOV { get; set; }
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
                    _keyActionListener.LockMSW_ActionFactory(false, BuilderStatus.Unlock);
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
                    _keyActionListener.LockMSW_ActionFactory(false, BuilderStatus.Unlock);
                }
                InvalidateOwn();
            }
        }
        public DateTime SellingReportStartDate { get; set; }
        public DateTime SellingReportEndDate { get; set; }
        public DateTime ComprehensiveReportStartDate { get; set; }
        public DateTime ComprehensiveReportEndDate { get; set; }

        private static Logger L = new Logger("ReportPageViewModel");
        private KeyActionListener _keyActionListener = KeyActionListener.Current;
        private bool _isInitSellingReportButtonRunning;
        private bool _isInitComprehensiveReportButtonRunning;

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            SellingReportStartDate = DateTime.Today;
            SellingReportEndDate = DateTime.Today;
            ComprehensiveReportStartDate = DateTime.Today;
            ComprehensiveReportEndDate = DateTime.Today;

            ButtonCommandOV = new MSW_RP_ButtonCommandOV(this);
        }

        protected override void OnInitialized()
        {
        }

    }
}
