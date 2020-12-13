using Pharmacy.Base.Observable.ObserverPattern;
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
    public class MSW_PageController : BaseObservable<Uri>
    {
        private static MSW_PageController _instance;

        public Lazy<Uri> HomePage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/HomePage.xaml", UriKind.Relative));

        public Lazy<Uri> PersonalInfoPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/PersonalInfoPage.xaml", UriKind.Relative));

        public Lazy<Uri> SellingPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/SellingPage.xaml", UriKind.Relative));
        
        public Lazy<Uri> UserManagementPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/UserManagementPage.xaml", UriKind.Relative));
        
        public Lazy<Uri> CustomerManagementPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/CustomerManagementPage.xaml", UriKind.Relative));
        
        public Lazy<Uri> SupplierManagementPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/SupplierManagementPage.xaml", UriKind.Relative));
        
        public Lazy<Uri> InvoiceManagementPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/InvoiceManagementPage.xaml", UriKind.Relative));

        public Lazy<Uri> MedicineManagementPage = new Lazy<Uri>(() =>
         new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/MedicineManagementPage.xaml", UriKind.Relative));

        public Lazy<Uri> OtherPaymentsManagementPage = new Lazy<Uri>(() =>
         new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/OtherPaymentsManagementPage.xaml", UriKind.Relative));

        public Lazy<Uri> WarehouseManagementPage = new Lazy<Uri>(() =>
         new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/WarehouseManagementPage.xaml", UriKind.Relative));

        public Lazy<Uri> ReportPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/ReportPage.xaml", UriKind.Relative));

        public Uri CurrentPageSource;

        private MSW_PageController()
        {
            CurrentPageSource = HomePage.Value;
        }

        public void UpdateCurrentPageSource(PageSource pageNum)
        {
            switch (pageNum)
            {
                case PageSource.HomePage:
                    CurrentPageSource = HomePage.Value;
                    break;
                case PageSource.PersonalInfoPage:
                    CurrentPageSource = PersonalInfoPage.Value;
                    break;
                case PageSource.CustomerManagementPage:
                    CurrentPageSource = CustomerManagementPage.Value;
                    break;
                case PageSource.SupplierManagementPage:
                    CurrentPageSource = SupplierManagementPage.Value;
                    break;
                case PageSource.UserManagementPage:
                    CurrentPageSource = UserManagementPage.Value;
                    break;
                case PageSource.InvoiceManagementPage:
                    CurrentPageSource = InvoiceManagementPage.Value;
                    break;
                case PageSource.ReportPage:
                    CurrentPageSource = ReportPage.Value;
                    break;
                case PageSource.SellingPage:
                    CurrentPageSource = SellingPage.Value;
                    break;
                case PageSource.MedicineManagementPage:
                    CurrentPageSource = MedicineManagementPage.Value;
                    break;
                case PageSource.OtherPaymentsManagementPage:
                    CurrentPageSource = OtherPaymentsManagementPage.Value;
                    break;
                case PageSource.WarehouseManagementPage:
                    CurrentPageSource = WarehouseManagementPage.Value;
                    break;
                default:
                    CurrentPageSource = HomePage.Value;
                    break;
            }
            NotifyChange(CurrentPageSource);
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
        ReportPage = 10
    }

}
