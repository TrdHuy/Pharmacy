using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.AddWarehouseImportPage
{
    public class MSW_WHMP_AWIP_DeleteWarehouseImportDetailButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private AddWarehouseImportPageViewModel _viewModel;
        private DataGrid ctrl;
        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as AddWarehouseImportPageViewModel;
            ctrl = dataTransfer[1] as DataGrid;

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa thuốc này khỏi danh sách?",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                OwnerWindow.MainScreen,
                "Thông báo!");

            if (mesResult == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {
                _viewModel.LstWarehouseImportDetail.RemoveAt(ctrl.SelectedIndex);
                _viewModel.UpdateTotalPriceAndNewPrice();

                return true;
            }

            return false;
        }
    }
}
