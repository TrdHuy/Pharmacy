using Pharmacy.Base.MVVM.ViewModels;
using System.Windows.Controls;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.AddWarehouseImportPage
{
    internal class MSW_WHMP_AWIP_DeleteWarehouseImportDetailButtonAction : MSW_WHMP_AWIP_ButtonAction
    {
        private DataGrid warehouseDataGrid;

        public MSW_WHMP_AWIP_DeleteWarehouseImportDetailButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            warehouseDataGrid = DataTransfer[1] as DataGrid;

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa thuốc này khỏi danh sách?",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                OwnerWindow.MainScreen,
                "Thông báo!");

            if (mesResult == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {
                AWIPViewModel.LstWarehouseImportDetail.RemoveAt(warehouseDataGrid.SelectedIndex);
                AWIPViewModel.UpdateTotalPriceAndNewPrice();

                return;
            }

            return;
        }
    }
}
