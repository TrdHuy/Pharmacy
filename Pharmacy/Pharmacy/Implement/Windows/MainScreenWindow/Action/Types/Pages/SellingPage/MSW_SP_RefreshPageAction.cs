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

            if(_viewModel.CustomerOrderDetailItemSource.Count > 0)
            {
                var x = App.Current.ShowApplicationMessageBox("Bạn có chắc tạo mới!",
                 HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                 HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                 OwnerWindow.MainScreen,
                 "Thông báo!!");
                if(x == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultNo)
                {
                    return false;
                }
            }
            
            bool refreshCustomer = true;
            bool refreshBill = true;
            _viewModel.RefreshViewModel(refreshCustomer,refreshBill);

            return true;
        }
    }
}
