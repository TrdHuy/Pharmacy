using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Windows.BaseWindow.Action.Builder;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.MainMenu;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.AppInfoPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerInstantiationPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerModificationPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerDebtsPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.InvoiceManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.AddMedicinePage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.DiscountByMedicinePage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.ModifyMedicinePage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.ShowMedicineInfoPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.AddOtherPaymentPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.ModifyOtherPaymentPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.PersonalInfoPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.Report;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SettingPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.AddSupplierPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.ModifySupplierPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.SupplierDebtPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SupplierManagementPage.SupplierImportHistoryPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserInstantiationPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserModificationPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.AddWarehouseImportPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ModifyWarehouseImportPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ShowWarehouseImportInfoPage;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Factory
{
    internal class MSW_CommandExecuterBuilder : BaseCommandExecuterBuilder
    {
        public override ICommandExecuter BuildCommandExecuter(string keyTag, ILogger logger)
        {
            ICommandExecuter commandExecuter = null;

            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_MSW_NONADMIN_BUTTON:
                    commandExecuter = new MSW_NonAdminAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_NOT_IMPLEMENTED_BUTTON:
                    commandExecuter = new MSW_NotImplementedAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_NOT_SUPPORTED_BUTTON:
                    commandExecuter = new MSW_NotSupportedAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_HOME_PAGE:
                    commandExecuter = new MSW_HomePageButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO:
                    commandExecuter = new MSW_PersonalInfoAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SELLING_MANAGEMENT:
                    commandExecuter = new MSW_SellingAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_USER_MANAGEMENT:
                    commandExecuter = new MSW_UserManagementAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CUSTOMER_MANAGEMENT:
                    commandExecuter = new MSW_CustomerManagementAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SUPPLIER_MANAGEMENT:
                    commandExecuter = new MSW_SupplierManagementAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_INVOICE_MANAGEMENT:
                    commandExecuter = new MSW_InvoiceManagementAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MEDICINE_MANAGEMENT:
                    commandExecuter = new MSW_MedicineManagementAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OTHER_PAYMENTS_MANAGEMENT:
                    commandExecuter = new MSW_OtherPaymentsManagementAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WAREHOUSE_MANAGEMENT:
                    commandExecuter = new MSW_WarehouseManagementAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_REPORT:
                    commandExecuter = new MSW_ReportAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SETTING:
                    commandExecuter = new MSW_SettingAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_APP_INFO:
                    commandExecuter = new MSW_AppInfoAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MM_BUG_REPORT:
                    commandExecuter = new MSW_MM_BugReportButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MM_CONTACT_US:
                    commandExecuter = new MSW_MM_ContactUsButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
                default:
                    break;
            }

            return commandExecuter;
        }

        public override IViewModelCommandExecuter BuildViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger)
        {
            IViewModelCommandExecuter viewModelCommandExecuter = null;
            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_MSW_CMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_AddButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CIP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CIP_CameraButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CIP_CancleButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_DEBTS_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_DebtsButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_BILL_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_BillButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_RETURN_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_ReturnButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_EditButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_DeleteButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_HISTORY_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_HistoryButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CMoP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CMoP_CancleButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CMoP_CameraButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_PIP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_PIP_CancleButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_PIP_CameraButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_EditButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_DeleteUserButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_AddNewUserButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UMoP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UMoP_CancleButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UIP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UMoP_CameraButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UIP_CancleButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UIP_CameraButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DeleteMedicineButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_AddNewMedicineButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_ModifyMedicineButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_SHOW_INFO_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_ShowMedicineInfoButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_PRINT_MEDICINE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_PrintMedicineListButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_PROMO_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DiscountByMedicineButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_RETURN_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CDP_ReturnButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_PRINT_DEBTS_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CDP_PrintDebtsButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_BILL_DISPLAY_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CDP_BillDisplayButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_EDIT_ENABLER_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_EditEnablerButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_ADD_ORDER_DETAIL_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_AddOrderDetailButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_DELETE_ORDER_DETAIL_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_DeleteOrderDetailButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_REFRESH_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_RefreshButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_PRINT_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_PrintInvoiceButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_AMP_CameraButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_AMP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_AMP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_MMP_CameraButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_MMP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_MMP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_SMIP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_SMIP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_CREATE_NEW_PROMO_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DBMP_CreateNewPromoButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DBMP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DBMP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DBMP_DeleteButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_DeleteWarehouseImportButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_SP_AddOrderDetailAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_REMOVE_BUTTON:
                    viewModelCommandExecuter = new MSW_SP_RemoveOrderDetailAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_INSTANTIATE_BUTTON:
                    viewModelCommandExecuter = new MSW_SP_InstantiateNewOrderAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_REFRESH_BUTTON:
                    viewModelCommandExecuter = new MSW_SP_RefreshPageAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AddWarehouseImportButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_ModifyWarehouseImportButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SHOW_INFO_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_ShowWarehouseImportInfoButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SHOW_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_ShowInvoiceButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_BROWSE_INVOICE_IMAGE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AWIP_BrowseInvoiceImageButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AWIP_AddMedicineToListButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AWIP_DeleteWarehouseImportDetailButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AWIP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AWIP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_BROWSE_INVOICE_IMAGE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_MWIP_BrowseInvoiceImageButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_MWIP_AddMedicineToListButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_MWIP_DeleteWarehouseImportDetailButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_MWIP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_MWIP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SWIIP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_SWIIP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SWIIP_SHOW_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_SWIIP_ShowInvoiceButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_DeleteSupplierButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_AddSupplierButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_ModifySupplierButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SHOW_IMPORT_HISTORY_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_ShowImportHistoryButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_ASP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_ASP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_ASP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_ASP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_MSP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_MSP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_MSP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_MSP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_SIHP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_SHOW_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_SIHP_ShowInvoiceButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_SHOW_DEBT_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_SIHP_ShowSupplierDebButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_SDP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_SHOW_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_SDP_ShowInvoiceButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_DeleteOtherPaymentButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_AddOtherPaymentButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_ModifyOtherPaymentButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_SHOW_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_ShowInvoiceButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_AOPP_BROWSE_INVOICE_IMAGE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_AOPP_BrowseInvoiceImageButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_AOPP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_AOPP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_AOPP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_AOPP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_BROWSE_INVOICE_IMAGE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_MOPP_BrowseInvoiceImageButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_MOPP_CancelButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_MOPP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_IMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_IMP_EditButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_IMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_IMP_DeleteButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SeP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_SeP_CancleButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SeP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_SeP_SaveButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_RP_INIT_SELLING_REPORT_BUTTON:
                    viewModelCommandExecuter = new MSW_RP_InitSellingReportButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_RP_INIT_COMPREHENSIVE_REPORT_BUTTON:
                    viewModelCommandExecuter = new MSW_RP_InitComprehensiveReportButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_AIP_APP_UPDATE_BUTTON:
                    viewModelCommandExecuter = new MSW_AIP_AppUpdateButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_AIP_BUG_REPORT_BUTTON:
                    viewModelCommandExecuter = new MSW_AIP_BugReportButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_AIP_CONTACT_US_BUTTON:
                    viewModelCommandExecuter = new MSW_AIP_ContactUsButtonAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                default:
                    break;
            }
            return viewModelCommandExecuter;
        }

        public override ICommandExecuter BuildAlternativeCommandExecuterWhenBuilderIsLock(string keyTag, ILogger logger = null)
        {
            ICommandExecuter action;
            switch (keyTag)
            {
                default:
                    action = new MSW_LockedBuilderAlternativeAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, logger);
                    break;
            }
            return action;
        }
    }
}
