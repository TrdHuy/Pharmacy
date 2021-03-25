using System.Windows.Controls;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage
{
    internal class MSW_SP_RemoveOrderDetailAction : MSW_SP_ButtonAction
    {
        public MSW_SP_RemoveOrderDetailAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }
        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);
            DataGrid ctrl = DataTransfer[1] as DataGrid;

            SPViewModel.CustomerOrderDetailItemSource.RemoveAt(ctrl.SelectedIndex);
            return;
        }
    }
}
