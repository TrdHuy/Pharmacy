using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.ShowWarehouseImportInfo.OVs
{
    internal class MSW_WHMP_SWIIP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_WHMP_SWIIP_ButtonCommandOV");
        
        public CommandExecuterModel CancelButtonCommand { get; set; }
        public CommandExecuterModel BrowseInvoiceImageButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_WHMP_SWIIP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            
            CancelButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_SWIIP_CANCEL_BUTTON
                , paramaters);
            });
            BrowseInvoiceImageButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_WHMP_SWIIP_SHOW_INVOICE_BUTTON
                , paramaters);
            });
        }

    }
}

