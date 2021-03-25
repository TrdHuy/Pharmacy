using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.ModifyWarehouseImport.OVs
{
    internal class MSW_WHMP_MWIP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_WHMP_MWIP_ButtonCommandOV");
        private bool _isAddImportDetailButtonRunning;
        private bool _isAddWarehouseImportButtonRunning;

        public bool IsAddImportDetailButtonRunning
        {
            get { return _isAddImportDetailButtonRunning; }
            set
            {
                _isAddImportDetailButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, FactoryStatus.Unlock);
                }
                InvalidateOwn();
            }
        }
        public bool IsAddWarehouseImportButtonRunning
        {
            get { return _isAddWarehouseImportButtonRunning; }
            set
            {
                _isAddWarehouseImportButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, FactoryStatus.Unlock);
                }
                InvalidateOwn();
            }
        }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CancelButtonCommand { get; set; }
        public RunInputCommand BrowseInvoiceImageButtonCommand { get; set; }
        public RunInputCommand AddMedicineToListButtonCommand { get; set; }
        public RunInputCommand DeleteMedicineFromListButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_WHMP_MWIP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            DeleteMedicineFromListButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON
                , paramaters);
            });
            AddMedicineToListButtonCommand = new RunInputCommand((paramaters) =>
            {
                IsAddImportDetailButtonRunning = true;
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON
                , paramaters
                , new FactoryLocker(FactoryStatus.TaskHandling, true));
            });
            SaveButtonCommand = new RunInputCommand((paramaters) =>
            {
                IsAddWarehouseImportButtonRunning = true;
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_SAVE_BUTTON
                , paramaters
                , new FactoryLocker(FactoryStatus.TaskHandling, true));
            });
            CancelButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_CANCEL_BUTTON
                , paramaters);
            });
            BrowseInvoiceImageButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_BROWSE_INVOICE_IMAGE_BUTTON
                , paramaters);
            });
        }

    }
}

