using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.BaseWindow.Utils.PageController
{
    public enum PageSource
    {
        // Page index for MS_Window
        NONE = -1,
        HOME_PAGE = 0,
        PERSONAL_INFO_PAGE = 1,
        SELLING_PAGE = 2,
        USER_MANAGEMENT_PAGE = 3,
        CUSTOMER_MANAGEMENT_PAGE = 4,
        SUPPLIER_MANAGEMENT_PAGE = 5,
        INVOICE_MANAGEMENT_PAGE = 6,
        MEDICINE_MANAGEMENT_PAGE = 7,
        OTHER_PAYMENT_MANAGEMENT_PAGE = 8,
        WAREHOUSE_MANAGEMENT_PAGE = 9,
        REPORT_PAGE = 10,
        ADD_MEDICINE_PAGE = 11,
        MODIFY_MEDICINE_PAGE = 12,
        SHOW_MEDICINE_INFO_PAGE = 13,
        DISCOUNT_BY_MEDICINE_PAGE = 14,
        ADD_WAREHOUSE_IMPORT_PAGE = 15,
        MODIFY_WAREHOUSE_IMPORT_PAGE = 16,
        SHOW_WAREHOUSE_IMPORT_INFO_PAGE = 17,
        ADD_SUPPLIER_PAGE = 18,
        MODIFY_SUPPLIER_PAGE = 19,
        SUPPLIER_IMPORT_HISTORY_PAGE = 20,
        SUPPLIER_DEBT_PAGE = 21,
        ADD_OTHER_PAYMENT_PAGE = 22,
        MODIFY_OTHER_PAYMENT_PAGE = 23,
        SETTING_PAGE = 24,
        USER_MODIFICATION_PAGE = 31,
        USER_INSTANTIATION_PAGE = 32,
        CUSTOMER_MODIFICATION_PAGE = 41,
        CUSTOMER_INSTANTIATION_PAGE = 42,
        CUSTOMER_TRANSACTION_HISTORY_PAGE = 43,
        CUSTOMER_DEBT_PAGE = 431,
        CUSTOMER_BILL_PAGE = 432,
        APP_INFO_PAGE = 44,

        // Page index for PopupScreenWindow
        DAILY_REPORT_DETAIL_PAGE = 1000
    }
}
