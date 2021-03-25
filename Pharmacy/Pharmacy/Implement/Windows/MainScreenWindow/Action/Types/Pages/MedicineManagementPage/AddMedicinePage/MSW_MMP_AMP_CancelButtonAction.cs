using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.AddMedicinePage
{
    internal class MSW_MMP_AMP_CancelButtonAction : MSW_MMP_AMP_ButtonAction
    {
        public MSW_MMP_AMP_CancelButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.MEDICINE_MANAGEMENT_PAGE);
        }
    }
}