using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.HomePage.OVs
{
    internal class MSW_HP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_HP_ButtonCommandOV");

        public RunInputCommand SellingCommand { get; set; }
        public RunInputCommand UserManagementCommand { get; set; }
        public RunInputCommand CustomerManagementCommand { get; set; }
        public RunInputCommand MedicineManagementCommand { get; set; }
        public RunInputCommand SupplierManagementCommand { get; set; }
        public RunInputCommand WarehouseManagementCommand { get; set; }
        public RunInputCommand InvoiceManagementCommand { get; set; }
        public RunInputCommand OtherPaymentsManagementCommand { get; set; }
        public RunInputCommand ReportCommand { get; set; }
        public RunInputCommand PersonalInfoCommand { get; set; }

        protected override Logger logger => L;

        public MSW_HP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SellingCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_SELLING_MANAGEMENT
                    , paramaters
                    , false);
            });

            UserManagementCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_USER_MANAGEMENT
                    , paramaters
                    , false);
            });

            CustomerManagementCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CUSTOMER_MANAGEMENT
                    , paramaters
                    , false);
            });

            MedicineManagementCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MEDICINE_MANAGEMENT
                    , paramaters
                    , false);
            });

            SupplierManagementCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_SUPPLIER_MANAGEMENT
                    , paramaters
                    , false);
            });

            WarehouseManagementCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WAREHOUSE_MANAGEMENT
                    , paramaters
                    , false);
            });

            InvoiceManagementCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_INVOICE_MANAGEMENT
                    , paramaters
                    , false);
            });

            OtherPaymentsManagementCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OTHER_PAYMENTS_MANAGEMENT
                    , paramaters
                    , false);
            });

            ReportCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_REPORT
                    , paramaters
                    , false);
            });

            PersonalInfoCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO
                    , paramaters
                    , false);
            });
        }

    }
}
