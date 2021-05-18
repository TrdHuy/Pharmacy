using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.AppInfoPage
{
    internal class MSW_AIP_BugReportButtonAction : MSW_AIP_ButtonAction
    {
        public MSW_AIP_BugReportButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger)
        {
        }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            App.Current.ShowBugReportWindow();
        }
    }
}
