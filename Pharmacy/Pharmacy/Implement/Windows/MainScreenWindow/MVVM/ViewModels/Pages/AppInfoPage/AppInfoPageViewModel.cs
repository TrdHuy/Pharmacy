using Pharmacy.Base.Utils.Attributes;
using Pharmacy.Implement.AppImpl.Models.VOs;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.AppInfoPage.OVs;
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

        public AppInfoVO AppInfo { get; set; }
        public MSW_AIP_ButtonCommandOV ButtonCommandOV { get; set; }

        protected override Logger logger => L;

        protected override void OnInitialized()
        {
        }

        protected override void OnInitializing()
        {
            AppInfo = new AppInfoVO();
            ButtonCommandOV = new MSW_AIP_ButtonCommandOV(this);
        }

    }
}
