using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.VOs;
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
    public class MSW_PageController : BaseObservable<PageVO>
    {
        private static MSW_PageController _instance;

        public Lazy<PageVO> HomePage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.HOME_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.HOME_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> PersonalInfoPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.PERSONAL_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.PERSONAL_INFO_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> SellingPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.SELLING_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.SELLING_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> UserManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.USER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.USER_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> CustomerManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.CUSTOMER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.CUSTOMER_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> SupplierManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.SUPPLIER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.SUPPLIER_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> InvoiceManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.INVOICE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.INVOICE_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> MedicineManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.MEDICINE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.MEDICINE_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> OtherPaymentsManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.OTHER_PAYMENT_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.OTHER_PAYMENT_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> WarehouseManagementPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.WAREHOUSE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.WAREHOUSE_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> ReportPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.REPORT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.REPORT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> UserModificationPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.USER_MODIFICATION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.USER_MODIFICATION_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> UserInstantiationPage = new Lazy<PageVO>(() =>
           new PageVO(
                new Uri(PharmacyDefinitions.USER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.USER_INSTANTIATION_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> AddMedicinePage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.ADD_MEDICINE_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.ADD_MEDICINE_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> ModifyMedicinePage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.MODIFY_MEDICINE_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.MODIFY_MEDICINE_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> CustomerInstantiationPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.CUSTOMER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.CUSTOMER_INSTANTIATION_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> CustomerModificationPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.CUSTOMER_MODIFICATION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.CUSTOMER_MODIFICATION_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> CustomerTransactionHistoryPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.CUSTOMER_TRANSACTION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.CUSTOMER_TRANSACTION_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> CustomerDebtsPage = new Lazy<PageVO>(() =>
           new PageVO(
               new Uri(PharmacyDefinitions.CUSTOMER_DEBTS_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
               PharmacyDefinitions.CUSTOMER_DEBTS_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> CustomerBillPage = new Lazy<PageVO>(() =>
           new PageVO(
               new Uri(PharmacyDefinitions.CUSTOMER_BILL_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
               PharmacyDefinitions.CUSTOMER_BILL_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> ShowMedicineInfoPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.SHOW_MEDICINE_INFO_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.SHOW_MEDICINE_INFO_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> DiscountByMedicinePage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.DISCOUNT_BY_MEDICINE_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.DISCOUNT_BY_MEDICINE_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> AddWarehouseImportPage = new Lazy<PageVO>(() =>
            new PageVO(
                 new Uri(PharmacyDefinitions.ADD_WAREHOUSE_IMPORT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                 PharmacyDefinitions.ADD_WAREHOUSE_IMPORT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> ModifyWarehouseImportPage = new Lazy<PageVO>(() =>
            new PageVO(
                 new Uri(PharmacyDefinitions.MODIFY_WAREHOUSE_IMPORT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                 PharmacyDefinitions.MODIFY_WAREHOUSE_IMPORT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> ShowWarehouseImportInfoPage = new Lazy<PageVO>(() =>
            new PageVO(
                new Uri(PharmacyDefinitions.SHOW_WAREHOUSE_IMPORT_INFO_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.SHOW_WAREHOUSE_IMPORT_INFO_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> AddSupplierPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.ADD_SUPPLIER_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.ADD_SUPPLIER_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> ModifySupplierPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.MODIFY_SUPPLIER_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.MODIFY_SUPPLIER_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> SupplierImportHistoryPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.SUPPLIER_IMPORT_HISTORY_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.SUPPLIER_IMPORT_HISTORY_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> SupplierDebtPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.SUPPLIER_DEBT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.SUPPLIER_DEBT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> AddOtherPaymentPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.ADD_OTHER_PAYMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.ADD_OTHER_PAYMENT_PAGE_LOADING_DELAY_TIME));
        
        public Lazy<PageVO> ModifyOtherPaymentPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.MODIFY_OTHER_PAYMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.MODIFY_OTHER_PAYMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> SettingPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.SETTING_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.SETTING_PAGE_LOADING_DELAY_TIME));

        public PageVO CurrentPageOV { get; set; }
        public PageSource CurrentPageSource { get; private set; } = PageSource.None;
        public PageSource PreviousePageSource { get; private set; } = PageSource.None;


        private MSW_PageController()
        {
            CurrentPageOV = new PageVO(HomePage.Value.PageUri,
                HomePage.Value.LoadingDelayTime);
        }

        public void UpdateCurrentPageSource(PageSource pageNum)
        {
            PreviousePageSource = CurrentPageSource;
            CurrentPageSource = pageNum;

            switch (pageNum)
            {
                case PageSource.HomePage:
                    CurrentPageOV.PageUri = HomePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = HomePage.Value.LoadingDelayTime;
                    break;
                case PageSource.PersonalInfoPage:
                    CurrentPageOV.PageUri = PersonalInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = PersonalInfoPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CustomerManagementPage:
                    CurrentPageOV.PageUri = CustomerManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SupplierManagementPage:
                    CurrentPageOV.PageUri = SupplierManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.UserManagementPage:
                    CurrentPageOV.PageUri = UserManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.InvoiceManagementPage:
                    CurrentPageOV.PageUri = InvoiceManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = InvoiceManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.ReportPage:
                    CurrentPageOV.PageUri = ReportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ReportPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SellingPage:
                    CurrentPageOV.PageUri = SellingPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SellingPage.Value.LoadingDelayTime;
                    break;
                case PageSource.MedicineManagementPage:
                    CurrentPageOV.PageUri = MedicineManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = MedicineManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.OtherPaymentsManagementPage:
                    CurrentPageOV.PageUri = OtherPaymentsManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = OtherPaymentsManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.WarehouseManagementPage:
                    CurrentPageOV.PageUri = WarehouseManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = WarehouseManagementPage.Value.LoadingDelayTime;
                    break;
                case PageSource.AddMedicinePage:
                    CurrentPageOV.PageUri = AddMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddMedicinePage.Value.LoadingDelayTime;
                    break;
                case PageSource.ModifyMedicinePage:
                    CurrentPageOV.PageUri = ModifyMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyMedicinePage.Value.LoadingDelayTime;
                    break;
                case PageSource.ShowMedicineInfoPage:
                    CurrentPageOV.PageUri = ShowMedicineInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ShowMedicineInfoPage.Value.LoadingDelayTime;
                    break;
                case PageSource.DiscountByMedicinePage:
                    CurrentPageOV.PageUri = DiscountByMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = DiscountByMedicinePage.Value.LoadingDelayTime;
                    break;
                case PageSource.UserInstantiationPage:
                    CurrentPageOV.PageUri = UserInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PageSource.UserModificationPage:
                    CurrentPageOV.PageUri = UserModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserModificationPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CustomerInstantiationPage:
                    CurrentPageOV.PageUri = CustomerInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CustomerModificationPage:
                    CurrentPageOV.PageUri = CustomerModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerModificationPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CustomerTransactionHistoryPage:
                    CurrentPageOV.PageUri = CustomerTransactionHistoryPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerTransactionHistoryPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CustomerDebtsPage:
                    CurrentPageOV.PageUri = CustomerDebtsPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerDebtsPage.Value.LoadingDelayTime;
                    break;
                case PageSource.CustomerBillPage:
                    CurrentPageOV.PageUri = CustomerBillPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerBillPage.Value.LoadingDelayTime;
                    break;
                case PageSource.AddWarehouseImportPage:
                    CurrentPageOV.PageUri = AddWarehouseImportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddWarehouseImportPage.Value.LoadingDelayTime;
                    break;
                case PageSource.ModifyWarehouseImportPage:
                    CurrentPageOV.PageUri = ModifyWarehouseImportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyWarehouseImportPage.Value.LoadingDelayTime;
                    break;
                case PageSource.ShowWarehouseImportInfoPage:
                    CurrentPageOV.PageUri = ShowWarehouseImportInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ShowWarehouseImportInfoPage.Value.LoadingDelayTime;
                    break;
                case PageSource.AddSupplierPage:
                    CurrentPageOV.PageUri = AddSupplierPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddSupplierPage.Value.LoadingDelayTime;
                    break;
                case PageSource.ModifySupplierPage:
                    CurrentPageOV.PageUri = ModifySupplierPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifySupplierPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SupplierImportHistoryPage:
                    CurrentPageOV.PageUri = SupplierImportHistoryPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierImportHistoryPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SupplierDebtPage:
                    CurrentPageOV.PageUri = SupplierDebtPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierDebtPage.Value.LoadingDelayTime;
                    break;
                case PageSource.AddOtherPaymentPage:
                    CurrentPageOV.PageUri = AddOtherPaymentPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddOtherPaymentPage.Value.LoadingDelayTime;
                    break;
                case PageSource.ModifyOtherPaymentPage:
                    CurrentPageOV.PageUri = ModifyOtherPaymentPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyOtherPaymentPage.Value.LoadingDelayTime;
                    break;
                case PageSource.SettingPage:
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
        public void UpdatePageOVUri(Uri uri)
        {
            var x = "/" + uri.OriginalString;
            PreviousePageSource = CurrentPageSource;
            switch (x)
            {
                case PharmacyDefinitions.HOME_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.HomePage;
                    CurrentPageOV.PageUri = HomePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = HomePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.PERSONAL_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.PersonalInfoPage;
                    CurrentPageOV.PageUri = PersonalInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = PersonalInfoPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CustomerManagementPage;
                    CurrentPageOV.PageUri = CustomerManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SUPPLIER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SupplierManagementPage;
                    CurrentPageOV.PageUri = SupplierManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.USER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.UserManagementPage;
                    CurrentPageOV.PageUri = UserManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.INVOICE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.InvoiceManagementPage;
                    CurrentPageOV.PageUri = InvoiceManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = InvoiceManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.REPORT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ReportPage;
                    CurrentPageOV.PageUri = ReportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ReportPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SELLING_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SellingPage;
                    CurrentPageOV.PageUri = SellingPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SellingPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MEDICINE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.MedicineManagementPage;
                    CurrentPageOV.PageUri = MedicineManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = MedicineManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.OTHER_PAYMENT_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.OtherPaymentsManagementPage;
                    CurrentPageOV.PageUri = OtherPaymentsManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = OtherPaymentsManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.WAREHOUSE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.WarehouseManagementPage;
                    CurrentPageOV.PageUri = WarehouseManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = WarehouseManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.USER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.UserInstantiationPage;
                    CurrentPageOV.PageUri = UserInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.USER_MODIFICATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.UserModificationPage;
                    CurrentPageOV.PageUri = UserModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserModificationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.ADD_MEDICINE_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.AddMedicinePage;
                    CurrentPageOV.PageUri = AddMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddMedicinePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MODIFY_MEDICINE_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ModifyMedicinePage;
                    CurrentPageOV.PageUri = ModifyMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyMedicinePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SHOW_MEDICINE_INFO_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ShowMedicineInfoPage;
                    CurrentPageOV.PageUri = ShowMedicineInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ShowMedicineInfoPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.DISCOUNT_BY_MEDICINE_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.DiscountByMedicinePage;
                    CurrentPageOV.PageUri = DiscountByMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = DiscountByMedicinePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CustomerInstantiationPage;
                    CurrentPageOV.PageUri = CustomerInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_MODIFICATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CustomerModificationPage;
                    CurrentPageOV.PageUri = CustomerModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerModificationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_TRANSACTION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CustomerTransactionHistoryPage;
                    CurrentPageOV.PageUri = CustomerTransactionHistoryPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerTransactionHistoryPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_DEBTS_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CustomerDebtsPage;
                    CurrentPageOV.PageUri = CustomerDebtsPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerDebtsPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_BILL_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.CustomerBillPage;
                    CurrentPageOV.PageUri = CustomerBillPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerBillPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.ADD_WAREHOUSE_IMPORT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.AddWarehouseImportPage;
                    CurrentPageOV.PageUri = AddWarehouseImportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddWarehouseImportPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MODIFY_WAREHOUSE_IMPORT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ModifyWarehouseImportPage;
                    CurrentPageOV.PageUri = ModifyWarehouseImportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyWarehouseImportPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SHOW_WAREHOUSE_IMPORT_INFO_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ShowWarehouseImportInfoPage;
                    CurrentPageOV.PageUri = ShowWarehouseImportInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ShowWarehouseImportInfoPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.ADD_SUPPLIER_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.AddSupplierPage;
                    CurrentPageOV.PageUri = AddSupplierPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddSupplierPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MODIFY_SUPPLIER_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ModifySupplierPage;
                    CurrentPageOV.PageUri = ModifySupplierPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifySupplierPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SUPPLIER_IMPORT_HISTORY_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SupplierImportHistoryPage;
                    CurrentPageOV.PageUri = SupplierImportHistoryPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierImportHistoryPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SUPPLIER_DEBT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SupplierDebtPage;
                    CurrentPageOV.PageUri = SupplierDebtPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierDebtPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.ADD_OTHER_PAYMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.AddOtherPaymentPage;
                    CurrentPageOV.PageUri = AddOtherPaymentPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddOtherPaymentPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MODIFY_OTHER_PAYMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.ModifyOtherPaymentPage;
                    CurrentPageOV.PageUri = ModifyOtherPaymentPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyOtherPaymentPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SETTING_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageSource = PageSource.SettingPage;
                    CurrentPageOV.PageUri = SettingPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SettingPage.Value.LoadingDelayTime;
                    break;
                default:
                    CurrentPageSource = PageSource.HomePage;
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


    public enum PageSource
    {
        None = -1,
        HomePage = 0,
        PersonalInfoPage = 1,
        SellingPage = 2,
        UserManagementPage = 3,
        CustomerManagementPage = 4,
        SupplierManagementPage = 5,
        InvoiceManagementPage = 6,
        MedicineManagementPage = 7,
        OtherPaymentsManagementPage = 8,
        WarehouseManagementPage = 9,
        ReportPage = 10,
        AddMedicinePage = 11,
        ModifyMedicinePage = 12,
        ShowMedicineInfoPage = 13,
        DiscountByMedicinePage = 14,
        AddWarehouseImportPage = 15,
        ModifyWarehouseImportPage = 16,
        ShowWarehouseImportInfoPage = 17,
        AddSupplierPage = 18,
        ModifySupplierPage = 19,
        SupplierImportHistoryPage = 20,
        SupplierDebtPage = 21,
        AddOtherPaymentPage = 22,
        ModifyOtherPaymentPage = 23,
        SettingPage = 24,

        UserModificationPage = 31,
        UserInstantiationPage = 32,

        CustomerModificationPage = 41,
        CustomerInstantiationPage = 42,
        CustomerTransactionHistoryPage = 43,
        CustomerDebtsPage = 431,
        CustomerBillPage = 432,

    }


}
