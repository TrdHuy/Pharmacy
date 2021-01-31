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

        public Lazy<PageVO> ShowMedicineInfoPage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.SHOW_MEDICINE_INFO_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.SHOW_MEDICINE_INFO_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageVO> DiscountByMedicinePage = new Lazy<PageVO>(() =>
         new PageVO(
             new Uri(PharmacyDefinitions.DISCOUNT_BY_MEDICINE_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
             PharmacyDefinitions.DISCOUNT_BY_MEDICINE_PAGE_LOADING_DELAY_TIME));

        public PageVO CurrentPageOV;
        public Uri CurrentPageSource;

        private MSW_PageController()
        {
            CurrentPageOV = new PageVO(HomePage.Value.PageUri,
                HomePage.Value.LoadingDelayTime);
        }

        public void UpdateCurrentPageSource(PageSource pageNum)
        {
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

            switch (x)
            {
                case PharmacyDefinitions.HOME_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = HomePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = HomePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.PERSONAL_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = PersonalInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = PersonalInfoPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = CustomerManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SUPPLIER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = SupplierManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SupplierManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.USER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = UserManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.INVOICE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = InvoiceManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = InvoiceManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.REPORT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = ReportPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ReportPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SELLING_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = SellingPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = SellingPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MEDICINE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = MedicineManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = MedicineManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.OTHER_PAYMENT_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = OtherPaymentsManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = OtherPaymentsManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.WAREHOUSE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = WarehouseManagementPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = WarehouseManagementPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.USER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = UserInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.USER_MODIFICATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = UserModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserModificationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.ADD_MEDICINE_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = AddMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = AddMedicinePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.MODIFY_MEDICINE_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = ModifyMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ModifyMedicinePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.SHOW_MEDICINE_INFO_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = ShowMedicineInfoPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = ShowMedicineInfoPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.DISCOUNT_BY_MEDICINE_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = DiscountByMedicinePage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = DiscountByMedicinePage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = CustomerInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_MODIFICATION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = CustomerModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerModificationPage.Value.LoadingDelayTime;
                    break;
                case PharmacyDefinitions.CUSTOMER_TRANSACTION_PAGE_URI_ORIGINAL_STRING:
                    CurrentPageOV.PageUri = CustomerTransactionHistoryPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = CustomerTransactionHistoryPage.Value.LoadingDelayTime;
                    break;
                default:
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

        UserModificationPage = 31,
        UserInstantiationPage = 32,

        CustomerModificationPage = 41,
        CustomerInstantiationPage = 42,
        CustomerTransactionHistoryPage = 43
    }


}
