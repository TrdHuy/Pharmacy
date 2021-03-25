using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.ModifyMedicinePage
{
    internal class MSW_MMP_MMP_SaveButtonAction : MSW_MMP_MMP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;

        public MSW_MMP_MMP_SaveButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            if (!MMPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                MMPViewModel.IsSaveButtonRunning = false;
                return;
            }

            tblMedicine medicine = new tblMedicine();
            medicine.MedicineID = MMPViewModel.MedicineID.Trim();
            medicine.MedicineName = MMPViewModel.MedicineName.Trim();
            medicine.MedicineTypeID = MMPViewModel.LstMedicineType[MMPViewModel.MedicineTypeID].MedicineTypeID;
            medicine.MedicineUnitID = MMPViewModel.LstMedicineUnit[MMPViewModel.MedicineUnitID].MedicineUnitID;
            medicine.SupplierID = MMPViewModel.LstSupplier[MMPViewModel.SupplierID].SupplierID;
            medicine.BidPrice = MMPViewModel.BidPrice;
            medicine.AskingPrice = MMPViewModel.AskingPrice;
            medicine.MedicineDescription = MMPViewModel.MedicineDescription;
            medicine.IsActive = true;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.MODIFY_MEDICINE_CMD_KEY,
                PharmacyDefinitions.MODIFY_MEDICINE_DELAY_TIME,
                _sqlCmdObserver,
                medicine,
                MMPViewModel.MedicineImageFileName);

            return;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Chỉnh sửa thông tin thuốc thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi chỉnh sửa thông tin thuốc. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            MMPViewModel.IsSaveButtonRunning = false;
            PageHost.UpdateCurrentPageSource(PageSource.MEDICINE_MANAGEMENT_PAGE);
        }
    }
}