using System.Windows.Controls;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage
{
    internal class MSW_SP_RemoveOrderDetailAction : MSW_SP_ButtonAction
    {
        public MSW_SP_RemoveOrderDetailAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        public override void ExecuteCommand()
        {
            base.ExecuteCommand();
            DataGrid ctrl = DataTransfer[1] as DataGrid;

            SPViewModel.CustomerOrderDetailItemSource.RemoveAt(ctrl.SelectedIndex);
            return;
        }
    }
}
