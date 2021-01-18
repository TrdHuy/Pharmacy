using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage
{
    public class MSW_SP_RemoveOrderDetailAction : Base.UIEventHandler.Action.IAction
    {
        private SellingPageViewModel _viewModel;
        private DataGrid ctrl;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as SellingPageViewModel;
            ctrl = dataTransfer[1] as DataGrid;

            _viewModel.CustomerOrderDetailItemSource.RemoveAt(ctrl.SelectedIndex);
            return true;
        }
    }
}
