using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Config;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.Views;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy
{
    public enum WindowDisplayStatus
    {
        OnLoginScreen = 1,
        OnMainScreen = 2,
        AppExit = 10
    }
    public enum OwnerWindow
    {
        Default = 0,
        LoginScreen = 1,
        MainScreen = 2
    }

    public class WindowDirector
    {
        private bool _notityIconEnable = RUNE.IS_SUPPORT_NOTIFY_ICON;
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private LoginScreenWindow _loginWindow;
        private MSWindow _mainScreenWindow;
        private WindowDisplayStatus _curStatus;
        private WindowDisplayStatus _preStatus;

        private bool _isLoginWindowExited = false;
        private bool _isMainWindowExited = false;

        #region Private properties
        private MSWindow MainScreenWindow
        {
            get
            {
                if (_mainScreenWindow == null)
                {
                    _mainScreenWindow = new MSWindow();
                    _mainScreenWindow.CloseWindowCommand = new RunInputCommand(CloseMainScreenWindow);
                }
                return _mainScreenWindow;
            }
            set
            {
                _mainScreenWindow = value;
            }
        }
        private LoginScreenWindow LoginScreenWindow
        {
            get
            {
                if (_loginWindow == null)
                {
                    _loginWindow = new LoginScreenWindow();
                    _loginWindow.CloseWindowCommand = new RunInputCommand(CloseLoginWindowCommand);
                }
                return _loginWindow;
            }
            set
            {
                _loginWindow = value;
            }
        }
        #endregion

        #region Public properties
        public WindowDisplayStatus DisplayStatus
        {
            get { return _curStatus; }
            set
            {
                _preStatus = _curStatus;
                _curStatus = value;
                WindowDisplayStatusChanged();
            }
        }
        #endregion

        public WindowDirector()
        {
            SetupFeatures();
        }


        private void SetupFeatures()
        {
            if (_notityIconEnable)
            {

                _notifyIcon = new System.Windows.Forms.NotifyIcon();
                _notifyIcon.DoubleClick += ShowMainWindow;
                _notifyIcon.Icon = Pharmacy.Properties.Resources.notifyIcon;
                _notifyIcon.Visible = true;

                _notifyIcon.ContextMenuStrip =
               new System.Windows.Forms.ContextMenuStrip();
                _notifyIcon.ContextMenuStrip.Items.Add("Trang chủ").Click += ShowMainWindow;
                _notifyIcon.ContextMenuStrip.Items.Add("Đăng xuất").Click += OnLoggingOut;
                _notifyIcon.ContextMenuStrip.Items.Add("Thoát").Click += ExitApplication;
            }
        }

        private void CloseMainScreenWindow(object obj)
        {
            if (_notityIconEnable && DisplayStatus != WindowDisplayStatus.AppExit)
            {
                MainScreenWindow?.Hide();
            }
            else
            {
                ExitApplication();
            }
        }

        private void CloseLoginWindowCommand(object obj)
        {
            ExitApplication();
        }

        private void ShowMainWindow(object sender, EventArgs e)
        {
            if (App.Current.IsOnline)
            {
                DisplayStatus = WindowDisplayStatus.OnMainScreen;
            }
            else
            {
                DisplayStatus = WindowDisplayStatus.OnLoginScreen;
            }
        }

        private void OnLoggingOut(object sender, EventArgs e)
        {
            DisplayStatus = WindowDisplayStatus.OnLoginScreen;
            App.Current.ClearSessionID();
            MainScreenWindow.ForceClose();
            _mainScreenWindow = null;
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            DisplayStatus = WindowDisplayStatus.AppExit;
            App.Current.ClearSessionID();
        }

        private void WindowDisplayStatusChanged()
        {
            if (DisplayStatus == WindowDisplayStatus.AppExit)
            {
                ExitApplication();
            }
            else
            {
                ClosePreviousWindow();
                ShowCurrentWindow();
            }
        }

        private void ClosePreviousWindow()
        {

            if (_preStatus != DisplayStatus)
            {
                switch (_preStatus)
                {
                    case WindowDisplayStatus.OnLoginScreen:
                        if (DisplayStatus == WindowDisplayStatus.OnMainScreen)
                        {
                            LoginScreenWindow?.Hide();
                        }
                        else
                        {
                            ExitApplication();
                        }
                        break;
                    case WindowDisplayStatus.OnMainScreen:
                        MainScreenWindow?.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ShowCurrentWindow()
        {
            switch (DisplayStatus)
            {
                case WindowDisplayStatus.OnLoginScreen:
                    LoginScreenWindow?.Show();
                    break;
                case WindowDisplayStatus.OnMainScreen:
                    MainScreenWindow?.Show();
                    break;
                default:
                    break;
            }
        }

        private void ExitApplication()
        {
            if (!_isLoginWindowExited)
            {
                _isLoginWindowExited = true;
                LoginScreenWindow?.ForceClose();
            }
            if (!_isMainWindowExited)
            {
                _isMainWindowExited = true;
                MainScreenWindow?.ForceClose();
            }
            if (_notityIconEnable)
            {
                _notifyIcon?.Dispose();
                _notifyIcon = null;
            }
        }

        public AnubisMessgaeResult ShowMessageBox(
            string message,
            OwnerWindow owner = OwnerWindow.Default,
            AnubisMessageBoxType messageType = AnubisMessageBoxType.Default,
            AnubisMessageImage messageIcon = AnubisMessageImage.Non,
            string caption = "Cảnh báo!!!")
        {
            AnubisMessageBox messageBox;
            switch (owner)
            {
                case OwnerWindow.Default:
                    messageBox = new AnubisMessageBox(message);
                    break;
                case OwnerWindow.LoginScreen:
                    messageBox = new AnubisMessageBox(LoginScreenWindow, message, messageType, messageIcon);
                    break;
                case OwnerWindow.MainScreen:
                    messageBox = new AnubisMessageBox(MainScreenWindow, message, messageType, messageIcon);
                    break;
                default:
                    messageBox = new AnubisMessageBox(message);
                    break;
            }
            messageBox.CaptionContent = caption;
            return messageBox.Show();
        }
    }
}
