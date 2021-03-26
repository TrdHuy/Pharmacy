using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs;
using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.AddWarehouseImportPage
{
    internal class MSW_WHMP_AWIP_AddMedicineToListButtonAction : MSW_WHMP_AWIP_ButtonAction
    {
        private DataGrid dataGrid;

        public MSW_WHMP_AWIP_AddMedicineToListButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            dataGrid = DataTransfer[1] as DataGrid;

            try
            {
                StringBuilder error = new StringBuilder();
                if (AWIPViewModel.SelectedMedicine == null)
                {
                    error.Append("Chưa chọn thuốc!").AppendLine();
                }
                if (AWIPViewModel.MedicinePrice < 0)
                {
                    error.Append("Giá thuốc không hợp lệ!").AppendLine();
                }
                if (AWIPViewModel.MedicineQuantity <= 0)
                {
                    error.Append("Số lượng thuốc không hợp lệ!").AppendLine();
                }

                if (error.Length > 0)
                {
                    throw new Exception(error.ToString());
                }

                MSW_WHMP_WarehouseImportDetailOV item;
                if ((item = AWIPViewModel.LstWarehouseImportDetail.Where(o => o.MedicineID == AWIPViewModel.SelectedMedicine.MedicineID).FirstOrDefault()) == null)
                {
                    item = new MSW_WHMP_WarehouseImportDetailOV();
                    item.MedicineID = AWIPViewModel.SelectedMedicine.MedicineID;
                    item.MedicineName = AWIPViewModel.SelectedMedicine.MedicineName;
                    item.MedicineUnitName = AWIPViewModel.SelectedMedicine.tblMedicineUnit.MedicineUnitName;
                    item.Quantity = AWIPViewModel.MedicineQuantity;
                    item.UnitPrice = AWIPViewModel.MedicinePrice;
                    item.TotalPrice = (decimal)item.Quantity * item.UnitPrice;

                    AWIPViewModel.LstWarehouseImportDetail.Add(item);
                }
                else
                {
                    item.Quantity += AWIPViewModel.MedicineQuantity;
                    item.TotalPrice = (decimal)item.Quantity * item.UnitPrice;
                    dataGrid.Items.Refresh();
                }

                AWIPViewModel.UpdateTotalPriceAndNewPrice();
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Lỗi!");
                return;
            }
            finally
            {
                AWIPViewModel.ButtonCommandOV.IsAddImportDetailButtonRunning = false;
            }
            return;
        }
    }
}