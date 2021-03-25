using Newtonsoft.Json;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Config;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.Extensions.Entities;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.HomePage.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using static HPSolutionCCDevPackage.netFramework.AtumImageView;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages
{
    internal class HomePageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("HomePageViewModel");

        private KeyActionListener _keyActionListener = KeyActionListener.Current;

        private string _currentTime;

        #region public properties

        public tblUser CurrentUser { get { return App.Current.CurrentUser; } }

        public EventCommandModel AvatarCommand { get; set; }
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
            AvatarCommand = new EventCommandModel(AvatarClickEvent);
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

        private void AvatarClickEvent(object sender, EventArgs e, object paramater)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramater;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO
                , dataTransfer);
        }

    }
}
