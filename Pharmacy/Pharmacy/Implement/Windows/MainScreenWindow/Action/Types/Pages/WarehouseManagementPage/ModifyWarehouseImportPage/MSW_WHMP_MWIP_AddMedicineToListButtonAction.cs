using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage.OVs;
using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.WarehouseManagementPage.ModifyWarehouseImportPage
{
    internal class MSW_WHMP_MWIP_AddMedicineToListButtonAction : MSW_WHMP_MWIP_ButtonAction
    {
        private DataGrid dataGrid;

        public MSW_WHMP_MWIP_AddMedicineToListButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            dataGrid = DataTransfer[0] as DataGrid;

            try
            {
                StringBuilder error = new StringBuilder();
                if (MWIPViewModel.SelectedMedicine == null)
                {
                    error.Append("Chưa chọn thuốc!").AppendLine();
                }
                if (MWIPViewModel.MedicinePrice < 0)
                {
                    error.Append("Giá thuốc không hợp lệ!").AppendLine();
                }
                if (MWIPViewModel.MedicineQuantity <= 0)
                {
                    error.Append("Số lượng thuốc không hợp lệ!").AppendLine();
                }

                if (error.Length > 0)
                {
                    throw new Exception(error.ToString());
                }

                MSW_WHMP_WarehouseImportDetailOV item;
                if ((item = MWIPViewModel.LstWarehouseImportDetail.Where(o => o.MedicineID == MWIPViewModel.SelectedMedicine.MedicineID).FirstOrDefault()) == null)
                {
                    item = new MSW_WHMP_WarehouseImportDetailOV();
                    item.MedicineID = MWIPViewModel.SelectedMedicine.MedicineID;
                    item.MedicineName = MWIPViewModel.SelectedMedicine.MedicineName;
                    item.MedicineUnitName = MWIPViewModel.SelectedMedicine.tblMedicineUnit.MedicineUnitName;
                    item.Quantity = MWIPViewModel.MedicineQuantity;
                    item.UnitPrice = MWIPViewModel.MedicinePrice;
                    item.TotalPrice = (decimal)item.Quantity * item.UnitPrice;

                    MWIPViewModel.LstWarehouseImportDetail.Add(item);
                }
                else
                {
                    item.UnitPrice = MWIPViewModel.MedicinePrice;
                    item.Quantity += MWIPViewModel.MedicineQuantity;
                    item.TotalPrice = (decimal)item.Quantity * item.UnitPrice;
                    dataGrid.Items.Refresh();
                }

                var detail = MWIPViewModel.ImportInfo.tblWarehouseImportDetails.Where(o => o.MedicineID == MWIPViewModel.SelectedMedicine.MedicineID).FirstOrDefault();
                if (detail != null)
                    detail.IsActive = true;

                MWIPViewModel.UpdateTotalPriceAndNewPrice();
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
                MWIPViewModel.ButtonCommandOV.IsAddImportDetailButtonRunning = false;
            }
            return;
        }
    }
}