using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_PersonalInfoAction : MSW_ButtonAction
    {
        public MSW_PersonalInfoAction(ILogger logger) : base(logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.PERSONAL_INFO_PAGE);
        }
    }
}
