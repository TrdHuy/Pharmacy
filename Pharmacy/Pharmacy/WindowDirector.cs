using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Config;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.Views;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Models.VOs;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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

        private Collection<PopupScreenWindow> _popupScreenWindows;

        #region Private properties
        private MSWindow MainScreenWindow
        {
            get
            {
                if (_mainScreenWindow == null)
                {
                    _mainScreenWindow = new MSWindow();
                    _mainScreenWindow.CloseWindowCommand = new CommandModel(CloseMainScreenWindow);
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
                    _loginWindow.CloseWindowCommand = new CommandModel(CloseLoginWindowCommand);
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
            _popupScreenWindows = new Collection<PopupScreenWindow>();
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
            DbManager.Instance.Dispose();
        }

        public AnubisMessgaeResult ShowMessageBox(
            string message,
            OwnerWindow owner = OwnerWindow.Default,
            AnubisMessageBoxType messageType = AnubisMessageBoxType.Default,
            AnubisMessageImage messageIcon = AnubisMessageImage.Non,
            string caption = "Cảnh báo!!!")
        {
            AnubisMessageBox messageBox;
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(new Uri("/Pharmacy;component/Resources/Styles/Buttons.xaml", UriKind.Relative));
            ResourceDictionary resColor = (ResourceDictionary)Application.LoadComponent(new Uri("/Pharmacy;component/Resources/Styles/Colors.xaml", UriKind.Relative));
            switch (owner)
            {
                case OwnerWindow.Default:
                    messageBox = new AnubisMessageBox(message, messageType, messageIcon, caption);
                    break;
                case OwnerWindow.LoginScreen:
                    messageBox = new AnubisMessageBox(LoginScreenWindow, message, messageType, messageIcon);
                    break;
                case OwnerWindow.MainScreen:
                    messageBox = new AnubisMessageBox(MainScreenWindow, message, messageType, messageIcon);
                    break;
                default:
                    messageBox = new AnubisMessageBox(message, messageType, messageIcon, caption);
                    break;
            }
            messageBox.CaptionContent = caption;
            messageBox.OKButtonStyle = (Style)res["OkMessageBoxButton"];
            messageBox.YesButtonStyle = (Style)res["YesMessageBoxButton"];
            messageBox.NoButtonStyle = (Style)res["NoMessageBoxButton"];
            messageBox.CancleButtonStyle = (Style)res["CancleMessageBoxButton"];
            messageBox.BorderThickness = new Thickness(1);
            messageBox.BorderBrush = (SolidColorBrush)resColor["NormalTheme_MessageBox_Border_Brush"];
            return messageBox.Show();
        }

        public AnubisMessgaeResult ShowMessageBox(
            object message,
            OwnerWindow owner = OwnerWindow.Default,
            AnubisMessageBoxType messageType = AnubisMessageBoxType.Default,
            AnubisMessageImage messageIcon = AnubisMessageImage.Non,
            string caption = "Cảnh báo!!!")
        {
            AnubisMessageBox messageBox;
            ResourceDictionary resButton = (ResourceDictionary)Application.LoadComponent(new Uri("/Pharmacy;component/Resources/Styles/Buttons.xaml", UriKind.Relative));
            ResourceDictionary resColor = (ResourceDictionary)Application.LoadComponent(new Uri("/Pharmacy;component/Resources/Styles/Colors.xaml", UriKind.Relative));
            switch (owner)
            {
                case OwnerWindow.Default:
                    messageBox = new AnubisMessageBox(null, message, messageType);
                    break;
                case OwnerWindow.LoginScreen:
                    messageBox = new AnubisMessageBox(LoginScreenWindow, message, messageType, messageIcon);
                    break;
                case OwnerWindow.MainScreen:
                    messageBox = new AnubisMessageBox(MainScreenWindow, message, messageType, messageIcon);
                    break;
                default:
                    messageBox = new AnubisMessageBox(null, message, messageType);
                    break;
            }
            messageBox.CaptionContent = caption;
            messageBox.OKButtonStyle = (Style)resButton["OkMessageBoxButton"];
            messageBox.YesButtonStyle = (Style)resButton["YesMessageBoxButton"];
            messageBox.NoButtonStyle = (Style)resButton["NoMessageBoxButton"];
            messageBox.CancleButtonStyle = (Style)resButton["CancleMessageBoxButton"];
            messageBox.BorderThickness = new Thickness(1);
            messageBox.BorderBrush = (SolidColorBrush)resColor["NormalTheme_MessageBox_Border_Brush"];
            return messageBox.Show();
        }

        public int ShowMultiOptionMessageBox(
            string message,
            IEnumerable<OsirisButton> optionsSource,
            OwnerWindow owner = OwnerWindow.Default,
            AnubisMessageImage messageIcon = AnubisMessageImage.Non,
            string caption = "Cảnh báo!!!")
        {
            AnubisMessageBox messageBox;
            switch (owner)
            {
                case OwnerWindow.Default:
                    messageBox = new AnubisMessageBox(message, AnubisMessageBoxType.MultiOptions, messageIcon, caption);
                    break;
                case OwnerWindow.LoginScreen:
                    messageBox = new AnubisMessageBox(LoginScreenWindow, message, AnubisMessageBoxType.MultiOptions, messageIcon);
                    break;
                case OwnerWindow.MainScreen:
                    messageBox = new AnubisMessageBox(MainScreenWindow, message, AnubisMessageBoxType.MultiOptions, messageIcon);
                    break;
                default:
                    messageBox = new AnubisMessageBox(message, AnubisMessageBoxType.MultiOptions, messageIcon, caption);
                    break;
            }
            var defaultOptStyle = new Style();
            defaultOptStyle.TargetType = typeof(OsirisButton);
            defaultOptStyle.Setters.Add(new Setter(OsirisButton.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            defaultOptStyle.Setters.Add(new Setter(OsirisButton.BorderThicknessProperty, new Thickness(0)));
            defaultOptStyle.Setters.Add(new Setter(OsirisButton.MarginProperty, new Thickness(20, 5, 20, 5)));
            defaultOptStyle.Setters.Add(new Setter(OsirisButton.FontSizeProperty, 12d));
            defaultOptStyle.Setters.Add(new Setter(OsirisButton.FontWeightProperty, FontWeights.Normal));

            defaultOptStyle.Setters.Add(new Setter(OsirisButton.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            defaultOptStyle.Setters.Add(new Setter(OsirisButton.HorizontalContentAlignmentProperty, HorizontalAlignment.Left));

            Trigger mouseOverTrigger = new Trigger();
            mouseOverTrigger.Property = OsirisButton.IsMouseOverProperty;
            mouseOverTrigger.Value = true;
            Setter setterMouseOverTrigger_1 = new Setter();
            setterMouseOverTrigger_1.Property = OsirisButton.ForegroundProperty;
            setterMouseOverTrigger_1.Value = Brushes.LightGreen;
            mouseOverTrigger.Setters.Add(setterMouseOverTrigger_1);
            Setter setterMouseOverTrigger_2 = new Setter();
            setterMouseOverTrigger_2.Property = OsirisButton.BorderThicknessProperty;
            setterMouseOverTrigger_2.Value = new Thickness(1);
            mouseOverTrigger.Setters.Add(setterMouseOverTrigger_2);

            Trigger mousePressedTrigger = new Trigger();
            mousePressedTrigger.Property = OsirisButton.IsPressedProperty;
            mousePressedTrigger.Value = true;
            Setter setterMousePressedTrigger_1 = new Setter();
            setterMousePressedTrigger_1.Property = OsirisButton.ForegroundProperty;
            setterMousePressedTrigger_1.Value = Brushes.DarkGreen;
            mousePressedTrigger.Setters.Add(setterMousePressedTrigger_1);
            Setter setterMousePressedTrigger_2 = new Setter();
            setterMousePressedTrigger_2.Property = OsirisButton.BorderThicknessProperty;
            setterMousePressedTrigger_2.Value = new Thickness(1);
            mousePressedTrigger.Setters.Add(setterMousePressedTrigger_2);

            defaultOptStyle.Triggers.Add(mouseOverTrigger);
            defaultOptStyle.Triggers.Add(mousePressedTrigger);

            foreach (var btn in optionsSource)
            {
                btn.Style = defaultOptStyle;
            }

            messageBox.CaptionContent = caption;
            messageBox.AnubisMesOptionsSource = optionsSource;
            messageBox.BorderThickness = new Thickness(1);
            messageBox.FontSize = 15;
            messageBox.FontWeight = FontWeights.Bold;
            messageBox.Show();
            return messageBox.SelectedOption;
        }

        public void ShowPopupScreenWindow(PopupScreenWindowViewModel dataContext)
        {
            PopupScreenWindow popup = new PopupScreenWindow()
            {
                DataContext = dataContext
            };

            popup.CloseWindowCommand = new CommandModel(ClosePopupWindown);
            _popupScreenWindows.Add(popup);

            popup.Show();
        }

        public void ShowDialogPopupScreenWindow(PopupScreenWindowViewModel dataContext)
        {
            PopupScreenWindow popup = new PopupScreenWindow();
            popup.DataContext = dataContext;
            popup.CloseWindowCommand = new CommandModel(ClosePopupWindown);
            _popupScreenWindows.Add(popup);

            popup.ShowDialog();
        }

        public void ShowPopupWindow(PSW_ContentVO vo)
        {
            var titleBarHeight = 30d;
            PopupScreenWindow popup = new PopupScreenWindow();

            ResourceDictionary resDimen = (ResourceDictionary)Application.LoadComponent(new Uri("/Pharmacy;component/Resources/Styles/Dimens.xaml", UriKind.Relative));

            popup.Height = vo?.DesignHeight + titleBarHeight ?? (double)resDimen["DefaultPopupWindowHeight"];
            popup.Width = vo?.DesignWidth ?? (double)resDimen["DefaultPopupWindowWidth"];
            popup.MinHeight = popup.Height;
            popup.MinWidth = popup.Width;

            popup.DataContext = new PopupScreenWindowViewModel(vo);

            popup.CloseWindowCommand = new CommandModel(ClosePopupWindown);
            _popupScreenWindows.Add(popup);


            popup.Show();
        }

        private void ClosePopupWindown(object paramater)
        {
            var ctrl = paramater as PopupScreenWindow;

            if (ctrl != null)
            {
                _popupScreenWindows.Remove(ctrl);
                ctrl.ForceClose();
            }
        }

    }
}
