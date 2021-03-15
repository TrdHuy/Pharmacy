using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.MVVM.Models.VOs;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Utils
{
    public class MSW_PageController : BasePageController
    {
        private static MSW_PageController _instance;

        private Lazy<PageVO> HomePage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.HOME_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.HOME_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> PersonalInfoPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.PERSONAL_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.PERSONAL_INFO_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> SellingPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.SELLING_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.SELLING_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> UserManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.USER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.USER_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> CustomerManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.CUSTOMER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.CUSTOMER_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> SupplierManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.SUPPLIER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.SUPPLIER_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> InvoiceManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.INVOICE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.INVOICE_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> MedicineManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.MEDICINE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.MEDICINE_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> OtherPaymentsManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.OTHER_PAYMENT_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.OTHER_PAYMENT_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> WarehouseManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.WAREHOUSE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.WAREHOUSE_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> ReportPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.REPORT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.REPORT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> UserModificationPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.USER_MODIFICATION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.USER_MODIFICATION_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> UserInstantiationPage = new Lazy<PageVO>(() =>
           new PageVO(
                new Uri(PharmacyDefinitions.USER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.USER_INSTANTIATION_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> AddMedicinePage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.ADD_MEDICINE_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.ADD_MEDICINE_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> ModifyMedicinePage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.MODIFY_MEDICINE_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.MODIFY_MEDICINE_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> CustomerInstantiationPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.CUSTOMER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.CUSTOMER_INSTANTIATION_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> CustomerModificationPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.CUSTOMER_MODIFICATION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.CUSTOMER_MODIFICATION_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> CustomerTransactionHistoryPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.CUSTOMER_TRANSACTION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.CUSTOMER_TRANSACTION_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> CustomerDebtsPage = new Lazy<PageVO>(() =>
           new PageVO(
               new Uri(PharmacyDefinitions.CUSTOMER_DEBTS_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
               PharmacyDefinitions.CUSTOMER_DEBTS_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> CustomerBillPage = new Lazy<PageVO>(() =>
           new PageVO(
               new Uri(PharmacyDefinitions.CUSTOMER_BILL_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
               PharmacyDefinitions.CUSTOMER_BILL_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> ShowMedicineInfoPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.SHOW_MEDICINE_INFO_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.SHOW_MEDICINE_INFO_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> DiscountByMedicinePage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.DISCOUNT_BY_MEDICINE_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.DISCOUNT_BY_MEDICINE_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> AddWarehouseImportPage = new Lazy<PageVO>(() =>
            new PageVO(
                 new Uri(PharmacyDefinitions.ADD_WAREHOUSE_IMPORT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                 PharmacyDefinitions.ADD_WAREHOUSE_IMPORT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> ModifyWarehouseImportPage = new Lazy<PageVO>(() =>
            new PageVO(
                 new Uri(PharmacyDefinitions.MODIFY_WAREHOUSE_IMPORT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                 PharmacyDefinitions.MODIFY_WAREHOUSE_IMPORT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> ShowWarehouseImportInfoPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.SHOW_WAREHOUSE_IMPORT_INFO_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.SHOW_WAREHOUSE_IMPORT_INFO_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> AddSupplierPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.ADD_SUPPLIER_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.ADD_SUPPLIER_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> ModifySupplierPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.MODIFY_SUPPLIER_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.MODIFY_SUPPLIER_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> SupplierImportHistoryPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.SUPPLIER_IMPORT_HISTORY_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.SUPPLIER_IMPORT_HISTORY_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> SupplierDebtPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.SUPPLIER_DEBT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.SUPPLIER_DEBT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> AddOtherPaymentPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.ADD_OTHER_PAYMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.ADD_OTHER_PAYMENT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> ModifyOtherPaymentPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.MODIFY_OTHER_PAYMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.MODIFY_OTHER_PAYMENT_PAGE_LOADING_DELAY_TIME));

        private Lazy<PageVO> SettingPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.SETTING_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.SETTING_PAGE_LOADING_DELAY_TIME));

        


        private MSW_PageController()
        {
            CurrentPageOV = new PageVO(HomePage.Value.PageUri,
                HomePage.Value.LoadingDelayTime);
        }

        public override void UpdateCurrentPageSource(PageSource pageNum)
        {
            PreviousePageSource = CurrentPageSource;
            CurrentPageSource = pageNum;

            switch (pageNum)
            {
                case PageSource.HOME_PAGE:
                    CurrentPageOV.PageUri = HomePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = HomePage.Value.LoadingDelayTime;
                    break;
                case PageSource.PERSONAL_INFO_PAGE:
                    CurrentPageOV.PageUri = PersonalInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = PersonalInfoPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CUSTOMER_MANAGEMENT_PAGE:
                    CurrentPageOV.PageUri = CustomerManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SUPPLIER_MANAGEMENT_PAGE:
                    CurrentPageOV.PageUri = SupplierManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.USER_MANAGEMENT_PAGE:
                    CurrentPageOV.PageUri = UserManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.INVOICE_MANAGEMENT_PAGE:
                    CurrentPageOV.PageUri = InvoiceManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = InvoiceManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.REPORT_PAGE:
                    CurrentPageOV.PageUri = ReportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ReportPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SELLING_PAGE:
                    CurrentPageOV.PageUri = SellingPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SellingPage.Value.LoadingDelayTime;
                    break;
                case PageSource.MEDICINE_MANAGEMENT_PAGE:
                    CurrentPageOV.PageUri = MedicineManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = MedicineManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.OTHER_PAYMENT_MANAGEMENT_PAGE:
                    CurrentPageOV.PageUri = OtherPaymentsManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = OtherPaymentsManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.WAREHOUSE_MANAGEMENT_PAGE:
                    CurrentPageOV.PageUri = WarehouseManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = WarehouseManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.ADD_MEDICINE_PAGE:
                    CurrentPageOV.PageUri = AddMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddMedicinePage.Value.LoadingDelayTime;
                    break;
                case PageSource.MODIFY_MEDICINE_PAGE:
                    CurrentPageOV.PageUri = ModifyMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyMedicinePage.Value.LoadingDelayTime;
                    break;
                case PageSource.SHOW_MEDICINE_INFO_PAGE:
                    CurrentPageOV.PageUri = ShowMedicineInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ShowMedicineInfoPage.Value.LoadingDelayTime;
                    break;
                case PageSource.DISCOUNT_BY_MEDICINE_PAGE:
                    CurrentPageOV.PageUri = DiscountByMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = DiscountByMedicinePage.Value.LoadingDelayTime;
                    break;
                case PageSource.USER_INSTANTIATION_PAGE:
                    CurrentPageOV.PageUri = UserInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PageSource.USER_MODIFICATION_PAGE:
                    CurrentPageOV.PageUri = UserModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserModificationPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CUSTOMER_INSTANTIATION_PAGE:
                    CurrentPageOV.PageUri = CustomerInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CUSTOMER_MODIFICATION_PAGE:
                    CurrentPageOV.PageUri = CustomerModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerModificationPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CUSTOMER_TRANSACTION_HISTORY_PAGE:
                    CurrentPageOV.PageUri = CustomerTransactionHistoryPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerTransactionHistoryPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CUSTOMER_DEBT_PAGE:
                    CurrentPageOV.PageUri = CustomerDebtsPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerDebtsPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CUSTOMER_BILL_PAGE:
                    CurrentPageOV.PageUri = CustomerBillPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerBillPage.Value.LoadingDelayTime;
                    break;
                case PageSource.ADD_WAREHOUSE_IMPORT_PAGE:
                    CurrentPageOV.PageUri = AddWarehouseImportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddWarehouseImportPage.Value.LoadingDelayTime;
                    break;
                case PageSource.MODIFY_WAREHOUSE_IMPORT_PAGE:
                    CurrentPageOV.PageUri = ModifyWarehouseImportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyWarehouseImportPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SHOW_WAREHOUSE_IMPORT_INFO_PAGE:
                    CurrentPageOV.PageUri = ShowWarehouseImportInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ShowWarehouseImportInfoPage.Value.LoadingDelayTime;
                    break;
                case PageSource.ADD_SUPPLIER_PAGE:
                    CurrentPageOV.PageUri = AddSupplierPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddSupplierPage.Value.LoadingDelayTime;
                    break;
                case PageSource.MODIFY_SUPPLIER_PAGE:
                    CurrentPageOV.PageUri = ModifySupplierPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifySupplierPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SUPPLIER_IMPORT_HISTORY_PAGE:
                    CurrentPageOV.PageUri = SupplierImportHistoryPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierImportHistoryPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SUPPLIER_DEBT_PAGE:
                    CurrentPageOV.PageUri = SupplierDebtPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierDebtPage.Value.LoadingDelayTime;
                    break;
                case PageSource.ADD_OTHER_PAYMENT_PAGE:
                    CurrentPageOV.PageUri = AddOtherPaymentPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddOtherPaymentPage.Value.LoadingDelayTime;
                    break;
                case PageSource.MODIFY_OTHER_PAYMENT_PAGE:
                    CurrentPageOV.PageUri = ModifyOtherPaymentPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyOtherPaymentPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SETTING_PAGE:
                    CurrentPageOV.PageUri = SettingPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SettingPage.Value.LoadingDelayTime;
                    break;
                default:
                    CurrentPageOV.PageUri = HomePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = HomePage.Value.LoadingDelayTime;
                    break;
            }
            NotifyChange(CurrentPageOV);
        }

        // This method use for navigating behavior, when source was update from view,
        // it will call this method to update current uri, and loading delay time
        public override void UpdatePageOVUri(Uri uri)
        {
            var x = "/" + uri.OriginalString;
            PreviousePageSource = CurrentPageSource;
            switch (x)
            {
                case PharmacyDefinitions.HOME_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.HOME_PAGE;
                    CurrentPageOV.PageUri = HomePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = HomePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.PERSONAL_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.PERSONAL_INFO_PAGE;
                    CurrentPageOV.PageUri = PersonalInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = PersonalInfoPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CUSTOMER_MANAGEMENT_PAGE;
                    CurrentPageOV.PageUri = CustomerManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SUPPLIER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SUPPLIER_MANAGEMENT_PAGE;
                    CurrentPageOV.PageUri = SupplierManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.USER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.USER_MANAGEMENT_PAGE;
                    CurrentPageOV.PageUri = UserManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.INVOICE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.INVOICE_MANAGEMENT_PAGE;
                    CurrentPageOV.PageUri = InvoiceManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = InvoiceManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.REPORT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.REPORT_PAGE;
                    CurrentPageOV.PageUri = ReportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ReportPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SELLING_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SELLING_PAGE;
                    CurrentPageOV.PageUri = SellingPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SellingPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MEDICINE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.MEDICINE_MANAGEMENT_PAGE;
                    CurrentPageOV.PageUri = MedicineManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = MedicineManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.OTHER_PAYMENT_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.OTHER_PAYMENT_MANAGEMENT_PAGE;
                    CurrentPageOV.PageUri = OtherPaymentsManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = OtherPaymentsManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.WAREHOUSE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.WAREHOUSE_MANAGEMENT_PAGE;
                    CurrentPageOV.PageUri = WarehouseManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = WarehouseManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.USER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.USER_INSTANTIATION_PAGE;
                    CurrentPageOV.PageUri = UserInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.USER_MODIFICATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.USER_MODIFICATION_PAGE;
                    CurrentPageOV.PageUri = UserModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserModificationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.ADD_MEDICINE_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ADD_MEDICINE_PAGE;
                    CurrentPageOV.PageUri = AddMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddMedicinePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MODIFY_MEDICINE_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.MODIFY_MEDICINE_PAGE;
                    CurrentPageOV.PageUri = ModifyMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyMedicinePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SHOW_MEDICINE_INFO_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SHOW_MEDICINE_INFO_PAGE;
                    CurrentPageOV.PageUri = ShowMedicineInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ShowMedicineInfoPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.DISCOUNT_BY_MEDICINE_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.DISCOUNT_BY_MEDICINE_PAGE;
                    CurrentPageOV.PageUri = DiscountByMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = DiscountByMedicinePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CUSTOMER_INSTANTIATION_PAGE;
                    CurrentPageOV.PageUri = CustomerInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_MODIFICATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CUSTOMER_MODIFICATION_PAGE;
                    CurrentPageOV.PageUri = CustomerModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerModificationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_TRANSACTION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CUSTOMER_TRANSACTION_HISTORY_PAGE;
                    CurrentPageOV.PageUri = CustomerTransactionHistoryPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerTransactionHistoryPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_DEBTS_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CUSTOMER_DEBT_PAGE;
                    CurrentPageOV.PageUri = CustomerDebtsPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerDebtsPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_BILL_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CUSTOMER_BILL_PAGE;
                    CurrentPageOV.PageUri = CustomerBillPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerBillPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.ADD_WAREHOUSE_IMPORT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ADD_WAREHOUSE_IMPORT_PAGE;
                    CurrentPageOV.PageUri = AddWarehouseImportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddWarehouseImportPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MODIFY_WAREHOUSE_IMPORT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.MODIFY_WAREHOUSE_IMPORT_PAGE;
                    CurrentPageOV.PageUri = ModifyWarehouseImportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyWarehouseImportPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SHOW_WAREHOUSE_IMPORT_INFO_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SHOW_WAREHOUSE_IMPORT_INFO_PAGE;
                    CurrentPageOV.PageUri = ShowWarehouseImportInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ShowWarehouseImportInfoPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.ADD_SUPPLIER_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ADD_SUPPLIER_PAGE;
                    CurrentPageOV.PageUri = AddSupplierPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddSupplierPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MODIFY_SUPPLIER_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.MODIFY_SUPPLIER_PAGE;
                    CurrentPageOV.PageUri = ModifySupplierPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifySupplierPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SUPPLIER_IMPORT_HISTORY_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SUPPLIER_IMPORT_HISTORY_PAGE;
                    CurrentPageOV.PageUri = SupplierImportHistoryPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierImportHistoryPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SUPPLIER_DEBT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SUPPLIER_DEBT_PAGE;
                    CurrentPageOV.PageUri = SupplierDebtPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierDebtPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.ADD_OTHER_PAYMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ADD_OTHER_PAYMENT_PAGE;
                    CurrentPageOV.PageUri = AddOtherPaymentPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddOtherPaymentPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MODIFY_OTHER_PAYMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.MODIFY_OTHER_PAYMENT_PAGE;
                    CurrentPageOV.PageUri = ModifyOtherPaymentPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyOtherPaymentPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SETTING_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SETTING_PAGE;
                    CurrentPageOV.PageUri = SettingPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SettingPage.Value.LoadingDelayTime;
                    break;
                default:
                    CurrentPageSource = PageSource.HOME_PAGE;
                    CurrentPageOV.PageUri = HomePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = HomePage.Value.LoadingDelayTime;
                    break;
            }

            NotifyChange(CurrentPageOV);
        }

        public static MSW_PageController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MSW_PageController();
                }
                return _instance;
            }
        }
    }

}
