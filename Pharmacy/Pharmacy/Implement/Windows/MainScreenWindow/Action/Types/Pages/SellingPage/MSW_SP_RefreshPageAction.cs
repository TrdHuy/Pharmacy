using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage
{
    public class MSW_SP_RefreshPageAction : Base.UIEventHandler.Action.IAction
    {
        private SellingPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as SellingPageViewModel;

            bool refreshCustomer = true;
            bool refreshBill = true;
            _viewModel.RefreshViewModel(refreshCustomer,refreshBill);
            return true;
        }
    }
}
