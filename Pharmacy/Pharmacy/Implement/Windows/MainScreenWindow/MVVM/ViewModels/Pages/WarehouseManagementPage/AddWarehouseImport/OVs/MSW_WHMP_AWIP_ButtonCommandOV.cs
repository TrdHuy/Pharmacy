using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.AddWarehouseImport.OVs
{
    internal class MSW_WHMP_AWIP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_WHMP_AWIP_ButtonCommandOV");
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
                    _keyActionListener.LockMSW_ActionFactory(false, BuilderStatus.Unlock);
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
                    _keyActionListener.LockMSW_ActionFactory(false, BuilderStatus.Unlock);
                }
                InvalidateOwn();
            }
        }
        public CommandModel SaveButtonCommand { get; set; }
        public CommandModel CancelButtonCommand { get; set; }
        public CommandModel BrowseInvoiceImageButtonCommand { get; set; }
        public CommandModel AddMedicineToListButtonCommand { get; set; }
        public CommandModel DeleteMedicineFromListButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_WHMP_AWIP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            DeleteMedicineFromListButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON
                , paramaters);
            });
            AddMedicineToListButtonCommand = new CommandModel((paramaters) =>
            {
                IsAddImportDetailButtonRunning = true;
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON
                , paramaters
                , new BuilderLocker(BuilderStatus.TaskHandling,true));
            });
            SaveButtonCommand = new CommandModel((paramaters) =>
            {
                IsAddWarehouseImportButtonRunning = true;
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_SAVE_BUTTON
                , paramaters
                , new BuilderLocker(BuilderStatus.TaskHandling, true));
            });
            CancelButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_CANCEL_BUTTON
                , paramaters);
            });
            BrowseInvoiceImageButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_BROWSE_INVOICE_IMAGE_BUTTON
                , paramaters);
            });
        }

    }
}

