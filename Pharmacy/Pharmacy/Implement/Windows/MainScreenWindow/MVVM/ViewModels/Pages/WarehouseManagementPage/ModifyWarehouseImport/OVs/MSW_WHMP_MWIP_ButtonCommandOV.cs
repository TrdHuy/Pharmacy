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
        public CommandExecuterModel SaveButtonCommand { get; set; }
        public CommandExecuterModel CancelButtonCommand { get; set; }
        public CommandExecuterModel BrowseInvoiceImageButtonCommand { get; set; }
        public CommandExecuterModel AddMedicineToListButtonCommand { get; set; }
        public CommandExecuterModel DeleteMedicineFromListButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_WHMP_MWIP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            DeleteMedicineFromListButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON
                , paramaters);
            });
            AddMedicineToListButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsAddImportDetailButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON
                , paramaters
                , new BuilderLocker(BuilderStatus.TaskHandling, true)) as ICommandExecuter;
            });
            SaveButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsAddWarehouseImportButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_SAVE_BUTTON
                , paramaters
                , new BuilderLocker(BuilderStatus.TaskHandling, true)) as ICommandExecuter;
            });
            CancelButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_CANCEL_BUTTON
                , paramaters);
            });
            BrowseInvoiceImageButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_BROWSE_INVOICE_IMAGE_BUTTON
                , paramaters);
            });
        }

    }
}

