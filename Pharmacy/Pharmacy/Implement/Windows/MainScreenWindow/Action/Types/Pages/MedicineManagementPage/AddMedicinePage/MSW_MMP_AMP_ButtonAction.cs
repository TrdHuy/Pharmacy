using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.AddMedicinePage
{
    internal class MSW_MMP_AMP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected AddMedicinePageViewModel AMPViewModel
        {
            get
            {
                return ViewModel as AddMedicinePageViewModel;
            }
        }
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_MMP_AMP_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}
