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
       
        public RunInputCommand AddNewOtherPaymentButtonCommand { get; set; }
        public RunInputCommand EditOtherPaymentButtonCommand { get; set; }
        public RunInputCommand DeleteOtherPaymentButtonCommand { get; set; }
        public RunInputCommand ShowInvoiceButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_OPMP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            AddNewOtherPaymentButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_ADD_BUTTON
                , paramaters);
            });
            EditOtherPaymentButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_EDIT_BUTTON
                , paramaters);
            }); 
            DeleteOtherPaymentButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_DELETE_BUTTON
                , paramaters);
            });
            ShowInvoiceButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OPMP_SHOW_INVOICE_BUTTON
                , paramaters);
            });
        }

    }
}

