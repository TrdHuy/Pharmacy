using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages
{
    public class HomePageViewModel : AbstractViewModel
    {
        private ApplicationDataManager _applicationDataManager = ApplicationDataManager.Instance;
        private IActionListener _keyActionListener = KeyActionListener.Instance;

        private string _currentTime;

        #region public properties

        public tblUser CurrentUser { get; set; }
        public RunInputCommand PersonalInfoCommand { get; set; }

        #endregion

        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                InvalidateOwn();
            }
        }

        public HomePageViewModel()
        {
            CurrentUser = _applicationDataManager.CurrentUser;
            ClockIntansiation();
            PersonalInfoCommand = new RunInputCommand(PersonalInfoButtonClickEvent);
          
        }
        protected override void InitPropertiesRegistry()
        {
        }

        private void ClockIntansiation()
        {
            DispatcherTimer clock = new DispatcherTimer();
            clock.Interval = TimeSpan.FromMilliseconds(500);
            clock.Tick += new EventHandler(UpdateClock);
            clock.Start();
        }

        private void UpdateClock(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("hh:mm:ss");
        }

        private void PersonalInfoButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_PERSONAL_INFO
                , dataTransfer);
        }

    }
}
