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

        public Uri CurrentPageSource;

        private MSW_PageController()
        {
            CurrentPageSource = HomePage.Value;
        }

        public void UpdateCurrentPageSource(int pageNum)
        {
            switch (pageNum)
            {
                case 0:
                    CurrentPageSource = HomePage.Value;
                    break;
                case 1:
                    CurrentPageSource = PersonalInfoPage.Value;
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

}
