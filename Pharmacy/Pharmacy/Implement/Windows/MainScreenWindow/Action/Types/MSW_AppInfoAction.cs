using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_AppInfoAction : MSW_NavigationButtonAction
    {
        public MSW_AppInfoAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }
        protected override void ExecuteCommand()
        {
            PageHost.UpdateCurrentPageSource(PageSource.APP_INFO_PAGE);
        }
    }
}
