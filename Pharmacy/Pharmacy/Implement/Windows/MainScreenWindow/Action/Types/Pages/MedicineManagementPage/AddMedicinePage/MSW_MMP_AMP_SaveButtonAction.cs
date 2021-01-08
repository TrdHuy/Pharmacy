using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.AddMedicinePage
{
    public class MSW_MMP_AMP_SaveButtonAction : Base.UIEventHandler.Action.IAction
    {
        private SQLQueryCustodian _sqlCmdObserver;
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private AddMedicinePageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as AddMedicinePageViewModel;
            if (!_viewModel.IsSaveButtonCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường bị sai trên!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                _viewModel.IsSaveButtonRunning = false;
                return false;
            }

            tblMedicine medicine = new tblMedicine();
            medicine.MedicineID = _viewModel.MedicineID.Trim();
            medicine.MedicineName = _viewModel.MedicineName.Trim();
            medicine.MedicineTypeID = _viewModel.LstMedicineType[_viewModel.MedicineTypeID].MedicineTypeID;
            medicine.MedicineUnitID = _viewModel.LstMedicineUnit[_viewModel.MedicineUnitID].MedicineUnitID;
            medicine.SupplierID = _viewModel.LstSupplier[_viewModel.SupplierID].SupplierID;
            medicine.BidPrice = decimal.Parse(_viewModel.BidPrice.Trim());
            medicine.AskingPrice = decimal.Parse(_viewModel.AskingPrice.Trim());
            medicine.MedicineDescription = _viewModel.MedicineDescription;
            medicine.IsActive = true;

            _sqlCmdObserver = new SQLQueryCustodian(SQLQueryCallback);
            DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.ADD_NEW_MEDICINE_CMD_KEY,
                PharmacyDefinitions.ADD_NEW_MEDICINE_DELAY_TIME,
                _sqlCmdObserver,
                medicine);

            return true;
        }
        private void SQLQueryCallback(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                App.Current.ShowApplicationMessageBox("Thêm thuốc mới thành công",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
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
            _viewModel.IsSaveButtonRunning = false;
            _pageHost.UpdateCurrentPageSource(PageSource.MedicineManagementPage);
        }
    }
}