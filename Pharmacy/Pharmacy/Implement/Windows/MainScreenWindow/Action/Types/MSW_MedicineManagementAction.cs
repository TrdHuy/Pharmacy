using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types
{
    internal class MSW_MedicineManagementAction : MSW_ButtonAction
    {
        public MSW_MedicineManagementAction(ILogger logger) : base(logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.MEDICINE_MANAGEMENT_PAGE);
        }
    }
}
