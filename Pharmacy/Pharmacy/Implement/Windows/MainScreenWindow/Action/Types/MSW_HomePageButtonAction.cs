using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{

    internal class MSW_HomePageButtonAction : MSW_ButtonAction
    {
        public MSW_HomePageButtonAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }
        public override void ExecuteCommand()
        {
            PageHost.UpdateCurrentPageSource(PageSource.HOME_PAGE);
        }
    }
}
