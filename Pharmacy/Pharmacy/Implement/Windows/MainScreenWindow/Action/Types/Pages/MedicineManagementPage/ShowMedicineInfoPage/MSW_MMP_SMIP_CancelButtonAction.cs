using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.ShowMedicineInfoPage
{
    internal class MSW_MMP_SMIP_CancelButtonAction : BaseViewModelCommandExecuter
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;

        public MSW_MMP_SMIP_CancelButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            _pageHost.UpdateCurrentPageSource(PageSource.MEDICINE_MANAGEMENT_PAGE);
        }
    }
}