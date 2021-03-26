using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_PersonalInfoAction : MSW_ButtonAction
    {
        public MSW_PersonalInfoAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }
        public override void ExecuteCommand()
        {
            PageHost.UpdateCurrentPageSource(PageSource.PERSONAL_INFO_PAGE);
        }
    }
}
