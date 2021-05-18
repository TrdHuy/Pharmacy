using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.Action.Types.Pages.BugReportPage
{
    internal class PSW_BRP_SendReportAction : PSW_BRP_ButtonAction
    {
        public PSW_BRP_SendReportAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
        }
    }
}
