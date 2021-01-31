using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.UIEventHandler
{
    class KeyFeatureTag
    {
        public const string KEY_TAG_LSW_LOGIN_FEATURE = "lsw_login_feature";
        public const string KEY_TAG_LSW_BUG_REPORT_FEATURE = "lsw_bug_report_feature";
        public const string KEY_TAG_LSW_CUSTOMER_SERVICE_FEATURE = "lsw_customer_service_feature";

        public const string KEY_TAG_MSW_HOME_PAGE = "msw_home_page";
        public const string KEY_TAG_MSW_PERSONAL_INFO = "msw_personal_info";
        public const string KEY_TAG_MSW_SELLING_MANAGEMENT = "msw_selling_management";
        public const string KEY_TAG_MSW_CUSTOMER_MANAGEMENT = "msw_customer_management";
        public const string KEY_TAG_MSW_USER_MANAGEMENT = "msw_user_management";
        public const string KEY_TAG_MSW_SUPPLIER_MANAGEMENT = "msw_supplier_management";
        public const string KEY_TAG_MSW_INVOICE_MANAGEMENT = "msw_invoice_management";
        public const string KEY_TAG_MSW_MEDICINE_MANAGEMENT = "msw_medicine_management";
        public const string KEY_TAG_MSW_OTHER_PAYMENTS_MANAGEMENT = "msw_other_payments_management";
        public const string KEY_TAG_MSW_WAREHOUSE_MANAGEMENT = "msw_warehouse_management";
        public const string KEY_TAG_MSW_REPORT = "msw_report";

        //Personal info page key string
        public const string KEY_TAG_MSW_PIP_SAVE_BUTTON = "msw_pip_save_button";
        public const string KEY_TAG_MSW_PIP_CANCLE_BUTTON = "msw_pip_cancle_button";
        public const string KEY_TAG_MSW_PIP_CAMERA_BUTTON = "msw_pip_camera_button";

        //User management page key string
        public const string KEY_TAG_MSW_UMP_EDIT_BUTTON = "msw_ump_edit_button";
        public const string KEY_TAG_MSW_UMP_DELETE_BUTTON = "msw_ump_delete_button";
        public const string KEY_TAG_MSW_UMP_ADD_BUTTON = "msw_ump_add_button";

        //User modification page key string
        public const string KEY_TAG_MSW_UMP_UMoP_SAVE_BUTTON = "msw_ump_umop_save_button";
        public const string KEY_TAG_MSW_UMP_UMoP_CANCLE_BUTTON = "msw_ump_umop_cancle_button";
        public const string KEY_TAG_MSW_UMP_UMoP_CAMERA_BUTTON = "msw_ump_umop_camera_button";

        //User instantiation page key string
        public const string KEY_TAG_MSW_UMP_UIP_SAVE_BUTTON = "msw_ump_uip_save_button";
        public const string KEY_TAG_MSW_UMP_UIP_CAMERA_BUTTON = "msw_ump_uip_camera_button";
        public const string KEY_TAG_MSW_UMP_UIP_CANCLE_BUTTON = "msw_ump_uip_cancle_button";

        //Customer management page key string
        public const string KEY_TAG_MSW_CMP_EDIT_BUTTON = "msw_cmp_edit_button";
        public const string KEY_TAG_MSW_CMP_DELETE_BUTTON = "msw_cmp_delete_button";
        public const string KEY_TAG_MSW_CMP_HISTORY_BUTTON = "msw_cmp_history_button";
        public const string KEY_TAG_MSW_CMP_ADD_BUTTON = "msw_cmp_add_button";

        //Customer instantiation page key string
        public const string KEY_TAG_MSW_CMP_CIP_SAVE_BUTTON = "msw_cmp_cip_save_button";
        public const string KEY_TAG_MSW_CMP_CIP_CAMERA_BUTTON = "msw_cmp_cip_camera_button";
        public const string KEY_TAG_MSW_CMP_CIP_CANCLE_BUTTON = "msw_cmp_cip_cancle_button";

        //Customer modification page key string
        public const string KEY_TAG_MSW_CMP_CMoP_SAVE_BUTTON = "msw_cmp_cmop_save_button";
        public const string KEY_TAG_MSW_CMP_CMoP_CAMERA_BUTTON = "msw_cmp_cmop_camera_button";
        public const string KEY_TAG_MSW_CMP_CMoP_CANCLE_BUTTON = "msw_cmp_cmop_cancle_button";

        //Medicine management page key string
        public const string KEY_TAG_MSW_MMP_SHOW_INFO_BUTTON = "msw_mmp_show_info_button";
        public const string KEY_TAG_MSW_MMP_EDIT_BUTTON = "msw_mmp_edit_button";
        public const string KEY_TAG_MSW_MMP_DELETE_BUTTON = "msw_mmp_delete_button";
        public const string KEY_TAG_MSW_MMP_ADD_BUTTON = "msw_mmp_add_button";
        public const string KEY_TAG_MSW_MMP_PROMO_BUTTON = "msw_mmp_promo_button";
        public const string KEY_TAG_MSW_MMP_PRINT_MEDICINE_BUTTON = "msw_mmp_print_medicine_button";
        public const string KEY_TAG_MSW_MMP_EXCEL_IMPORT_BUTTON = "msw_mmp_excel_import_button";

        //Add Medicine page key string
        public const string KEY_TAG_MSW_MMP_AMP_CANCEL_BUTTON = "msw_mmp_amp_cancel_button";
        public const string KEY_TAG_MSW_MMP_AMP_SAVE_BUTTON = "msw_mmp_amp_save_button";
        public const string KEY_TAG_MSW_MMP_AMP_CAMERA_BUTTON = "msw_mmp_amp_camera_button";

        //Modify Medicine page key string
        public const string KEY_TAG_MSW_MMP_MMP_CANCEL_BUTTON = "msw_mmp_mmp_cancel_button";
        public const string KEY_TAG_MSW_MMP_MMP_SAVE_BUTTON = "msw_mmp_mmp_save_button";
        public const string KEY_TAG_MSW_MMP_MMP_CAMERA_BUTTON = "msw_mmp_mmp_camera_button";

        //Show Medicine Info page key string
        public const string KEY_TAG_MSW_MMP_SMIP_CANCEL_BUTTON = "msw_mmp_smip_cancel_button";

        //Discount by Medicine page key string
        public const string KEY_TAG_MSW_MMP_DBMP_CANCEL_BUTTON = "msw_mmp_dbmp_cancel_button";
        public const string KEY_TAG_MSW_MMP_DBMP_SAVE_BUTTON = "msw_mmp_dbmp_save_button";
        public const string KEY_TAG_MSW_MMP_DBMP_DELETE_BUTTON = "msw_mmp_dbmp_delete_button";

        //Warehouse management page key string
        public const string KEY_TAG_MSW_WHMP_DELETE_BUTTON = "msw_whmp_delete_button";
        public const string KEY_TAG_MSW_WHMP_ADD_BUTTON = "msw_whmp_add_button";
        public const string KEY_TAG_MSW_WHMP_EDIT_BUTTON = "msw_whmp_edit_button";
        public const string KEY_TAG_MSW_WHMP_SHOW_INFO_BUTTON = "msw_whmp_show_info_button";
        public const string KEY_TAG_MSW_WHMP_SHOW_INVOICE_BUTTON = "msw_whmp_show_invoice_button";

        //Selling page key string#
        public const string KEY_TAG_MSW_SP_ADD_BUTTON = "msw_sp_add_button";
        public const string KEY_TAG_MSW_SP_REMOVE_BUTTON = "msw_sp_remove_button";
        public const string KEY_TAG_MSW_SP_INSTANTIATE_BUTTON = "msw_sp_instantiate_button";
        public const string KEY_TAG_MSW_SP_REFRESH_BUTTON = "msw_sp_refresh_button";
       
        //Add WarehouseImport page key string
        public const string KEY_TAG_MSW_WHMP_AWIP_CANCEL_BUTTON = "msw_whmp_awip_cancel_button";
        public const string KEY_TAG_MSW_WHMP_AWIP_SAVE_BUTTON = "msw_whmp_awip_save_button";
        public const string KEY_TAG_MSW_WHMP_AWIP_BROWSE_INVOICE_IMAGE_BUTTON = "msw_whmp_awip_browse_image_button_button";
        public const string KEY_TAG_MSW_WHMP_AWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON = "msw_whmp_awip_add_medicine_to_import_list_button";
        public const string KEY_TAG_MSW_WHMP_AWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON = "msw_whmp_awip_delete_medicine_to_import_list_button";

        //Modify WarehouseImport page key string
        public const string KEY_TAG_MSW_WHMP_MWIP_CANCEL_BUTTON = "msw_whmp_mwip_cancel_button";
        public const string KEY_TAG_MSW_WHMP_MWIP_SAVE_BUTTON = "msw_whmp_mwip_save_button";
        public const string KEY_TAG_MSW_WHMP_MWIP_BROWSE_INVOICE_IMAGE_BUTTON = "msw_whmp_mwip_browse_image_button_button";
        public const string KEY_TAG_MSW_WHMP_MWIP_ADD_MEDICINE_TO_IMPORT_LIST_BUTTON = "msw_whmp_mwip_add_medicine_to_import_list_button";
        public const string KEY_TAG_MSW_WHMP_MWIP_DELETE_MEDICINE_TO_IMPORT_LIST_BUTTON = "msw_whmp_mwip_delete_medicine_to_import_list_button";

        //Show WarehouseImport Info page key string
        public const string KEY_TAG_MSW_WHMP_SWIIP_CANCEL_BUTTON = "msw_whmp_swiip_cancel_button";
        public const string KEY_TAG_MSW_WHMP_SWIIP_SHOW_INVOICE_BUTTON = "msw_whmp_swiip_show_invoice_button";

        //Supplier management page key string
        public const string KEY_TAG_MSW_SMP_DELETE_BUTTON = "msw_smp_delete_button";
        public const string KEY_TAG_MSW_SMP_ADD_BUTTON = "msw_smp_add_button";
        public const string KEY_TAG_MSW_SMP_EDIT_BUTTON = "msw_smp_edit_button";
        public const string KEY_TAG_MSW_SMP_SHOW_INFO_BUTTON = "msw_smp_show_info_button";
        public const string KEY_TAG_MSW_SMP_SHOW_INVOICE_BUTTON = "msw_smp_show_invoice_button";

        //Add Supplier page key string
        public const string KEY_TAG_MSW_SMP_ASP_CANCEL_BUTTON = "msw_smp_asp_cancel_button";
        public const string KEY_TAG_MSW_SMP_ASP_SAVE_BUTTON = "msw_smp_asp_save_button";
    }
}
