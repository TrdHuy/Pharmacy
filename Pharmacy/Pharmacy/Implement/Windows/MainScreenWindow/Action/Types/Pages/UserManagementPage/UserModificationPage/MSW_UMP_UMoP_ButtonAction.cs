using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage.UserModification;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserModificationPage
{
    internal class MSW_UMP_UMoP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected UserModificationPageViewModel UMoPViewModel
        {
            get
            {
                return ViewModel as UserModificationPageViewModel;
            }
        }
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_UMP_UMoP_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}

