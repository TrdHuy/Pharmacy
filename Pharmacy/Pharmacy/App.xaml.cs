using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Config;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.Views;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels;
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
        private WindowDirector _winDirector;

        //Only bind this to un-modified property
        public tblUser CurrentUser
        {
            get { return _applicationDataContext.CurrentUser; }
        }
        public bool IsOnline
        {
            get { return !String.IsNullOrEmpty(_applicationDataContext.SessionID); }
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
            _winDirector = new WindowDirector();
            _applicationDataContext = new ApplicationDataContext();
            _winDirector.DisplayStatus = WindowDisplayStatus.OnLoginScreen;
        }

        public void SessionIDInstansiation(tblUser curUser)
        {
            string connectionID = _applicationDataContext.GenerateConnectionID();
            string sessionID = DateTime.Now + "/" + curUser.Username + "/" + connectionID;
            _applicationDataContext.UpdateSessionInfo(connectionID, sessionID, curUser);
            _winDirector.DisplayStatus = WindowDisplayStatus.OnMainScreen;
        }

        public void SubcribeProperty(PropertyObserver observer)
        {
            if (observer != null)
            {
                _applicationDataContext.SubcirbeProperty(observer);
            }
            else
            {
                throw new ArgumentNullException(observer.GetType().Name, "PropertyObserver was null, please instantinate first!");
            }
        }

        public void ClearSessionID()
        {
            _applicationDataContext.UpdateSessionInfo("", "", null);
        }

        public AnubisMessgaeResult ShowApplicationMessageBox(
            string message,
            AnubisMessageBoxType mesType = AnubisMessageBoxType.Default,
            AnubisMessageImage mesIcon = AnubisMessageImage.Non,
            OwnerWindow owner = OwnerWindow.Default,
            string caption = "Cảnh báo!!!")
        {
            return _winDirector.ShowMessageBox(message, owner, mesType, mesIcon, caption);
        }

        public AnubisMessgaeResult ShowApplicationMessageBox(
            object message,
            AnubisMessageBoxType mesType = AnubisMessageBoxType.Default,
            AnubisMessageImage mesIcon = AnubisMessageImage.Non,
            OwnerWindow owner = OwnerWindow.Default,
            string caption = "Cảnh báo!!!")
        {
            return _winDirector.ShowMessageBox(message, owner, mesType, mesIcon, caption);
        }

        public void ShowPopupScreenWindow(PopupScreenWindowViewModel dataContext)
        {
            _winDirector.ShowPopupScreenWindow(dataContext);
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
