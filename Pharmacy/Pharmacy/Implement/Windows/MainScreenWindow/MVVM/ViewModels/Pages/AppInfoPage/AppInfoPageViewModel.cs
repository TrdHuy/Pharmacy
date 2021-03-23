using Pharmacy.Base.Utils.Attributes;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.AppInfoPage
{
    internal class AppInfoPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("AppInfoPageViewModel");

        public string ProductName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }
        public string ProductID
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductIDAttribute>()?.ProductID;
            }
        }
        public string ProductVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        public string ReleaseDate
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyReleaseDateAttribute>()?.ReleaseDate;
            }
        }
        public string Description
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyReleaseDetailAttribute>()?.ReleaseDetail;
            }
        }
        public string Company
        {
            get
            {
                return Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)
                    .OfType<AssemblyCompanyAttribute>()
                    .FirstOrDefault()?
                    .Company;

                //return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).CompanyName;
            }
        }


        protected override Logger logger => L;

        protected override void OnInitialized()
        {
        }

        protected override void OnInitializing()
        {

        }

    }
}
