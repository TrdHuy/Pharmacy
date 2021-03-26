using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using Pharmacy.Implement.Windows.BaseWindow.Utils.PageController;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.AddMedicinePage
{
    internal class MSW_MMP_AMP_SaveButtonAction : MSW_MMP_AMP_ButtonAction
    {
        private SQLQueryCustodian _sqlCmdObserver;

        public MSW_MMP_AMP_SaveButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        public override void ExecuteCommand()
        {
            if (!AMPViewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                AMPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
                return;
            }

            tblMedicine medicine = new tblMedicine();
            medicine.MedicineID = AMPViewModel.MedicineID.Trim();
            medicine.MedicineName = AMPViewModel.MedicineName.Trim();
            medicine.MedicineTypeID = AMPViewModel.LstMedicineType[AMPViewModel.MedicineTypeID].MedicineTypeID;
            medicine.MedicineUnitID = AMPViewModel.LstMedicineUnit[AMPViewModel.MedicineUnitID].MedicineUnitID;
            medicine.SupplierID = AMPViewModel.LstSupplier[AMPViewModel.SupplierID].SupplierID;
            medicine.BidPrice = AMPViewModel.BidPrice;
            medicine.AskingPrice = AMPViewModel.AskingPrice;
            medicine.MedicineDescription = AMPViewModel.MedicineDescription;
            medicine.IsActive = true;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.ADD_NEW_MEDICINE_CMD_KEY,
                PharmacyDefinitions.ADD_NEW_MEDICINE_DELAY_TIME,
                _sqlCmdObserver,
                medicine,
                AMPViewModel.MedicineImageFileName);

            return;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Thêm thuốc mới thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi thêm thuốc mới. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Thông báo!");
            }
            AMPViewModel.ButtonCommandOV.IsSaveButtonRunning = false;
            PageHost.UpdateCurrentPageSource(PageSource.MEDICINE_MANAGEMENT_PAGE);
        }
    }
}