using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage.UserInstantiation;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserInstantiationPage
{
    internal class MSW_UMP_UIP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected UserInstantiationPageViewModel UIPViewModel
        {
            get
            {
                return ViewModel as UserInstantiationPageViewModel;
            }
        }
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_UMP_UIP_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}
