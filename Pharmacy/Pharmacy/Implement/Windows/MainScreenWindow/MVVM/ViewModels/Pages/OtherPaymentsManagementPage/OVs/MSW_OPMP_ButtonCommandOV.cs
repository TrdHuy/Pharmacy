using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage.OVs
{
    internal class MSW_OPMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_OPMP_ButtonCommandOV");
       
        public CommandExecuterModel AddNewOtherPaymentButtonCommand { get; set; }
        public CommandExecuterModel EditOtherPaymentButtonCommand { get; set; }
        public CommandExecuterModel DeleteOtherPaymentButtonCommand { get; set; }
        public CommandExecuterModel ShowInvoiceButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_OPMP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            AddNewOtherPaymentButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_ADD_BUTTON
                , paramaters);
            });
            EditOtherPaymentButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_EDIT_BUTTON
                , paramaters);
            }); 
            DeleteOtherPaymentButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_DELETE_BUTTON
                , paramaters);
            });
            ShowInvoiceButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_SHOW_INVOICE_BUTTON
                , paramaters);
            });
        }

    }
}

