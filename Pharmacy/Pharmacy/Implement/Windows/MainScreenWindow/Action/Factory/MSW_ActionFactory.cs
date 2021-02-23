using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Action;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerInstantiationPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerModificationPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerDebtsPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.AddMedicinePage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.DiscountByMedicinePage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.ModifyMedicinePage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.ShowMedicineInfoPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.AddOtherPaymentPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.ModifyOtherPaymentPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.PersonalInfoPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Factory
{
    public class MSW_ActionFactory : KeyActionFactory
    {
        protected override IAction CreateActionFromCurrentWindow(string keyTag)
        {
            IAction action;

            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_MSW_NOT_IMPLEMENTED_BUTTON:
                    action = new MSW_NotImplementedAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_NOT_SUPPORTED_BUTTON:
                    action = new MSW_NotSupportedAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_HOME_PAGE:
                    action = new MSW_HomePageButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO:
                    action = new MSW_PersonalInfoAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SELLING_MANAGEMENT:
                    action = new MSW_SellingAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_USER_MANAGEMENT:
                    action = new MSW_UserManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CUSTOMER_MANAGEMENT:
                    action = new MSW_CustomerManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SUPPLIER_MANAGEMENT:
                    action = new MSW_SupplierManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_INVOICE_MANAGEMENT:
                    action = new MSW_InvoiceManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MEDICINE_MANAGEMENT:
                    action = new MSW_MedicineManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OTHER_PAYMENTS_MANAGEMENT:
                    action = new MSW_OtherPaymentsManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WAREHOUSE_MANAGEMENT:
                    action = new MSW_WarehouseManagementAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_REPORT:
                    action = new MSW_Repor();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_SAVE_BUTTON:
                    action = new MSW_PIP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_CANCLE_BUTTON:
                    action = new MSW_PIP_CancleButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_PIP_CAMERA_BUTTON:
                    action = new MSW_PIP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_EDIT_BUTTON:
                    action = new MSW_UMP_EditButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_DELETE_BUTTON:
                    action = new MSW_UMP_DeleteUserButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_ADD_BUTTON:
                    action = new MSW_UMP_AddNewUserButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_SAVE_BUTTON:
                    action = new MSW_UMP_UMoP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CANCLE_BUTTON:
                    action = new MSW_UMP_UMoP_CancleButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_SAVE_BUTTON:
                    action = new MSW_UMP_UIP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CAMERA_BUTTON:
                    action = new MSW_UMP_UMoP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_CANCLE_BUTTON:
                    action = new MSW_UMP_UIP_CancleButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_CAMERA_BUTTON:
                    action = new MSW_UMP_UIP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DELETE_BUTTON:
                    action = new MSW_MMP_DeleteMedicineButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_ADD_BUTTON:
                    action = new MSW_MMP_AddNewMedicineButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_EDIT_BUTTON:
                    action = new MSW_MMP_ModifyMedicineButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_SHOW_INFO_BUTTON:
                    action = new MSW_MMP_ShowMedicineInfoButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_PROMO_BUTTON:
                    action = new MSW_MMP_DiscountByMedicineButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_ADD_BUTTON:
                    action = new MSW_CMP_AddButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_SAVE_BUTTON:
                    action = new MSW_CMP_CIP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CAMERA_BUTTON:
                    action = new MSW_CMP_CIP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CANCLE_BUTTON:
                    action = new MSW_CMP_CIP_CancleButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_DEBTS_BUTTON:
                    action = new MSW_CMP_CTP_DebtsButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_BILL_BUTTON:
                    action = new MSW_CMP_CTP_BillButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_RETURN_BUTTON:
                    action = new MSW_CMP_CTP_ReturnButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_RETURN_BUTTON:
                    action = new MSW_CMP_CTP_CDP_ReturnButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_PRINT_DEBTS_BUTTON:
                    action = new MSW_CMP_CTP_CDP_PrintDebtsButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CDP_BILL_DISPLAY_BUTTON:
                    action = new MSW_CMP_CTP_CDP_BillDisplayButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_EDIT_ENABLER_BUTTON:
                    action = new MSW_CMP_CTP_CBP_EditEnablerButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_SAVE_BUTTON:
                    action = new MSW_CMP_CTP_CBP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_ADD_ORDER_DETAIL_BUTTON:
                    action = new MSW_CMP_CTP_CBP_AddOrderDetailButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_DELETE_ORDER_DETAIL_BUTTON:
                    action = new MSW_CMP_CTP_CBP_DeleteOrderDetailButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CTP_CBP_REFRESH_BUTTON:
                    action = new MSW_CMP_CTP_CBP_RefreshButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_EDIT_BUTTON:
                    action = new MSW_CMP_EditButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_DELETE_BUTTON:
                    action = new MSW_CMP_DeleteButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_HISTORY_BUTTON:
                    action = new MSW_CMP_HistoryButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_SAVE_BUTTON:
                    action = new MSW_CMP_CMoP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_CANCLE_BUTTON:
                    action = new MSW_CMP_CMoP_CancleButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_CMP_CMoP_CAMERA_BUTTON:
                    action = new MSW_CMP_CMoP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CAMERA_BUTTON:
                    action = new MSW_MMP_AMP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CANCEL_BUTTON:
                    action = new MSW_MMP_AMP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_SAVE_BUTTON:
                    action = new MSW_MMP_AMP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_CAMERA_BUTTON:
                    action = new MSW_MMP_MMP_CameraButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_CANCEL_BUTTON:
                    action = new MSW_MMP_MMP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_MMP_SAVE_BUTTON:
                    action = new MSW_MMP_MMP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_SMIP_CANCEL_BUTTON:
                    action = new MSW_MMP_SMIP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_CANCEL_BUTTON:
                    action = new MSW_MMP_DBMP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_SAVE_BUTTON:
                    action = new MSW_MMP_DBMP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_MMP_DBMP_DELETE_BUTTON:
                    action = new MSW_MMP_DBMP_DeleteButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_DELETE_BUTTON:
                    action = new MSW_WHMP_DeleteWarehouseImportButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_ADD_BUTTON:
                    action = new MSW_SP_AddOrderDetailAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_REMOVE_BUTTON:
                    action = new MSW_SP_RemoveOrderDetailAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_INSTANTIATE_BUTTON:
                    action = new MSW_SP_InstantiateNewOrderAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SP_REFRESH_BUTTON:
                    action = new MSW_SP_RefreshPageAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_ADD_BUTTON:
                    action = new MSW_WHMP_AddWarehouseImportButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_EDIT_BUTTON:
                    action = new MSW_WHMP_ModifyWarehouseImportButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SHOW_INFO_BUTTON:
                    action = new MSW_WHMP_ShowWarehouseImportInfoButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SHOW_INVOICE_BUTTON:
                    action = new MSW_WHMP_ShowInvoiceButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_BROWSE_INVOICE_IMAGE_BUTTON:
                    action = new MSW_WHMP_AWIP_BrowseInvoiceImageButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    action = new MSW_WHMP_AWIP_AddMedicineToListButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    action = new MSW_WHMP_AWIP_DeleteWarehouseImportDetailButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_CANCEL_BUTTON:
                    action = new MSW_WHMP_AWIP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_AWIP_SAVE_BUTTON:
                    action = new MSW_WHMP_AWIP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_BROWSE_INVOICE_IMAGE_BUTTON:
                    action = new MSW_WHMP_MWIP_BrowseInvoiceImageButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    action = new MSW_WHMP_MWIP_AddMedicineToListButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON:
                    action = new MSW_WHMP_MWIP_DeleteWarehouseImportDetailButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_CANCEL_BUTTON:
                    action = new MSW_WHMP_MWIP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_MWIP_SAVE_BUTTON:
                    action = new MSW_WHMP_MWIP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SWIIP_CANCEL_BUTTON:
                    action = new MSW_WHMP_SWIIP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_WHMP_SWIIP_SHOW_INVOICE_BUTTON:
                    action = new MSW_WHMP_SWIIP_ShowInvoiceButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_DELETE_BUTTON:
                    action = new MSW_SMP_DeleteSupplierButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_ADD_BUTTON:
                    action = new MSW_SMP_AddSupplierButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_EDIT_BUTTON:
                    action = new MSW_SMP_ModifySupplierButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SHOW_IMPORT_HISTORY_BUTTON:
                    action = new MSW_SMP_ShowImportHistoryButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_ASP_CANCEL_BUTTON:
                    action = new MSW_SMP_ASP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_ASP_SAVE_BUTTON:
                    action = new MSW_SMP_ASP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_MSP_CANCEL_BUTTON:
                    action = new MSW_SMP_MSP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_MSP_SAVE_BUTTON:
                    action = new MSW_SMP_MSP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_CANCEL_BUTTON:
                    action = new MSW_SMP_SIHP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_SHOW_INVOICE_BUTTON:
                    action = new MSW_SMP_SIHP_ShowInvoiceButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SIHP_SHOW_DEBT_BUTTON:
                    action = new MSW_SMP_SIHP_ShowSupplierDebButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_CANCEL_BUTTON:
                    action = new MSW_SMP_SDP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_SMP_SDP_SHOW_INVOICE_BUTTON:
                    action = new MSW_SMP_SDP_ShowInvoiceButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_DELETE_BUTTON:
                    action = new MSW_OPMP_DeleteOtherPaymentButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_ADD_BUTTON:
                    action = new MSW_OPMP_AddOtherPaymentButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_EDIT_BUTTON:
                    action = new MSW_OPMP_ModifyOtherPaymentButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_SHOW_INVOICE_BUTTON:
                    action = new MSW_OPMP_ShowInvoiceButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_AOPP_BROWSE_INVOICE_IMAGE_BUTTON:
                    action = new MSW_OPMP_AOPP_BrowseInvoiceImageButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_AOPP_CANCEL_BUTTON:
                    action = new MSW_OPMP_AOPP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_AOPP_SAVE_BUTTON:
                    action = new MSW_OPMP_AOPP_SaveButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_BROWSE_INVOICE_IMAGE_BUTTON:
                    action = new MSW_OPMP_MOPP_BrowseInvoiceImageButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_CANCEL_BUTTON:
                    action = new MSW_OPMP_MOPP_CancelButtonAction();
                    break;
                case KeyFeatureTag.KEY_TAG_MSW_OPMP_MOPP_SAVE_BUTTON:
                    action = new MSW_OPMP_MOPP_SaveButtonAction();
                    break;
                default:
                    action = null;
                    break;
            }
            return action;
        }

        protected override IAction CreateAlternativeActionFromCurrentWindow(string keyTag)
        {
            IAction action;
            switch (keyTag)
            {
                default:
                    action = new MSW_AlternativeAction();
                    break;
            }
            return action;
        }

    }


}
