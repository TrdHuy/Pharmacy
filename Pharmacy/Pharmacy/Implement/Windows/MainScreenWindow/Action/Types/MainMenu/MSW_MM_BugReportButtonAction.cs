using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.MainMenu
{
    internal class MSW_MM_BugReportButtonAction : BaseCommandExecuter
    {
        public MSW_MM_BugReportButtonAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger)
        {
        }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            App.Current.ShowBugReportWindow();
        }
    }
}
