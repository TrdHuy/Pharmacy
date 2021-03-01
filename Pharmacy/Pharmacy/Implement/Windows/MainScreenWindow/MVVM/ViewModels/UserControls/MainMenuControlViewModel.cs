using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.UserControls
{
    public class MainMenuControlViewModel : BaseViewModel
    {
        private IActionListener _keyActionListener = KeyActionListener.Instance;

        public RunInputCommand PersonalAccountCommand { get; set; }
        public RunInputCommand HomePageCommand { get; set; }
        public RunInputCommand SettingPageCommand { get; set; }

        public MainMenuControlViewModel()
        {
            PersonalAccountCommand = new RunInputCommand(PersonalAccountButtonClickEvent);
            HomePageCommand = new RunInputCommand(HomePageButtonClickEvent);
            SettingPageCommand = new RunInputCommand(SettingPageButtonClickEvent);

    }

        private void SettingPageButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SETTING
                , dataTransfer);
        }

        private void HomePageButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_HOME_PAGE
                , dataTransfer);
        }

        private void PersonalAccountButtonClickEvent(object obj)
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
