using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ModifyWarehouseImportPage
{
    public class MSW_WHMP_MWIP_DeleteWarehouseImportDetailButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private ModifyWarehouseImportPageViewModel _viewModel;
        private DataGrid ctrl;
        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as ModifyWarehouseImportPageViewModel;
            ctrl = dataTransfer[1] as DataGrid;

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa thuốc này khỏi danh sách?",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                OwnerWindow.MainScreen,
                "Thông báo!");

            if (mesResult == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {
                App.Current.ShowApplicationMessageBox("Xóa thuốc thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                        OwnerWindow.MainScreen,
                        "Thông báo!");

                var detail = _viewModel.ImportInfo.tblWarehouseImportDetails.Where(o => o.MedicineID == _viewModel.LstWarehouseImportDetail[ctrl.SelectedIndex].MedicineID).FirstOrDefault();
                if (detail != null)
                    detail.IsActive = false;

                _viewModel.LstWarehouseImportDetail.RemoveAt(ctrl.SelectedIndex);
                _viewModel.UpdateTotalPriceAndNewPrice();
                return true;
            }

            return false;
        }
    }
}
