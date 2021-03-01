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
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage
{
    public class UserModificationPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("UserModificationPageViewModel");

        private KeyActionListener _keyActionListener = KeyActionListener.Instance;

        private tblUser _currentModifiedUser;
        private Visibility _userNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _userNameVerifiedTextBlockVisibility = Visibility.Visible;
        private Visibility _fullNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _phoneNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _newPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _verifiedPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _jobTitleAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _cameraButtonVisibility = Visibility.Visible;

        private string _newPassword = "";
        private string _verifiedPassword = "";
        private string _newPasswordAwareTextBlockContent = "";
        private string _userNameAwareTextBlockContent = "";
        private bool _isSaveButtonRunning = false;
        private ImageSource _userImageSource;


        public string UserImageFileName { get; set; }
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
        public ImageSource UserImageSource
        {
            get
            {
                return _userImageSource;
            }
            set
            {
                _userImageSource = value;
            }
        }

        public string UserNameText
        {
            get
            {
                return CurrentModifiedUser.Username;
            }
            set
            {
                CurrentModifiedUser.Username = value;
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
        public string AddressText
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
        public string JobText
        {
            get
            {
                return CurrentModifiedUser.Job;
            }
            set
            {
                CurrentModifiedUser.Job = value;
                JobTitleAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                    Visibility.Visible : Visibility.Collapsed;
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
                    VerifiedPasswordAwareTextBlockVisibility != Visibility.Visible &&
                    JobTitleAwareTextBlockVisibility != Visibility.Visible;
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
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }

        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CancleButtonCommand { get; set; }
        public RunInputCommand CameraButtonCommand { get; set; }

        public EventHandleCommand GridSizeChangedCommand { get; set; }
        public EventHandleCommand NewPasswordChangedCommand { get; set; }
        public EventHandleCommand VerifiedPasswordChangedCommand { get; set; }

        protected override Logger logger => L;

        protected override void OnInitializing()

        {
            SetupFeatures();
            CurrentModifiedUser = MSW_DataFlowHost.Current.CurrentModifiedUser;
            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            CancleButtonCommand = new RunInputCommand(CancleButtonClickEvent);

            UserNameAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedUser.Username) ?
                    Visibility.Visible : Visibility.Collapsed;
            UserNameVerifiedTextBlockVisibility = Visibility.Collapsed;
            FullNameAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedUser.FullName) ?
                 Visibility.Visible : Visibility.Collapsed;
            PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedUser.Phone) ?
                Visibility.Visible : Visibility.Collapsed;
            JobTitleAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentModifiedUser.Job) ?
                Visibility.Visible : Visibility.Collapsed;

            GridSizeChangedCommand = new EventHandleCommand(OnGridSizeChangedEvent);
            NewPasswordChangedCommand = new EventHandleCommand(OnNewPasswordChagedEvent);
            VerifiedPasswordChangedCommand = new EventHandleCommand(OnVerifiedPasswordChagedEvent);
        }

        protected override void OnInitialized()
        {
        }

        private void SetupFeatures()
        {
            CameraButtonVisibility = Visibility.Collapsed;
            bool isUsingCameraButton = RUNE.IS_SUPPORT_ADMIN_CHANGE_USER_IMAGE;
            if (isUsingCameraButton)
            {
                CameraButtonVisibility = Visibility.Visible;
                CameraButtonCommand = new RunInputCommand(CameraButtonClickEvent);
            }
        }

        private void CameraButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CAMERA_BUTTON
                , dataTransfer);
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

        private void OnGridSizeChangedEvent(object sender, EventArgs e, object paramaters)
        {
            Grid ctrl = (Grid)sender;
            Border avaBorder = (Border)((object[])paramaters)[0];

            if (avaBorder.RenderSize.Width >= avaBorder.RenderSize.Height)
            {
                ctrl.Width = avaBorder.RenderSize.Height;
            }
            else
            {
                ctrl.Width = avaBorder.RenderSize.Width;
            }
        }

        private void SaveButtonClickEvent(object paramaters)
        {
            IsSaveButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_SAVE_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
        }

        private void CancleButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CANCLE_BUTTON
                , dataTransfer);
        }


        private void UpdatePasswordAwareTextBlock()
        {
            UpdateNewPasswordAwareTextBlock();
            UpdateVerifiedPasswordAwareTextBlock();

            if (String.IsNullOrEmpty(NewPassword) &&
                String.IsNullOrEmpty(VerifiedPassword))
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
