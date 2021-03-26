using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Windows.Controls;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage
{
    internal class MSW_UMP_EditButtonAction : MSW_UMP_ButtonAction
    {
        public MSW_UMP_EditButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            DataGrid ctrl = DataTransfer[1] as DataGrid;

            MSW_DataFlowHost.Current.CurrentModifiedUser = UMPViewModel.UserItemSource[ctrl.SelectedIndex];
            PageHost.UpdateCurrentPageSource(PageSource.USER_MODIFICATION_PAGE);
        }
    }
}
