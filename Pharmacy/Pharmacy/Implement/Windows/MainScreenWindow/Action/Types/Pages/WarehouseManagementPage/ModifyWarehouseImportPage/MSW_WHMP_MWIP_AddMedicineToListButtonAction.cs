using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ModifyWarehouseImportPage
{
    public class MSW_WHMP_MWIP_AddMedicineToListButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private ModifyWarehouseImportPageViewModel _viewModel;
        private DataGrid dataGrid;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as ModifyWarehouseImportPageViewModel;
            dataGrid = dataTransfer[1] as DataGrid;

            try
            {
                StringBuilder error = new StringBuilder();
                if (_viewModel.SelectedMedicine == null)
                {
                    error.Append("Chưa chọn thuốc!").AppendLine();
                }
                if (_viewModel.MedicinePrice < 0)
                {
                    error.Append("Giá thuốc không hợp lệ!").AppendLine();
                }
                if (_viewModel.MedicineQuantity <= 0)
                {
                    error.Append("Số lượng thuốc không hợp lệ!").AppendLine();
                }

                if (error.Length > 0)
                {
                    throw new Exception(error.ToString());
                }

                MSW_WHMP_WarehouseImportDetailOV item;
                if ((item = _viewModel.LstWarehouseImportDetail.Where(o => o.MedicineID == _viewModel.SelectedMedicine.MedicineID).FirstOrDefault()) == null)
                {
                    item = new MSW_WHMP_WarehouseImportDetailOV();
                    item.MedicineID = _viewModel.SelectedMedicine.MedicineID;
                    item.MedicineName = _viewModel.SelectedMedicine.MedicineName;
                    item.MedicineUnitName = _viewModel.SelectedMedicine.tblMedicineUnit.MedicineUnitName;
                    item.Quantity = _viewModel.MedicineQuantity;
                    item.UnitPrice = _viewModel.MedicinePrice;
                    item.TotalPrice = (decimal)item.Quantity * item.UnitPrice;

                    _viewModel.LstWarehouseImportDetail.Add(item);
                }
                else
                {
                    item.Quantity += _viewModel.MedicineQuantity;
                    item.TotalPrice = (decimal)item.Quantity * item.UnitPrice;
                    dataGrid.Items.Refresh();
                }

                var detail = _viewModel.ImportInfo.tblWarehouseImportDetails.Where(o => o.MedicineID == _viewModel.SelectedMedicine.MedicineID).FirstOrDefault();
                if (detail != null)
                    detail.IsActive = true;

                _viewModel.UpdateTotalPriceAndNewPrice();
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Lỗi!");
                return false;
            }
            finally
            {
                _viewModel.IsAddImportDetailButtonRunning = false;
            }
            return true;
        }
    }
}