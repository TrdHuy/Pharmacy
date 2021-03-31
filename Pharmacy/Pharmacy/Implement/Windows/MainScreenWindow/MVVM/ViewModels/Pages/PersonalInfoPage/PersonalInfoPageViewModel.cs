using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Pharmacy.Implement.Utils;
using Pharmacy.Config;
using static HPSolutionCCDevPackage.netFramework.AtumImageView;
using Pharmacy.Implement.Utils.Extensions.Entities;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.PersonalInfoPage.OVs;
using System.ComponentModel;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages
{
    internal class PersonalInfoPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("PersonalInfoPageViewModel");
        
        private Visibility _fullNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _phoneNameAwareTextBlockVisibility = Visibility.Visible;
        private Visibility _currentPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _newPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private Visibility _verifiedPasswordAwareTextBlockVisibility = Visibility.Collapsed;
        private string _currentInputPassword = "";
        private string _newPassword = "";
        private string _verifiedPassword = "";
        private string _newPasswordAwareTextBlockContent = "";
        private ImageSource _userAvatarSource = null;

        public tblUser CurrentUser { get { return App.Current.CurrentUser; } }
        public string UserImageFileName { get; set; }

        public bool IsSupportChangePersonalAvatarLocation { get { return RUNE.IS_SUPPORT_CHANGE_PERSONAL_AVATAR_LOCATION; } }
        public bool IsSupportChangePersonalAvatarZoom { get { return RUNE.IS_SUPPORT_CHANGE_PERSONAL_AVATAR_ZOOM; } }
        public bool IsSupportLocatorPersonalAvatarWindow { get { return RUNE.IS_SUPPORT_LOCATOR_WINDOW_FOR_PERSONAL_AVATAR; } }

        [Bindable(true)]
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

        [Bindable(true)]
        public AtumUserData AvatarInfoData 
        {
            get
            {
                return CurrentUser.GetUserData().PersonalAvatarInfo;
            }
            set
            {
                CurrentUser.GetUserData().PersonalAvatarInfo = value;
                InvalidateOwn();
            }
        }

        [Bindable(true)]
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

        [Bindable(true)]
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

        [Bindable(true)]
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

        [Bindable(true)]
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

        [Bindable(true)]
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

        [Bindable(true)]
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

        [Bindable(true)]
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

        [Bindable(true)]
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

        [Bindable(true)]
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

        [Bindable(true)]
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

        [Bindable(true)]
        public string CurrentPassword
        {
            get { return _currentInputPassword; }
            set
            {
                _currentInputPassword = value;
                InvalidateOwn();
            }
        }

        [Bindable(true)]
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                InvalidateOwn();
            }
        }

        [Bindable(true)]
        public string VerifiedPassword
        {
            get { return _verifiedPassword; }
            set
            {
                _verifiedPassword = value;
                InvalidateOwn();
            }
        }

        [Bindable(true)]
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

        public EventCommandModel GridSizeChangedCommand;
        public EventCommandModel CurrentPasswordChangedCommand;
        public EventCommandModel NewPasswordChangedCommand;
        public EventCommandModel VerifiedPasswordChangedCommand;
        public MSW_PIP_ButtonCommandOV ButtonCommandOV{ get; set; }

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            ButtonCommandOV = new MSW_PIP_ButtonCommandOV(this);

            GridSizeChangedCommand = new EventCommandModel(OnGridSizeChangedEvent);
            CurrentPasswordChangedCommand = new EventCommandModel(OnCurrentPasswordChagedEvent);
            NewPasswordChangedCommand = new EventCommandModel(OnNewPasswordChagedEvent);
            VerifiedPasswordChangedCommand = new EventCommandModel(OnVerifiedPasswordChagedEvent);
        }

        protected override void OnInitialized()
        {
        }


        #region Button, event field

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

        public override void OnLoaded()
        {
            base.OnLoaded();
            UserImageSource = FileIOUtil.
                 GetBitmapFromName(CurrentUser.Username, FileIOUtil.USER_IMAGE_FOLDER_NAME).
                 ToImageSource();
            RefreshViewModel();
        }
    }
}
