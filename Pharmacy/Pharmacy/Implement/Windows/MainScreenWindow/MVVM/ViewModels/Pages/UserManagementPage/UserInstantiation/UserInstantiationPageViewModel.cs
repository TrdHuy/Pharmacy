using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Config;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage.UserInstantiation.OVs;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage.UserInstantiation
{
    internal class UserInstantiationPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("UserInstantiationPageViewModel");

        private KeyActionListener _keyActionListener = KeyActionListener.Current;

        private Visibility _userNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _userNameVerifiedTextBlockVisibility = Visibility.Visible;
        private Visibility _fullNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _phoneNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _newPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _verifiedPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _jobTitleAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _userNameVerifingTextBlockVisibility = Visibility.Collapsed;
        private Visibility _cameraButtonVisibility = Visibility.Visible;

        private string _newPassword = "";
        private string _verifiedPassword = "";
        private string _newPasswordAwareTextBlockContent = "";
        private string _userNameAwareTextBlockContent = "";
        private bool _isDoneEvaluateUserName = false;
        private bool _isUsernameTextBoxEnable = true;
        private ImageSource _userImageSource;

        #region Public properties
        public tblUser NewUser { get; set; }
        public string UserImageFileName { get; set; }

        public ImageSource UserImageSource
        {
            get
            {
                return _userImageSource;
            }
            set
            {
                _userImageSource = value;
                InvalidateOwn();
            }
        }

        public string UserNameText
        {
            get
            {
                return NewUser.Username;
            }
            set
            {
                NewUser.Username = value;
                UsernameAssessFeasibility();
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
                FullNameAwareTextBlockVisibility = string.IsNullOrEmpty(value) ?
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
                PhoneAwareTextBlockVisibility = string.IsNullOrEmpty(value) ?
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
        public string JobText
        {
            get
            {
                return NewUser.Job;
            }
            set
            {
                NewUser.Job = value;
                JobTitleAwareTextBlockVisibility = string.IsNullOrEmpty(value) ?
                    Visibility.Visible : Visibility.Collapsed;
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
        public Visibility CameraButtonVisibility
        {
            get
            {
                return _cameraButtonVisibility;
            }
            set
            {
                _cameraButtonVisibility = value;
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
        public Visibility JobTitleAwareTextBlockVisibility
        {
            get
            {
                return _jobTitleAwareTextBlockVisibility;
            }
            set
            {
                _jobTitleAwareTextBlockVisibility = value;
                InvalidateOwn();
            }
        }
        public Visibility UserNameVerifingTextBlockVisibility
        {
            get
            {
                return _userNameVerifingTextBlockVisibility;
            }
            set
            {
                _userNameVerifingTextBlockVisibility = value;
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
                    UserNameVerifiedTextBlockVisibility == Visibility.Visible &&
                    IsDoneEvaluateUserName &&
                    FullNameAwareTextBlockVisibility != Visibility.Visible &&
                    PhoneAwareTextBlockVisibility != Visibility.Visible &&
                    NewPasswordAwareTextBlockVisibility != Visibility.Visible &&
                    VerifiedPasswordAwareTextBlockVisibility != Visibility.Visible &&
                    JobTitleAwareTextBlockVisibility != Visibility.Visible;
            }
        }
        public bool IsUsernameTextBoxEnable
        {
            get
            {
                return _isUsernameTextBoxEnable;
            }
            set
            {
                _isUsernameTextBoxEnable = value;
                InvalidateOwn();
            }
        }
        public bool IsDoneEvaluateUserName
        {
            get
            {
                return _isDoneEvaluateUserName;
            }
            set
            {
                _isDoneEvaluateUserName = value;
            }
        }

        public MSW_UMP_UIP_ButtomCommandOV ButtonCommandOV { get; set; }
        public EventHandleCommand GridSizeChangedCommand { get; set; }
        public EventHandleCommand NewPasswordChangedCommand { get; set; }
        public EventHandleCommand VerifiedPasswordChangedCommand { get; set; }
        #endregion

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_UMP_UIP_ButtomCommandOV(this);

            NewUser = new tblUser();
            NewUser.Username = "";

            UserNameAwareTextBlockVisibility = string.IsNullOrEmpty(NewUser.Username) ?
                Visibility.Visible : Visibility.Collapsed;
            UserNameAwareTextBlockContent = UserNameAwareMessage.Empty.GetStringValue();
            UserNameVerifiedTextBlockVisibility = Visibility.Collapsed;
            UserNameVerifingTextBlockVisibility = Visibility.Collapsed;
            FullNameAwareTextBlockVisibility = string.IsNullOrEmpty(NewUser.FullName) ?
                Visibility.Visible : Visibility.Collapsed;
            PhoneAwareTextBlockVisibility = string.IsNullOrEmpty(NewUser.Phone) ?
                Visibility.Visible : Visibility.Collapsed;
            JobTitleAwareTextBlockVisibility = string.IsNullOrEmpty(NewUser.Job) ?
                Visibility.Visible : Visibility.Collapsed;

            UserImageSource = FileIOUtil.
                GetBitmapFromName(NewUser.Username, FileIOUtil.USER_IMAGE_FOLDER_NAME).
                ToImageSource();

            NewPasswordChangedCommand = new EventHandleCommand(OnNewPasswordChagedEvent);
            VerifiedPasswordChangedCommand = new EventHandleCommand(OnVerifiedPasswordChagedEvent);

        }

        protected override void OnInitialized()
        {
        }

        private void OnVerifiedPasswordChagedEvent(object sender, EventArgs e, object paramater)
        {
            PasswordBox ctrl = (PasswordBox)sender;
            VerifiedPassword = ctrl.Password;
            UpdatePasswordAwareTextBlock();
        }

        private void OnNewPasswordChagedEvent(object sender, EventArgs e, object paramater)
        {
            PasswordBox ctrl = (PasswordBox)sender;
            NewPassword = ctrl.Password;
            UpdatePasswordAwareTextBlock();
        }

        private void UsernameAssessFeasibility()
        {
            IsDoneEvaluateUserName = false;
            UserNameVerifingTextBlockVisibility = Visibility.Visible;
            UserNameVerifiedTextBlockVisibility = Visibility.Collapsed;
            UserNameAwareTextBlockVisibility = Visibility.Collapsed;
            IsUsernameTextBoxEnable = false;

            UserNameAwareMessage mes = UserNameAwareMessage.Empty;
            UserNameVerifiedTextBlockVisibility = Visibility.Collapsed;

            if (string.IsNullOrEmpty(UserNameText))
            {
                mes = UserNameAwareMessage.Empty;
                ShowUsernameAlertMessage(mes);
                return;
            }
            else if (UserNameText.Contains(" "))
            {
                mes = UserNameAwareMessage.WhiteSpaceAware;
                ShowUsernameAlertMessage(mes);
                return;
            }
            else if (UserNameText.IndexOfAny(PharmacyDefinitions.SPECIAL_CHARS_OF_USERNAME) != -1)
            {
                mes = UserNameAwareMessage.SpecialCharacter;
                ShowUsernameAlertMessage(mes);
                return;
            }

            // Last condition for user name
            IsUserNameExisted(mes);
        }

        private void ShowUsernameAlertMessage(UserNameAwareMessage mes)
        {
            UserNameAwareTextBlockContent = mes.GetStringValue();
            UserNameAwareTextBlockVisibility = Visibility.Visible;
            IsDoneEvaluateUserName = true;
            IsUsernameTextBoxEnable = true;
            UserNameVerifingTextBlockVisibility = Visibility.Collapsed;
        }

        private void IsUserNameExisted(UserNameAwareMessage mes)
        {
            bool isExisted = false;

            SQLQueryCustodian observer = new SQLQueryCustodian((sqlResult) =>
            {
                try
                {
                    isExisted = Convert.ToBoolean(sqlResult.Result);
                    if (isExisted)
                    {
                        UserNameAwareTextBlockVisibility = Visibility.Visible;
                        mes = UserNameAwareMessage.UserExisted;
                        UserNameAwareTextBlockContent = mes.GetStringValue();
                    }
                    else
                    {
                        UserNameAwareTextBlockVisibility = Visibility.Collapsed;
                        UserNameVerifiedTextBlockVisibility = Visibility.Visible;
                    }

                    IsDoneEvaluateUserName = true;
                    IsUsernameTextBoxEnable = true;
                    UserNameVerifingTextBlockVisibility = Visibility.Collapsed;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });

            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.CHECK_USER_NAME_EXISTED_CMD_KEY,
                1000,
                observer,
                UserNameText);
        }

        private void UpdatePasswordAwareTextBlock()
        {
            UpdateNewPasswordAwareTextBlock();
            UpdateVerifiedPasswordAwareTextBlock();

            if (string.IsNullOrEmpty(NewPassword) &&
                string.IsNullOrEmpty(VerifiedPassword))
            {
                NewPasswordAwareTextBlockVisibility = Visibility.Collapsed;
                VerifiedPasswordAwareTextBlockVisibility = Visibility.Collapsed;
            }
        }

        private void UpdateVerifiedPasswordAwareTextBlock()
        {
            VerifiedPasswordAwareTextBlockVisibility = VerifiedPassword.Equals(NewPassword) ?
                Visibility.Collapsed : Visibility.Visible;
        }

        private void UpdateNewPasswordAwareTextBlock()
        {
            NewPasswordAwareMessage mes = NewPasswordAwareMessage.WhiteSpaceAware;

            if (NewPassword.Contains(" "))
            {
                NewPasswordAwareTextBlockVisibility = Visibility.Visible;
                mes = NewPasswordAwareMessage.WhiteSpaceAware;
            }
            else if (NewPassword.Length < PharmacyDefinitions.MINIMUM_PASSWORD_LENGHT)
            {
                NewPasswordAwareTextBlockVisibility = Visibility.Visible;
                mes = NewPasswordAwareMessage.NotMeetLenght;
            }
            else if (NewPassword.IndexOfAny(PharmacyDefinitions.SPECIAL_CHARS_OF_PASSWORD) == -1)
            {
                NewPasswordAwareTextBlockVisibility = Visibility.Visible;
                mes = NewPasswordAwareMessage.WrongFormat;
            }
            else
            {
                NewPasswordAwareTextBlockVisibility = Visibility.Collapsed;
            }

            NewPasswordAwareTextBlockContent = mes.GetStringValue();
        }

    }
}
