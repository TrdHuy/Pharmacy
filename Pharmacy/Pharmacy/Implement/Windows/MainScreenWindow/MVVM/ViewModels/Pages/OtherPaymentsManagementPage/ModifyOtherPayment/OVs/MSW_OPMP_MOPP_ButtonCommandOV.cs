using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage.ModifyOtherPayment.OVs
{
    internal class MSW_OPMP_MOPP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_OPMP_MOPP_ButtonCommandOV");
        private bool _isSaveButtonRunning;

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
        public RunInputCommand CancelButtonCommand { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand BrowseInvoiceButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_OPMP_MOPP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            CancelButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_CANCEL_BUTTON
                , paramaters);
            });
            SaveButtonCommand = new RunInputCommand((paramaters) =>
            {
                IsSaveButtonRunning = true;
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_SAVE_BUTTON
                , paramaters
                , new FactoryLocker(FactoryStatus.TaskHandling, true));
            });
            BrowseInvoiceButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_BROWSE_INVOICE_IMAGE_BUTTON
                , paramaters);
            });
        }

    }
}

