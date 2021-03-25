using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_SettingAction : MSW_ButtonAction
    {
        public MSW_SettingAction(ILogger logger) : base(logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.SETTING_PAGE);
        }
    }
}
