using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_AppInfoAction : MSW_ButtonAction
    {
        public MSW_AppInfoAction(ILogger logger) : base(logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.APP_INFO_PAGE);
        }
    }
}
