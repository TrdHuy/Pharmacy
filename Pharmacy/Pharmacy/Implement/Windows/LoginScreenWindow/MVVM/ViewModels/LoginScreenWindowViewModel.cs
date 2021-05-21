using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.IO;
using System.Reflection;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.ViewModels
{
    public class LoginScreenWindowViewModel : BaseViewModel
    {
        private static Logger logger = new Logger("LoginScreenWindowViewModel");

        private KeyActionListener _keyActionListener = KeyActionListener.Current;
        private string _userName;
        private bool _isLoginButtonRunnig = false;

        public CommandExecuterModel SystemLoginButton { get; set; }
        public CommandExecuterModel BugReportButton { get; set; }
        public CommandExecuterModel ContactUsButton { get; set; }

        public bool IsUserRemember
        {
            get { return Properties.Settings.Default.IsUserRemember; }
            set
            {
                Properties.Settings.Default.IsUserRemember = value;
                Properties.Settings.Default.Save();
                InvalidateOwn();
            }
        }
        public bool IsLoginButtonRunning
        {
            get { return _isLoginButtonRunnig; }
            set
            {
                _isLoginButtonRunnig = value;
                InvalidateOwn();
            }
        }
        public string UserName
        {
            get
            {

                if (IsUserRemember)
                    return Properties.Settings.Default.UserName;
                else
                    return _userName;
            }
            set
            {
                if (IsUserRemember)
                {
                    Properties.Settings.Default.UserName = value;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    _userName = value;
                }
                InvalidateOwn();

            }
        }

        public LoginScreenWindowViewModel()
        {
            SystemLoginButton = new CommandExecuterModel(SystemLoginButtonClickEvent);
            BugReportButton = new CommandExecuterModel(BugReportButtonClickEvent);
            ContactUsButton = new CommandExecuterModel(ContactUsButtonClickEvent);
            PharmacyExtension.GrantAccess();
        }

        private ICommandExecuter ContactUsButtonClickEvent(object arg)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = arg;
            return _keyActionListener.OnKey(this
                , logger
                , WindowTag.WINDOW_TAG_LOGIN_SCREEN
                , KeyFeatureTag.KEY_TAG_LSW_CUSTOMER_SERVICE_FEATURE
                , dataTransfer) as ICommandExecuter;
        }

        private ICommandExecuter BugReportButtonClickEvent(object arg)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = arg;
            return _keyActionListener.OnKey(this
                , logger
                , WindowTag.WINDOW_TAG_LOGIN_SCREEN
                , KeyFeatureTag.KEY_TAG_LSW_BUG_REPORT_FEATURE
                , dataTransfer) as ICommandExecuter;
        }

        private ICommandExecuter SystemLoginButtonClickEvent(object obj)
        {
            IsLoginButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = obj;
            return _keyActionListener.OnKey(this
                , logger
                , WindowTag.WINDOW_TAG_LOGIN_SCREEN
                , KeyFeatureTag.KEY_TAG_LSW_LOGIN_FEATURE
                , dataTransfer) as ICommandExecuter;
        }
    }
}
