using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.BugReportPageVM.OVs
{
    internal class PSW_BRP_IssueInfoOV : BaseViewModel
    {
        [Bindable(true)]
        public string IssueTitleText { get; set; }

        [Bindable(true)]
        public string IssueDetailText { get; set; }

        [Bindable(true)]
        public string IncidentTimeText { get; set; }

        [Bindable(true)]
        public string LogPathText { get; set; }

        [Bindable(true)]
        public string VideoPathText { get; set; }

        public bool IsIssueInfoMeetCondition
        {
            get
            {
                return !(string.IsNullOrEmpty(IssueTitleText)
                    || string.IsNullOrEmpty(IssueDetailText)
                    || string.IsNullOrEmpty(IncidentTimeText)
                    || string.IsNullOrEmpty(LogPathText));
            }
        }
        public override void RefreshViewModel()
        {
            IssueTitleText = "";
            IssueDetailText = "";
            IncidentTimeText = "";
            LogPathText = "";
            VideoPathText = "";

            base.RefreshViewModel();
        }
    }
}
