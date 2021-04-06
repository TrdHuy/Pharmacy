using Pharmacy.Implement.AppImpl.Models.VOs;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.AppInfoPage.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using System.Windows;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.AppInfoPage
{
    internal class AppInfoPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("AppInfoPageViewModel");
        private bool _isAnActionRunning = false;

        public AppInfoVO AppInfo { get; set; }
        public MSW_AIP_ButtonCommandOV ButtonCommandOV { get; set; }

        public bool IsAnActionRunning
        {
            get
            {
                return _isAnActionRunning;
            }
            set
            {
                _isAnActionRunning = ButtonCommandOV.IsAppUpdateButtonRunning
                    || ButtonCommandOV.IsBugReportButtonRunning
                    || ButtonCommandOV.IsContatUsButtonRunning
                    || ButtonCommandOV.IsHpssHomePageButtonRunning;
                InvalidateOwn();
                Invalidate("AnimationLoadingVisibility");
            }
        }
        public Visibility AnimationLoadingVisibility
        {
            get
            {
                return IsAnActionRunning ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        protected override Logger logger => L;

        protected override void OnInitialized() { }

        protected override void OnInitializing()
        {
            AppInfo = new AppInfoVO();
            ButtonCommandOV = new MSW_AIP_ButtonCommandOV(this);
        }

        public override void OnUnloaded(object sender)
        {
            base.OnUnloaded(sender);
            ButtonCommandOV.OnDestroy();
        }

        public void UpdateFlagActionRunning()
        {
            IsAnActionRunning = true;
        }
    }
}
