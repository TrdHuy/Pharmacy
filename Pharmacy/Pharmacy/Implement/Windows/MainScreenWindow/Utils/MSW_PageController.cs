using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Implement.Utils.Definitions;
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
    public class MSW_PageController : BaseObservable<PageOV>
    {
        private static MSW_PageController _instance;

        public Lazy<PageOV> HomePage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.HOME_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.HOME_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> PersonalInfoPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.PERSONAL_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.PERSONAL_INFO_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> SellingPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.SELLING_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.SELLING_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> UserManagementPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.USER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.USER_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> CustomerManagementPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.CUSTOMER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.CUSTOMER_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> SupplierManagementPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.SUPPLIER_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.SUPPLIER_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> InvoiceManagementPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.INVOICE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.INVOICE_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> MedicineManagementPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.MEDICINE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.MEDICINE_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> OtherPaymentsManagementPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.OTHER_PAYMENT_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.OTHER_PAYMENT_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> WarehouseManagementPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.WAREHOUSE_MANAGEMENT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.WAREHOUSE_MANAGEMENT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> ReportPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.REPORT_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.REPORT_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> UserModificationPage = new Lazy<PageOV>(() =>
            new PageOV(
                new Uri(PharmacyDefinitions.USER_MODIFICATION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.USER_MODIFICATION_PAGE_LOADING_DELAY_TIME));

        public Lazy<PageOV> UserInstantiationPage = new Lazy<PageOV>(() =>
           new PageOV(
                new Uri(PharmacyDefinitions.USER_INSTANTIATION_PAGE_URI_ORIGINAL_STRING, UriKind.Relative),
                PharmacyDefinitions.USER_INSTANTIATION_PAGE_LOADING_DELAY_TIME));

        public Uri CurrentPageSource;
        public PageOV CurrentPageOV;

        private MSW_PageController()
        {
            CurrentPageOV = new PageOV(HomePage.Value.PageUri,
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
                case PageSource.UserInstantiationPage:
                    CurrentPageOV.PageUri = UserInstantiationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserInstantiationPage.Value.LoadingDelayTime;
                    break;
                case PageSource.UserModificationPage:
                    CurrentPageOV.PageUri = UserModificationPage.Value.PageUri;
                    CurrentPageOV.LoadingDelayTime = UserModificationPage.Value.LoadingDelayTime;
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

        UserModificationPage = 31,
        UserInstantiationPage = 32
    }


}
