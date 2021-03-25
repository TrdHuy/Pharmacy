using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.PersonalInfoPage
{
    internal class MSW_PIP_CancleButtonAction : MSW_PIP_ButtonAction
    {
        public MSW_PIP_CancleButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            DbManager.Instance.RollBack();
            PageHost.UpdateCurrentPageSource(PageSource.HOME_PAGE); 
        }
    }
}
