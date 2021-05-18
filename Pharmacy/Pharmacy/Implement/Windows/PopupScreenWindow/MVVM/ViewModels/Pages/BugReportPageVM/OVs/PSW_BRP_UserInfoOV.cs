using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.BugReportPageVM.OVs
{
    internal class PSW_BRP_UserInfoOV : BaseViewModel
    {
        [Bindable(true)]
        public string FullNameText { get; set; }

        [Bindable(true)]
        public string PhoneText { get; set; }

        [Bindable(true)]
        public string AddressText { get; set; }

        [Bindable(true)]
        public string EmailText { get; set; }

        public bool IsUserInfoMeetCondition
        {
            get
            {
                return !(string.IsNullOrEmpty(FullNameText)
                    || string.IsNullOrEmpty(PhoneText)
                    || string.IsNullOrEmpty(AddressText)
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
