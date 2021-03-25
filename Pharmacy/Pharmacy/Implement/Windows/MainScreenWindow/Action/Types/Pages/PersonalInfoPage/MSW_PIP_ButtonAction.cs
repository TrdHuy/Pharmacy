using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.PersonalInfoPage
{
    internal class MSW_PIP_ButtonAction : BaseViewModelCommandExecuter
    {
        protected PersonalInfoPageViewModel PIPViewModel
        {
            get
            {
                return ViewModel as PersonalInfoPageViewModel;
            }
        }
        protected MSW_PageController PageHost { get; } = MSW_PageController.Instance;

        public MSW_PIP_ButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
        }
    }
}
