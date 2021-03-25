using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.OVs
{
    internal class MSW_SMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_SMP_ButtonCommandOV");

        public CommandModel AddNewSupplierButtonCommand { get; set; }
        public CommandModel EditSupplierButtonCommand { get; set; }
        public CommandModel DeleteSupplierButtonCommand { get; set; }
        public CommandModel ShowImportHistoryButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_SMP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            AddNewSupplierButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_SMP_ADD_BUTTON
                , paramaters);
            });
            EditSupplierButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_SMP_EDIT_BUTTON
                , paramaters);
            });
            DeleteSupplierButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_SMP_DELETE_BUTTON
                , paramaters);
            });
            ShowImportHistoryButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_SMP_SHOW_IMPORT_HISTORY_BUTTON
                , paramaters);
            });
        }

    }
}
