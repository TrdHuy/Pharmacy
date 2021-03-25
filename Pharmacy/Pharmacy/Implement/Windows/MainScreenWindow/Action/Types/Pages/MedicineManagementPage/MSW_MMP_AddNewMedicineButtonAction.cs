using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage
{
    internal class MSW_MMP_AddNewMedicineButtonAction : MSW_MMP_ButtonAction
    { 
        public MSW_MMP_AddNewMedicineButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            PageHost.UpdateCurrentPageSource(PageSource.ADD_MEDICINE_PAGE);
        }
    }
}