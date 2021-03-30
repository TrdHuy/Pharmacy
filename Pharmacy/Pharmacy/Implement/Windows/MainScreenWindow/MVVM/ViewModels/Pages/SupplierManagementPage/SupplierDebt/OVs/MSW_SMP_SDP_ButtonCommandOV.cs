using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SupplierManagementPage.SupplierDebt.OVs
{
    internal class MSW_SMP_SDP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_SMP_SDP_ButtonCommandOV");

        public CommandExecuterModel CancelButtonCommand { get; set; }
        public CommandExecuterModel PrintDebtButtonCommand { get; set; }
        public CommandExecuterModel ShowInvoiceButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_SMP_SDP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            ShowInvoiceButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_SHOW_INVOICE_BUTTON
                , paramaters);
            });
            PrintDebtButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_PRINT_DEBT_BUTTON
                , paramaters);
            });
            CancelButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_CANCEL_BUTTON
                , paramaters);
            });
        }

    }
}
