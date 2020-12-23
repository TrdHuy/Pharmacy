using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage
{
    public class UserModificationPageViewModel : AbstractViewModel
    {
        private IActionListener _keyActionListener = KeyActionListener.Instance;

        private tblUser _currentModifiedUser;
        private Visibility _userNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _fullNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _phoneNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _newPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _verifiedPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private string _newPassword = "";
        private string _verifiedPassword = "";
        private string _newPasswordAwareTextBlockContent = "";
        private bool _isSaveButtonRunning = false;

        public tblUser CurrentModifiedUser
        {
            get
            {
                return _currentModifiedUser;
            }
            set
            {
                _currentModifiedUser = value;
            }
        }

        public string UserNameText
        {
            get
            {
                UserNameAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedUser.Username) ?
                  Visibility.Visible : Visibility.Collapsed;
                return CurrentModifiedUser.Username;
            }
            set
            {
                CurrentModifiedUser.Username = value;
                UserNameAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                 Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string FullNameText
        {
            get
            {
                FullNameAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedUser.FullName) ?
                  Visibility.Visible : Visibility.Collapsed;
                return CurrentModifiedUser.FullName;
            }
            set
            {
                CurrentModifiedUser.FullName = value;
                FullNameAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                    Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string PhoneText
        {
            get
            {
                PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedUser.Phone) ?
                 Visibility.Visible : Visibility.Collapsed;
                return CurrentModifiedUser.Phone;
            }
            set
            {
                CurrentModifiedUser.Phone = value;
                PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                    Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string AdressText
        {
            get
            {
                return CurrentModifiedUser.Address;
            }
            set
            {
                CurrentModifiedUser.Address = value;
                InvalidateOwn();
            }
        }
        public string EmailText
        {
            get
            {
                return CurrentModifiedUser.Email;
            }
            set
            {
                CurrentModifiedUser.Email = value;
                InvalidateOwn();
            }
        }
        public string LinkText
        {
            get
            {
                return CurrentModifiedUser.Link;
            }
            set
            {
                CurrentModifiedUser.Link = value;
                InvalidateOwn();
            }
        }

        public Visibility UserNameAwareTextBlockVisibility
        {
            get
            {
                return _userNameAwareTextBlockVisibility;
            }
            set
            {
                _userNameAwareTextBlockVisibility = value;
                InvalidateOwn();
            }
        }
        public Visibility FullNameAwareTextBlockVisibility
        {
            get
            {
                return _fullNameAwareTextBlockVisibility;
            }
            set
            {
                _fullNameAwareTextBlockVisibility = value;
                InvalidateOwn();
            }
        }
        public Visibility PhoneAwareTextBlockVisibility
        {
            get
            {
                return _phoneNameAwareTextBlockVisibility;
            }
            set
            {
                _phoneNameAwareTextBlockVisibility = value;
                InvalidateOwn();
            }
        }
        public Visibility NewPasswordAwareTextBlockVisibility
        {
            get
            {
                return _newPasswordAwareTextBlockVisibility;
            }
            set
            {
                _newPasswordAwareTextBlockVisibility = value;
                InvalidateOwn();
            }
        }
        public Visibility VerifiedPasswordAwareTextBlockVisibility
        {
            get
            {
                return _verifiedPasswordAwareTextBlockVisibility;
            }
            set
            {
                _verifiedPasswordAwareTextBlockVisibility = value;
                InvalidateOwn();
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                InvalidateOwn();
            }
        }
        public string VerifiedPassword
        {
            get { return _verifiedPassword; }
            set
            {
                _verifiedPassword = value;
                InvalidateOwn();
            }
        }
        public string NewPasswordAwareTextBlockContent
        {
            get
            {
                return _newPasswordAwareTextBlockContent;
            }
            set
            {
                _newPasswordAwareTextBlockContent = value;
                InvalidateOwn();
            }
        }
        public bool IsSaveButtonCanPerform
        {
            get
            {
                return FullNameAwareTextBlockVisibility != Visibility.Visible &&
                    PhoneAwareTextBlockVisibility != Visibility.Visible &&
                    NewPasswordAwareTextBlockVisibility != Visibility.Visible &&
                    VerifiedPasswordAwareTextBlockVisibility != Visibility.Visible;
            }
        }
        public bool IsSaveButtonRunning
        {
            get
            {
                return _isSaveButtonRunning;
            }
            set
            {
                _isSaveButtonRunning = value;
                InvalidateOwn();
            }
        }

        protected override void InitPropertiesRegistry()
        {
        }

        public UserModificationPageViewModel()
        {
            CurrentModifiedUser = MSW_DataFlowHost.Current.CurrentModifiedUser;
        }
    }
}
