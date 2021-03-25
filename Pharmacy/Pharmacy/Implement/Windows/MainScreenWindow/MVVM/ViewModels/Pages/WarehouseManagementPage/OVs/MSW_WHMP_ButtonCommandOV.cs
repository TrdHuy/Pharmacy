using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs
{
    internal class MSW_WHMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_WHMP_ButtonCommandOV");

        public CommandModel AddNewWarehouseImportButtonCommand { get; set; }
        public CommandModel EditWarehouseImportButtonCommand { get; set; }
        public CommandModel DeleteWarehouseImportButtonCommand { get; set; }
        public CommandModel ShowInvoiceButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_WHMP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            AddNewWarehouseImportButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_ADD_BUTTON
                , paramaters);
            });
            EditWarehouseImportButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_EDIT_BUTTON
                , paramaters);
            });
            DeleteWarehouseImportButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_DELETE_BUTTON
                , paramaters);
            });
            ShowInvoiceButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_SHOW_INVOICE_BUTTON
                , paramaters);
            });
        }

    }
}

