using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.LoginScreenWindow.Core.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.ViewModels
{
    class LoginScreenWindowViewModel : BaseLoginScreenWindowViewModel
    {
        private IActionListener _keyActionListener = KeyActionListener.Instance;

        public RunInputCommand SystemLoginButton {get; set;}

        public LoginScreenWindowViewModel()
        {
            SystemLoginButton = new RunInputCommand(SystemLoginButtonClickEvent);
        }

        protected override void InitPropertiesRegistry()
        {

        }

        private async void SystemLoginButtonClickEvent(object obj)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_LOGIN_SCREEN
                , KeyFeatureTag.KEY_TAG_LSW_LOGIN_FEATURE
                , dataTransfer);
        }
    }
}
