using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.ShowWarehouseImportInfo;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ShowWarehouseImportInfoPage
{
    internal class MSW_WHMP_SWIP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected ShowWarehouseImportInfoPageViewModel SWIPViewModel
        {
            get
            {
                return ViewModel as ShowWarehouseImportInfoPageViewModel;
            }
        }
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_WHMP_SWIP_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}
