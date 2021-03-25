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

        public CommandModel SellingCommand { get; set; }
        public CommandModel UserManagementCommand { get; set; }
        public CommandModel CustomerManagementCommand { get; set; }
        public CommandModel MedicineManagementCommand { get; set; }
        public CommandModel SupplierManagementCommand { get; set; }
        public CommandModel WarehouseManagementCommand { get; set; }
        public CommandModel InvoiceManagementCommand { get; set; }
        public CommandModel OtherPaymentsManagementCommand { get; set; }
        public CommandModel ReportCommand { get; set; }
        public CommandModel PersonalInfoCommand { get; set; }

        protected override Logger logger => L;

        public MSW_HP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SellingCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_SELLING_MANAGEMENT
                    , paramaters
                    , false);
            });

            UserManagementCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_USER_MANAGEMENT
                    , paramaters
                    , false);
            });

            CustomerManagementCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CUSTOMER_MANAGEMENT
                    , paramaters
                    , false);
            });

            MedicineManagementCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MEDICINE_MANAGEMENT
                    , paramaters
                    , false);
            });

            SupplierManagementCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_SUPPLIER_MANAGEMENT
                    , paramaters
                    , false);
            });

            WarehouseManagementCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_WAREHOUSE_MANAGEMENT
                    , paramaters
                    , false);
            });

            InvoiceManagementCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_INVOICE_MANAGEMENT
                    , paramaters
                    , false);
            });

            OtherPaymentsManagementCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_OTHER_PAYMENTS_MANAGEMENT
                    , paramaters
                    , false);
            });

            ReportCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_REPORT
                    , paramaters
                    , false);
            });

            PersonalInfoCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO
                    , paramaters
                    , false);
            });
        }

    }
}
