using Pharmacy.Config;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions.Entities;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.HomePage.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using System;
using System.Windows;
using System.Windows.Threading;
using static HPSolutionCCDevPackage.netFramework.AtumImageView;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages
{
    internal class HomePageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("HomePageViewModel");

        private string _currentTime;

        #region public properties

        public tblUser CurrentUser => App.Current.CurrentUser;

        public MSW_HP_ButtonCommandOV ButtonCommandOV { get; set; }


        public bool IsSupportChangePersonalAvatarLocation { get { return RUNE.IS_SUPPORT_CHANGE_PERSONAL_AVATAR_LOCATION; } }
        public bool IsSupportChangePersonalAvatarZoom { get { return RUNE.IS_SUPPORT_CHANGE_PERSONAL_AVATAR_ZOOM; } }

        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                InvalidateOwn();
            }
        }

        public AtumUserData AvatarInfoData
        {
            get
            {
                return CurrentUser.GetUserData().PersonalAvatarInfo;
            }
            set
            {
                CurrentUser.GetUserData().PersonalAvatarInfo = value;
                InvalidateOwn();
            }
        }

        public Visibility AdminToolboxsVisibility
        {
            get
            {
                return CurrentUser.IsAdmin ?
                    Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ClockIntansiation();
            ButtonCommandOV = new MSW_HP_ButtonCommandOV(this);
        }

        protected override void OnInitialized()
        {
        }

        protected override void InitPropertiesRegistry()
        {
            PropRegister("CurrentUser");
        }

        private void ClockIntansiation()
        {
            // Critical issue: DispatcherTimer initalized to many time when open HomePage,
            // must change it to instance only
            DispatcherTimer clock = new DispatcherTimer();
            clock.Interval = TimeSpan.FromMilliseconds(500);
            clock.Tick += new EventHandler(UpdateClock);
            clock.Start();
        }

        private void UpdateClock(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
