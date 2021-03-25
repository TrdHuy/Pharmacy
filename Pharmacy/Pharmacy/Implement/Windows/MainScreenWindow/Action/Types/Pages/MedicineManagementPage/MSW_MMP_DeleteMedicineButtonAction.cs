using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage
{
    internal class MSW_MMP_DeleteMedicineButtonAction : MSW_MMP_ButtonAction
    {
        private DataGrid medicineDataGrid;

        public MSW_MMP_DeleteMedicineButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);

            medicineDataGrid = DataTransfer[1] as DataGrid;

            var mesResult = App.Current.ShowApplicationMessageBox("Bạn có chắc xóa thuốc này?",
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                OwnerWindow.MainScreen,
                "Thông báo!");

            if (mesResult == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultYes)
            {
                SQLQueryCustodian sqlQueryObserver = new SQLQueryCustodian((queryResult) =>
                {
                    if (queryResult.MesResult == MessageQueryResult.Done)
                    {
                        App.Current.ShowApplicationMessageBox("Xóa thuốc thành công!",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                        OwnerWindow.MainScreen,
                        "Thông báo!");

                        MMPViewModel.MedicineItemSource.RemoveAt(medicineDataGrid.SelectedIndex);

                    }
                });

                DbManager.Instance.ExecuteQuery(SQLCommandKey.SET_MEDICINE_DEACTIVE_CMD_KEY,
                    sqlQueryObserver,
                    MMPViewModel.MedicineItemSource[medicineDataGrid.SelectedIndex].MedicineID);

                return ;
            }
            return ;
        }
    }
}
