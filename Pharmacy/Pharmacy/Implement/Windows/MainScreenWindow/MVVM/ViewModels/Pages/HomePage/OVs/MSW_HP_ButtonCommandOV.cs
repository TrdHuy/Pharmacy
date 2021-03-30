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

        public NavigationCommandExecuterModel SellingCommand { get; set; }
        public NavigationCommandExecuterModel UserManagementCommand { get; set; }
        public NavigationCommandExecuterModel CustomerManagementCommand { get; set; }
        public NavigationCommandExecuterModel MedicineManagementCommand { get; set; }
        public NavigationCommandExecuterModel SupplierManagementCommand { get; set; }
        public NavigationCommandExecuterModel WarehouseManagementCommand { get; set; }
        public NavigationCommandExecuterModel InvoiceManagementCommand { get; set; }
        public NavigationCommandExecuterModel OtherPaymentsManagementCommand { get; set; }
        public NavigationCommandExecuterModel ReportCommand { get; set; }
        public NavigationCommandExecuterModel PersonalInfoCommand { get; set; }

        protected override Logger logger => L;

        public MSW_HP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SellingCommand = new NavigationCommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_SELLING_MANAGEMENT
                    , paramaters
                    , false);
            });

            UserManagementCommand = new NavigationCommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_USER_MANAGEMENT
                    , paramaters
                    , false);
            });

            CustomerManagementCommand = new NavigationCommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CUSTOMER_MANAGEMENT
                    , paramaters
                    , false);
            });

            MedicineManagementCommand = new NavigationCommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_MEDICINE_MANAGEMENT
                    , paramaters
                    , false);
            });

            SupplierManagementCommand = new NavigationCommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_SUPPLIER_MANAGEMENT
                    , paramaters
                    , false);
            });

            WarehouseManagementCommand = new NavigationCommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_WAREHOUSE_MANAGEMENT
                    , paramaters
                    , false);
            });

            InvoiceManagementCommand = new NavigationCommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_INVOICE_MANAGEMENT
                    , paramaters
                    , false);
            });

            OtherPaymentsManagementCommand = new NavigationCommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_OTHER_PAYMENTS_MANAGEMENT
                    , paramaters
                    , false);
            });

            ReportCommand = new NavigationCommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_REPORT
                    , paramaters
                    , false);
            });

            PersonalInfoCommand = new NavigationCommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO
                    , paramaters
                    , false);
            });
        }

    }
}
