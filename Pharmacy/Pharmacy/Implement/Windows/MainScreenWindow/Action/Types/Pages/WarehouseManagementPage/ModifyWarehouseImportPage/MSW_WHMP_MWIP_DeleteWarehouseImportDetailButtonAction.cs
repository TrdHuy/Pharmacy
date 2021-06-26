using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Linq;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ModifyWarehouseImportPage
{
    internal class MSW_WHMP_MWIP_DeleteWarehouseImportDetailButtonAction : MSW_WHMP_MWIP_ButtonAction
    {
        private DataGrid ctrl;
        public MSW_WHMP_MWIP_DeleteWarehouseImportDetailButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            ctrl = DataTransfer[0] as DataGrid;

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa thuốc này khỏi danh sách?",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                OwnerWindow.MainScreen,
                "Thông báo");

            if (mesResult == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {
                App.Current.ShowApplicationMessageBox("Xóa thuốc thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                        OwnerWindow.MainScreen,
                        "Thông báo");

                var detail = MWIPViewModel.ImportInfo.tblWarehouseImportDetails.Where(o => o.MedicineID == MWIPViewModel.LstWarehouseImportDetail[ctrl.SelectedIndex].MedicineID).FirstOrDefault();
                if (detail != null)
                    detail.IsActive = false;

                MWIPViewModel.LstWarehouseImportDetail.RemoveAt(ctrl.SelectedIndex);
                MWIPViewModel.UpdateTotalPriceAndNewPrice();
                return;
            }

            return;
        }
    }
}
