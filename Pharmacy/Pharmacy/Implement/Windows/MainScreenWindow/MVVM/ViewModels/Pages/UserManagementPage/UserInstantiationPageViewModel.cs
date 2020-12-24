using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage
{
    public class UserInstantiationPageViewModel : AbstractViewModel
    {
        private IActionListener _keyActionListener = KeyActionListener.Instance;

        private Visibility _userNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _userNameVerifiedTextBlockVisibility = Visibility.Visible;
        private Visibility _fullNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _phoneNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _newPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _verifiedPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private string _newPassword = "";
        private string _verifiedPassword = "";
        private string _newPasswordAwareTextBlockContent = "";
        private string _userNameAwareTextBlockContent = "";
        private bool _isSaveButtonRunning = false;

        public tblUser NewUser { get; set; }

        public string UserNameText
        {
            get
            {
                return NewUser.Username;
            }
            set
            {
                NewUser.Username = value;
                OnUserNameTextBoxLostFocus();
                InvalidateOwn();
            }
        }
        public string UserNameAwareTextBlockContent
        {
            get
            {
                return _userNameAwareTextBlockContent;
            }
            set
            {
                _userNameAwareTextBlockContent = value;
                InvalidateOwn();
            }
        }
        public string FullNameText
        {
            get
            {

                return NewUser.FullName;
            }
            set
            {
                NewUser.FullName = value;
                FullNameAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                    Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string PhoneText
        {
            get
            {
                return NewUser.Phone;
            }
            set
            {
                NewUser.Phone = value;
                PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                    Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string AdressText
        {
            get
            {
                return NewUser.Address;
            }
            set
            {
                NewUser.Address = value;
                InvalidateOwn();
            }
        }
        public string EmailText
        {
            get
            {
                return NewUser.Email;
            }
            set
            {
                NewUser.Email = value;
                InvalidateOwn();
            }
        }
        public string LinkText
        {
            get
            {
                return NewUser.Link;
            }
            set
            {
                NewUser.Link = value;
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
        public Visibility UserNameVerifiedTextBlockVisibility
        {
            get
            {
                return _userNameVerifiedTextBlockVisibility;
            }
            set
            {
                _userNameVerifiedTextBlockVisibility = value;
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
                return UserNameAwareTextBlockVisibility != Visibility.Visible &&
                    FullNameAwareTextBlockVisibility != Visibility.Visible &&
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
        
        public RunInputCommand SaveButtonCommand { get; set; }

        protected override void InitPropertiesRegistry()
        {
        }

        public UserInstantiationPageViewModel()
        {
            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            NewUser = new tblUser();
            NewUser.Username = "";


            UserNameAwareTextBlockVisibility = String.IsNullOrEmpty(NewUser.Username) ?
                    Visibility.Visible : Visibility.Collapsed;
            UserNameVerifiedTextBlockVisibility = Visibility.Collapsed;
            FullNameAwareTextBlockVisibility = String.IsNullOrEmpty(NewUser.FullName) ?
                 Visibility.Visible : Visibility.Collapsed;
            PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(NewUser.Phone) ?
                Visibility.Visible : Visibility.Collapsed;

        }

        private void SaveButtonClickEvent(object paramaters)
        {
        }

        private void OnUserNameTextBoxLostFocus()
        {
            UserNameAwareMessage mes = UserNameAwareMessage.Empty;
            UserNameVerifiedTextBlockVisibility = Visibility.Collapsed;

            if (String.IsNullOrEmpty(UserNameText))
            {
                UserNameAwareTextBlockVisibility = Visibility.Visible;
                mes = UserNameAwareMessage.Empty;
            }
            else if (UserNameText.Contains(" "))
            {
                UserNameAwareTextBlockVisibility = Visibility.Visible;
                mes = UserNameAwareMessage.WhiteSpaceAware;
            }
            else if (UserNameText.IndexOfAny(PharmacyDefinitions.SPECIAL_CHARS_OF_USERNAME) != -1)
            {
                UserNameAwareTextBlockVisibility = Visibility.Visible;
                mes = UserNameAwareMessage.SpecialCharacter;
            }
            else if (IsUserNameExisted())
            {
                UserNameAwareTextBlockVisibility = Visibility.Visible;
                mes = UserNameAwareMessage.UserExisted;
            }
            else
            {
                UserNameAwareTextBlockVisibility = Visibility.Collapsed;
                UserNameVerifiedTextBlockVisibility = Visibility.Visible;
            }

            UserNameAwareTextBlockContent = mes.GetStringValue();
        }



        private bool IsUserNameExisted()
        {
            bool isExisted = false;

            SQLQueryCustodian observer = new SQLQueryCustodian((sqlResult) =>
            {
                try
                {
                    isExisted = Convert.ToBoolean(sqlResult.Result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });

            DbManager.Instance.ExecuteQuery(SQLCommandKey.CHECK_USER_NAME_EXISTED_CMD_KEY,
                observer,
                UserNameText);

            return isExisted;
        }
    }
}
