using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Observable.ObserverPattern;
using Pharmacy.Base.UIEventHandler.Listener;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.Attributes;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Pharmacy.Implement.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages
{
    public class PersonalInfoPageViewModel : AbstractViewModel
    {
        private IActionListener _keyActionListener = KeyActionListener.Instance;

        private Visibility _fullNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _phoneNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _currentPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _newPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _verifiedPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private string _newPassword = "";
        private string _verifiedPassword = "";
        private string _newPasswordAwareTextBlockContent = "";
        private bool _isSaveButtonRunning = false;
        private ImageSource _userAvatarSource = null;

        public tblUser CurrentUser { get { return App.Current.CurrentUser; } }
        public string UserImageFileName { get; set; }

        public ImageSource UserImageSource
        {
            get
            {
                return _userAvatarSource;
            }
            set
            {
                _userAvatarSource = value;
                InvalidateOwn();
            }
        }

        public string FullNameText
        {
            get
            {
                FullNameAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentUser.FullName) ?
                  Visibility.Visible : Visibility.Collapsed;
                return CurrentUser.FullName;
            }
            set
            {
                CurrentUser.FullName = value;
                FullNameAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                    Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string PhoneText
        {
            get
            {
                PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(CurrentUser.Phone) ?
                 Visibility.Visible : Visibility.Collapsed;
                return CurrentUser.Phone;
            }
            set
            {
                CurrentUser.Phone = value;
                PhoneAwareTextBlockVisibility = String.IsNullOrEmpty(value) ?
                    Visibility.Visible : Visibility.Collapsed;
                InvalidateOwn();
            }
        }
        public string AdressText
        {
            get
            {
                return CurrentUser.Address;
            }
            set
            {
                CurrentUser.Address = value;
                InvalidateOwn();
            }
        }
        public string EmailText
        {
            get
            {
                return CurrentUser.Email;
            }
            set
            {
                CurrentUser.Email = value;
                InvalidateOwn();
            }
        }
        public string LinkText
        {
            get
            {
                return CurrentUser.Link;
            }
            set
            {
                CurrentUser.Link = value;
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
        public Visibility CurrentPasswordAwareTextBlockVisibility
        {
            get
            {
                return _currentPasswordAwareTextBlockVisibility;
            }
            set
            {
                _currentPasswordAwareTextBlockVisibility = value;
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
        public string CurrentPassword
        {
            get { return CurrentUser.Password; }
            set
            {
                CurrentUser.Password = value;
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
                    CurrentPasswordAwareTextBlockVisibility != Visibility.Visible &&
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

        public EventHandleCommand GridSizeChangedCommand;
        public EventHandleCommand CurrentPasswordChangedCommand;
        public EventHandleCommand NewPasswordChangedCommand;
        public EventHandleCommand VerifiedPasswordChangedCommand;
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CancleButtonCommand { get; set; }
        public RunInputCommand CameraButtonCommand { get; set; }

        protected override void InitPropertiesRegistry()
        {
            PropRegister("CurrentUser");
        }

        public PersonalInfoPageViewModel()
        {
            UserImageSource = FileIOUtil.
                GetBitmapFromName(CurrentUser.Username, FileIOUtil.USER_IMAGE_FOLDER_NAME).
                ToImageSource();

            SaveButtonCommand = new RunInputCommand(OnSaveButtonClickEvent);
            CameraButtonCommand = new RunInputCommand(OnCameraButtonClickEvent);
            CancleButtonCommand = new RunInputCommand(OnCancleButtonClickEvent);

            GridSizeChangedCommand = new EventHandleCommand(OnGridSizeChangedEvent);
            CurrentPasswordChangedCommand = new EventHandleCommand(OnCurrentPasswordChagedEvent);
            NewPasswordChangedCommand = new EventHandleCommand(OnNewPasswordChagedEvent);
            VerifiedPasswordChangedCommand = new EventHandleCommand(OnVerifiedPasswordChagedEvent);
        }

        #region Button, event field

        private void OnCancleButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_PIP_CANCLE_BUTTON
                , dataTransfer);
        }

        private void OnCameraButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_PIP_CAMERA_BUTTON
                , dataTransfer);
        }

        private void OnSaveButtonClickEvent(object paramaters)
        {
            IsSaveButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = this;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_PIP_SAVE_BUTTON
                , dataTransfer);
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

        private void OnCurrentPasswordChagedEvent(object sender, EventArgs e, object paramater)
        {
            PasswordBox ctrl = (PasswordBox)sender;
            CurrentPassword = ctrl.Password;
            UpdatePasswordAwareTextBlock();
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
        #endregion

        private void UpdatePasswordAwareTextBlock()
        {
            UpdateCurrentPasswordAwareTextBlock();
            UpdateNewPasswordAwareTextBlock();
            UpdateVerifiedPasswordAwareTextBlock();

            if (String.IsNullOrEmpty(CurrentPassword) &&
                String.IsNullOrEmpty(NewPassword) &&
                String.IsNullOrEmpty(VerifiedPassword))
            {
                CurrentPasswordAwareTextBlockVisibility = Visibility.Collapsed;
                NewPasswordAwareTextBlockVisibility = Visibility.Collapsed;
                VerifiedPasswordAwareTextBlockVisibility = Visibility.Collapsed;
            }
        }

        private void UpdateCurrentPasswordAwareTextBlock()
        {
            CurrentPasswordAwareTextBlockVisibility = CurrentPassword.Equals(CurrentUser.Password) ?
               Visibility.Collapsed : Visibility.Visible;
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
            else if (NewPassword.Equals(CurrentUser.Password))
            {
                NewPasswordAwareTextBlockVisibility = Visibility.Visible;
                mes = NewPasswordAwareMessage.SameBefore;
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
