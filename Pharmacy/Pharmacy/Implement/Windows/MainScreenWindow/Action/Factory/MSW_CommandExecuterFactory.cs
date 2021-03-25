using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Windows.BaseWindow.Action.Factory;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative;
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
    internal class MSW_CommandExecuterFactory : BaseCommandExecuterFactory
    {
        public override ICommandExecuter CreateCommandExecuter(string keyTag, ILogger logger)
        {
            ICommandExecuter commandExecuter = null;

            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_MSW_NOT_IMPLEMENTED_BUTTON:
                    commandExecuter = new MSW_NotImplementedAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_NOT_SUPPORTED_BUTTON:
                    commandExecuter = new MSW_NotSupportedAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_HOME_PAGE:
                    commandExecuter = new MSW_HomePageButtonAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO:
                    commandExecuter = new MSW_PersonalInfoAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SELLING_MANAGEMENT:
                    commandExecuter = new MSW_SellingAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_USER_MANAGEMENT:
                    commandExecuter = new MSW_UserManagementAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CUSTOMER_MANAGEMENT:
                    commandExecuter = new MSW_CustomerManagementAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SUPPLIER_MANAGEMENT:
                    commandExecuter = new MSW_SupplierManagementAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_INVOICE_MANAGEMENT:
                    commandExecuter = new MSW_InvoiceManagementAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MEDICINE_MANAGEMENT:
                    commandExecuter = new MSW_MedicineManagementAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OTHER_PAYMENTS_MANAGEMENT:
                    commandExecuter = new MSW_OtherPaymentsManagementAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WAREHOUSE_MANAGEMENT:
                    commandExecuter = new MSW_WarehouseManagementAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_REPORT:
                    commandExecuter = new MSW_ReportAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SETTING:
                    commandExecuter = new MSW_SettingAction(logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_APP_INFO:
                    commandExecuter = new MSW_AppInfoAction(logger);
                    break;
                default:
                    commandExecuter = new MSW_NotImplementedAction(logger);
                    break;
            }

            return commandExecuter;
        }

        public override IViewModelCommandExecuter CreateViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger)
        {
            IViewModelCommandExecuter viewModelCommandExecuter = null;
            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_MSW_AIP_APP_UPDATE_BUTTON:
                    viewModelCommandExecuter = new MSW_AIP_AppUpdateButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_AddButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CIP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CIP_CameraButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CIP_CancleButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_DEBTS_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_DebtsButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_BILL_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_BillButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_RETURN_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_ReturnButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_EditButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_DeleteButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_HISTORY_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_HistoryButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CMoP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CMoP_CancleButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CMoP_CameraButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_PIP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_PIP_CancleButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_PIP_CameraButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_EditButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_DeleteUserButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_AddNewUserButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UMoP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UMoP_CancleButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UIP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UMoP_CameraButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UIP_CancleButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_UMP_UIP_CameraButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DeleteMedicineButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_AddNewMedicineButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_ModifyMedicineButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_SHOW_INFO_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_ShowMedicineInfoButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_PRINT_MEDICINE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_PrintMedicineListButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_PROMO_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DiscountByMedicineButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_RETURN_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CDP_ReturnButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_PRINT_DEBTS_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CDP_PrintDebtsButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_BILL_DISPLAY_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CDP_BillDisplayButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_EDIT_ENABLER_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_EditEnablerButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_ADD_ORDER_DETAIL_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_AddOrderDetailButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_DELETE_ORDER_DETAIL_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_DeleteOrderDetailButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_REFRESH_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_RefreshButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_PRINT_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_CMP_CTP_CBP_PrintInvoiceButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_AMP_CameraButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_AMP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_AMP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_CAMERA_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_MMP_CameraButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_MMP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_MMP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_SMIP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_SMIP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_CREATE_NEW_PROMO_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DBMP_CreateNewPromoButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DBMP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DBMP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_MMP_DBMP_DeleteButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_DeleteWarehouseImportButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_SP_AddOrderDetailAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_REMOVE_BUTTON:
                    viewModelCommandExecuter = new MSW_SP_RemoveOrderDetailAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_INSTANTIATE_BUTTON:
                    viewModelCommandExecuter = new MSW_SP_InstantiateNewOrderAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_REFRESH_BUTTON:
                    viewModelCommandExecuter = new MSW_SP_RefreshPageAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AddWarehouseImportButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_ModifyWarehouseImportButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SHOW_INFO_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_ShowWarehouseImportInfoButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SHOW_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_ShowInvoiceButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_BROWSE_INVOICE_IMAGE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AWIP_BrowseInvoiceImageButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AWIP_AddMedicineToListButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AWIP_DeleteWarehouseImportDetailButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AWIP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_AWIP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_BROWSE_INVOICE_IMAGE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_MWIP_BrowseInvoiceImageButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_MWIP_AddMedicineToListButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_MWIP_DeleteWarehouseImportDetailButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_MWIP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_MWIP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SWIIP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_SWIIP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SWIIP_SHOW_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_WHMP_SWIIP_ShowInvoiceButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_DeleteSupplierButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_AddSupplierButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_ModifySupplierButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SHOW_IMPORT_HISTORY_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_ShowImportHistoryButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_ASP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_ASP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_ASP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_ASP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_MSP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_MSP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_MSP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_MSP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_SIHP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_SHOW_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_SIHP_ShowInvoiceButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_SHOW_DEBT_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_SIHP_ShowSupplierDebButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_SDP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_SHOW_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_SMP_SDP_ShowInvoiceButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_DeleteOtherPaymentButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_ADD_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_AddOtherPaymentButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_ModifyOtherPaymentButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_SHOW_INVOICE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_ShowInvoiceButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_AOPP_BROWSE_INVOICE_IMAGE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_AOPP_BrowseInvoiceImageButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_AOPP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_AOPP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_AOPP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_AOPP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_BROWSE_INVOICE_IMAGE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_MOPP_BrowseInvoiceImageButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_CANCEL_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_MOPP_CancelButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_OPMP_MOPP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_IMP_EDIT_BUTTON:
                    viewModelCommandExecuter = new MSW_IMP_EditButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_IMP_DELETE_BUTTON:
                    viewModelCommandExecuter = new MSW_IMP_DeleteButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SeP_CANCLE_BUTTON:
                    viewModelCommandExecuter = new MSW_SeP_CancleButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SeP_SAVE_BUTTON:
                    viewModelCommandExecuter = new MSW_SeP_SaveButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_RP_INIT_SELLING_REPORT_BUTTON:
                    viewModelCommandExecuter = new MSW_RP_InitSellingReportButtonAction(viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_RP_INIT_COMPREHENSIVE_REPORT_BUTTON:
                    viewModelCommandExecuter = new MSW_RP_InitComprehensiveReportButtonAction(viewModel, logger);
                    break;
                default:
                    break;
            }
            return viewModelCommandExecuter;
        }

        public override ICommandExecuter CreateAlternativeCommandExecuterWhenFactoryIsLock(string keyTag, ILogger logger = null)
        {
            ICommandExecuter action;
            switch (keyTag)
            {
                default:
                    action = new MSW_AlternativeAction(logger);
                    break;
            }
            return action;
        }
    }
}
