using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.Views;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static App _instance;

        private ApplicationDataContext _applicationDataContext;
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private bool _isMainScreenWindowExit = false;
        private bool _isLoginOn = false;
        private Window _loginWindow;
        private Window _mainScreenWindow;

        public Window MainScreenWindow { get { return _mainScreenWindow; } }
        public Window LoginScreenWindow { get { return _loginWindow; } }
        public tblUser CurrentUser
        {
            get
            {
                return _applicationDataContext.CurrentUser;
            }
        }
        public static new App Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new App();
                }
                return _instance;
            }
        }

        private App() : base()
        {
            _instance = this;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _loginWindow = new Lazy<LoginScreenWindow>(() => new LoginScreenWindow()).Value;
            _mainScreenWindow = new Lazy<MSWindow>(() => new MSWindow()).Value;
            _applicationDataContext = new ApplicationDataContext();

            if (MainScreenWindow != null)
            {
                MainScreenWindow.Closing -= MainScreenWindow_Closing;
            }
            MainScreenWindow.Closing += MainScreenWindow_Closing;
            LoginScreenWindow.Closing += LoginScreenWindow_Closing;
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += ShowMainWindow;
            _notifyIcon.Icon = Pharmacy.Properties.Resources.notifyIcon;
            _notifyIcon.Visible = false;

            CreateContextMenu();

            LoginScreenWindow.Show();
        }

        private void MainScreenWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isMainScreenWindowExit)
            {
                e.Cancel = true;
                MainScreenWindow.Hide(); // A hidden window can be shown again, a closed one not
            }
        }

        private void LoginScreenWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            if (!_isMainScreenWindowExit)
            {
                _isMainScreenWindowExit = true;
                MainScreenWindow.Close();
            }

            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        private void ShowMainWindow(object sender, EventArgs e)
        {
            if (MainScreenWindow.IsVisible)
            {
                if (MainScreenWindow.WindowState == WindowState.Minimized)
                {
                    MainScreenWindow.WindowState = WindowState.Normal;
                }
                MainScreenWindow.Activate();
            }
            else
            {
                if (_isLoginOn)
                {
                    MainScreenWindow.Show();
                }
                else
                {
                    LoginScreenWindow.Show();
                }
            }
        }
        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip =
              new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Trang chủ").Click += ShowMainWindow;
            _notifyIcon.ContextMenuStrip.Items.Add("Đăng xuất").Click += OnLoggingOut;
            _notifyIcon.ContextMenuStrip.Items.Add("Thoát").Click += ExitApplication;
        }
        private void OnLoggingOut(object sender, EventArgs e)
        {
            _isLoginOn = false;
            MainScreenWindow.Hide();
            LoginScreenWindow.Show();
        }
        private void ExitApplication(object sender, EventArgs e)
        {
            LoginScreenWindow.Close();
        }


        public void SessionIDInstansiation(tblUser curUser)
        {
            string connectionID = _applicationDataContext.GenerateConnectionID();
            string sessionID = DateTime.Now + "/" + curUser.Username + "/" + connectionID;
            _applicationDataContext.UpdateSessionInfo(connectionID, sessionID, curUser);
            _isLoginOn = true;
        }

        public void ShowNotifyIcon()
        {
            _notifyIcon.Visible = true;
        }

        public void SubcribeProperty(PropertyObserver observer)
        {
            _applicationDataContext.SubcirbeProperty(observer);
        }


        /// <summary>
        /// Global data container, used to store shared data between all class of project
        /// </summary>
        private class ApplicationDataContext
        {
            private ObservableProperty _opUser = new ObservableProperty();

            public string ConnectionID { get; set; }
            public string SessionID { get; set; }
            public tblUser CurrentUser
            {
                get { return (tblUser)_opUser.Value; }
                set { _opUser.Value = value; }
            }

            public void SubcirbeProperty(PropertyObserver propObserver)
            {
                Type t = propObserver.PropType;
                switch (t)
                {
                    case Type user when user == typeof(tblUser):
                        _opUser.Subcribe(propObserver);
                        _opUser.NotifyChange(_opUser);
                        break;
                    default:
                        break;
                }
            }


            public void UpdateSessionInfo(string con, string ses, tblUser curUser)
            {
                ConnectionID = con;
                SessionID = ses;
                CurrentUser = curUser;
            }

            public string GenerateConnectionID()
            {
                Random rd = new Random();
                int ssIDLenght = 8;
                const string chars = "ABCDEFGHIJKLMNOPQRSTUWXYZ0123456789abcdefghijklmnopqrstuwxyz";
                return new string(Enumerable.Repeat(chars, ssIDLenght)
                    .Select(s => s[rd.Next(s.Length)])
                    .ToArray());
            }
        }


    }


}
