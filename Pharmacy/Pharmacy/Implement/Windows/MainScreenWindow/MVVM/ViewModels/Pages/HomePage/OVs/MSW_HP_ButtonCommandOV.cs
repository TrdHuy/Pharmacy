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

        public CommandExecuterModel SellingCommand { get; set; }
        public CommandExecuterModel UserManagementCommand { get; set; }
        public CommandExecuterModel CustomerManagementCommand { get; set; }
        public CommandExecuterModel MedicineManagementCommand { get; set; }
        public CommandExecuterModel SupplierManagementCommand { get; set; }
        public CommandExecuterModel WarehouseManagementCommand { get; set; }
        public CommandExecuterModel InvoiceManagementCommand { get; set; }
        public CommandExecuterModel OtherPaymentsManagementCommand { get; set; }
        public CommandExecuterModel ReportCommand { get; set; }
        public CommandExecuterModel PersonalInfoCommand { get; set; }

        protected override Logger logger => L;

        public MSW_HP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SellingCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_SELLING_MANAGEMENT
                    , paramaters
                    , false);
            });

            UserManagementCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_USER_MANAGEMENT
                    , paramaters
                    , false);
            });

            CustomerManagementCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CUSTOMER_MANAGEMENT
                    , paramaters
                    , false);
            });

            MedicineManagementCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_MEDICINE_MANAGEMENT
                    , paramaters
                    , false);
            });

            SupplierManagementCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_SUPPLIER_MANAGEMENT
                    , paramaters
                    , false);
            });

            WarehouseManagementCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_WAREHOUSE_MANAGEMENT
                    , paramaters
                    , false);
            });

            InvoiceManagementCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_INVOICE_MANAGEMENT
                    , paramaters
                    , false);
            });

            OtherPaymentsManagementCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_OTHER_PAYMENTS_MANAGEMENT
                    , paramaters
                    , false);
            });

            ReportCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_REPORT
                    , paramaters
                    , false);
            });

            PersonalInfoCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO
                    , paramaters
                    , false);
            });
        }

    }
}
