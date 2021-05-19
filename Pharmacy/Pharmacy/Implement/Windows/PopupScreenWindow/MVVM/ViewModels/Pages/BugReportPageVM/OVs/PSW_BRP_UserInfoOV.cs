using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.BugReportPageVM.OVs
{
    internal class PSW_BRP_UserInfoOV : BaseViewModel
    {
        private string _fullNameText;
        private string _phoneText;
        private string _addressText;
        private string _emailText;


        [Bindable(true)]
        public string FullNameText
        {
            get
            {
                return _fullNameText;
            }
            set
            {
                _fullNameText = value;
                InvalidateOwn();
                Invalidate("FullNameTextFieldAlertVisibility");
            }
        }

        [Bindable(true)]
        public string PhoneText
        {
            get
            {
                return _phoneText;
            }
            set
            {
                _phoneText = value;
                InvalidateOwn();
                Invalidate("PhoneTextFieldAlertVisibility");

            }
        }

        [Bindable(true)]
        public string AddressText
        {
            get
            {
                return _addressText;
            }
            set
            {
                _addressText = value;
                InvalidateOwn();
            }
        }

        [Bindable(true)]
        public string EmailText
        {
            get
            {
                return _emailText;
            }
            set
            {
                _emailText = value;
                InvalidateOwn();
                Invalidate("EmailTextFieldAlertVisibility");
            }
        }

        [Bindable(true)]
        public Visibility EmailTextFieldAlertVisibility
        {
            get
            {
                return String.IsNullOrEmpty(EmailText) ? Visibility.Visible : Visibility.Hidden;
            }

        }

        [Bindable(true)]
        public Visibility PhoneTextFieldAlertVisibility
        {
            get
            {
                return String.IsNullOrEmpty(PhoneText) ? Visibility.Visible : Visibility.Hidden;
            }

        }

        [Bindable(true)]
        public Visibility FullNameTextFieldAlertVisibility
        {
            get
            {
                return String.IsNullOrEmpty(FullNameText) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool IsUserInfoMeetCondition
        {
            get
            {
                return !(string.IsNullOrEmpty(FullNameText)
                    || string.IsNullOrEmpty(PhoneText)
                    || string.IsNullOrEmpty(EmailText));
            }
        }

        public override void RefreshViewModel()
        {
            FullNameText = "";
            PhoneText = "";
            AddressText = "";
            EmailText = "";

            base.RefreshViewModel();
        }
    }
}
