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
       
        public CommandModel AddNewOtherPaymentButtonCommand { get; set; }
        public CommandModel EditOtherPaymentButtonCommand { get; set; }
        public CommandModel DeleteOtherPaymentButtonCommand { get; set; }
        public CommandModel ShowInvoiceButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_OPMP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            AddNewOtherPaymentButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_ADD_BUTTON
                , paramaters);
            });
            EditOtherPaymentButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_EDIT_BUTTON
                , paramaters);
            }); 
            DeleteOtherPaymentButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_DELETE_BUTTON
                , paramaters);
            });
            ShowInvoiceButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_SHOW_INVOICE_BUTTON
                , paramaters);
            });
        }

    }
}

