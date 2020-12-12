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

        public Lazy<Uri> BusinessPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/BusinessManagementPage.xaml", UriKind.Relative));
        
        public Lazy<Uri> StaffPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/StaffManagementPage.xaml", UriKind.Relative));
        
        public Lazy<Uri> CustomerPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/CustomerManagementPage.xaml", UriKind.Relative));
        
        public Lazy<Uri> VendorPage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/VendorManagementPage.xaml", UriKind.Relative));
        
        public Lazy<Uri> SalePage = new Lazy<Uri>(() =>
            new Uri("/Pharmacy;component/Implement/Windows/MainScreenWindow/MVVM/Views/Pages/SaleManagementPage.xaml", UriKind.Relative));
        
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
                    CurrentPageSource = CustomerPage.Value;
                    break;
                case PageSource.VendorManagementPage:
                    CurrentPageSource = VendorPage.Value;
                    break;
                case PageSource.StaffManagementPage:
                    CurrentPageSource = StaffPage.Value;
                    break;
                case PageSource.SaleManagementPage:
                    CurrentPageSource = SalePage.Value;
                    break;
                case PageSource.ReportPage:
                    CurrentPageSource = ReportPage.Value;
                    break;
                case PageSource.BusinessManagementPage:
                    CurrentPageSource = BusinessPage.Value;
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
        BusinessManagementPage = 2,
        StaffManagementPage = 3,
        CustomerManagementPage = 4,
        VendorManagementPage = 5,
        SaleManagementPage = 6,
        ReportPage = 7
    }

}
