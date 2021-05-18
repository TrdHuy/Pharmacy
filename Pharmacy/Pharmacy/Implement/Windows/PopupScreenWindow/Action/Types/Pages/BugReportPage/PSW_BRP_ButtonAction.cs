using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.BugReportPageVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.Action.Types.Pages.BugReportPage
{
    internal class PSW_BRP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected BugReportPageViewModel BRPViewModel
        {
            get
            {
                return ViewModel as BugReportPageViewModel;
            }
        }
        
        public PSW_BRP_ButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
        }
    }
}
