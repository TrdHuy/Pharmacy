using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.BugReportPageVM.OVs
{
    internal class PSW_BRP_IssueInfoOV : BaseViewModel
    {
        private string _issueTitleText;
        private string _issueDetailText;
        private string _incidentTimeText;
        private string _logPathText;
        private string _videoPathText;


        [Bindable(true)]
        public string IssueTitleText
        {
            get
            {
                return _issueTitleText;
            }
            set
            {
                _issueTitleText = value;
                InvalidateOwn();
                Invalidate("IssueTitleTextFieldAlertVisibility");
            }
        }

        [Bindable(true)]
        public string IssueDetailText
        {
            get
            {
                return _issueDetailText;
            }
            set
            {
                _issueDetailText = value;
                InvalidateOwn();
                Invalidate("IssueDetailTextFieldAlertVisibility");

            }
        }

        [Bindable(true)]
        public string IncidentTimeText
        {
            get
            {
                return _incidentTimeText;
            }
            set
            {
                _incidentTimeText = value;
                InvalidateOwn();
                Invalidate("IncidentTimeTextFieldAlertVisibility");
            }
        }

        [Bindable(true)]
        public string LogPathText
        {
            get
            {
                return _logPathText;
            }
            set
            {
                _logPathText = value;
                InvalidateOwn();
                Invalidate("LogPathTextFieldAlertVisibility");
            }
        }

        [Bindable(true)]
        public string VideoPathText
        {
            get
            {
                return _videoPathText;
            }
            set
            {
                _videoPathText = value;
                InvalidateOwn();
            }
        }

        [Bindable(true)]
        public Visibility LogPathTextFieldAlertVisibility
        {
            get
            {
                return String.IsNullOrEmpty(LogPathText) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        [Bindable(true)]
        public Visibility IncidentTimeTextFieldAlertVisibility
        {
            get
            {
                return String.IsNullOrEmpty(IncidentTimeText) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        [Bindable(true)]
        public Visibility IssueDetailTextFieldAlertVisibility
        {
            get
            {
                return String.IsNullOrEmpty(IssueDetailText) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        [Bindable(true)]
        public Visibility IssueTitleTextFieldAlertVisibility
        {
            get
            {
                return String.IsNullOrEmpty(IssueTitleText) ? Visibility.Visible : Visibility.Hidden;
            }
        }

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
