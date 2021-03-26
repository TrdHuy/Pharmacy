using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_MedicineManagementAction : MSW_NavigationButtonAction
    {
        public MSW_MedicineManagementAction(string actionID, string builderID, ILogger logger) : base(actionID, builderID, logger) { }
       
        protected override void ExecuteCommand()
        {
            PageHost.UpdateCurrentPageSource(PageSource.MEDICINE_MANAGEMENT_PAGE);
        }
    }
}
