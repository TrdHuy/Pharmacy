using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using System.Linq;
using System;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils.DatabaseManager;
using System.Collections.Generic;
using HPSolutionCCDevPackage.netFramework;
using System.Windows.Controls;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    internal class MSW_CMP_CTP_CBP_AddOrderDetailButtonAction : MSW_CMP_CTP_CBP_ButtonAction
    {
        private SQLQueryCustodian _queryObserver;
        private DataGrid orderDetaiDataGrid;

        public MSW_CMP_CTP_CBP_AddOrderDetailButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            orderDetaiDataGrid = DataTransfer[0] as DataGrid;

            if (!CBPViewModel.IsAddOrderDetailCanPerform)
            {
                if (String.IsNullOrEmpty(CBPViewModel.MedicineOV.Quantity) || CBPViewModel.MedicineOV.Quantity.Equals("0"))
                {
                    App.Current.ShowApplicationMessageBox("Kiểm tra lại số lượng!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Cảnh báo!");
                    CBPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                    return;
                }
                else
                {
                    App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường đang bỏ trống!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Cảnh báo!");
                    CBPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                }

                return;
            }

            CreateNewOrderDetail();

            return;
        }

        private void CreateNewOrderDetail()
        {
            try
            {
                OrderDetailOV checkExistedVO = null;
                try
                {
                    if (CBPViewModel.CurrentOrderDetails.Count > 0)
                    {
                        checkExistedVO = CBPViewModel.CurrentOrderDetails.First(VO =>
                        VO.MedicineID.Equals(CBPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineID));
                    }
                }
                catch (Exception e)
                {

                }

                if (checkExistedVO != null)
                {
                    checkExistedVO.QuantityToString = (checkExistedVO.Quantity
                        + Convert.ToDouble(CBPViewModel.MedicineOV.Quantity)).ToString();
                    orderDetaiDataGrid.Items.Refresh();
                    CBPViewModel.Invalidate(CBPViewModel.MedicineOV, "MedicineCost");
                    CBPViewModel.Invalidate(CBPViewModel.MedicineOV, "TotalCost");
                    CBPViewModel.Invalidate(CBPViewModel.MedicineOV, "RestAmount");

                    ClearAddedMedicineInfo();
                }
                else
                {
                    MSW_CMP_CTP_CBP_OderDetailOV orderDetailVO = new MSW_CMP_CTP_CBP_OderDetailOV(CBPViewModel);
                    orderDetailVO.Medicine = CBPViewModel.MedicineOV.CurrentSelectedMedicine;
                    orderDetailVO.Order = CBPViewModel.CurrentCustomerOrder;
                    orderDetailVO.MedicineName = CBPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineName;
                    orderDetailVO.MedicineID = CBPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineID;
                    orderDetailVO.MedicineUnitName = CBPViewModel.MedicineOV.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName;
                    orderDetailVO.QuantityToString = CBPViewModel.MedicineOV.Quantity;
                    orderDetailVO.UnitPrice = CBPViewModel.MedicineOV.CurrentSelectedMedicine.AskingPrice;
                    orderDetailVO.UnitBidPrice = CBPViewModel.MedicineOV.CurrentSelectedMedicine.BidPrice;
                    GetPromo(orderDetailVO);

                    if (orderDetailVO.Quantity > 0)
                    {
                        CBPViewModel.CurrentOrderDetails.Add(orderDetailVO);
                        ClearAddedMedicineInfo();
                    }
                }
                CBPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
            }
            catch (Exception e)
            {

            }
        }

        private void ClearAddedMedicineInfo()
        {
            CBPViewModel.MedicineOV.CurrentSelectedMedicine = null;
            CBPViewModel.MedicineOV.Quantity = null;
        }

        private void GetPromo(OrderDetailOV orderDetailVO)
        {
            tblPromo appliedPromo = new tblPromo();
            if (CBPViewModel.CurrentCustomerOrder != null)
            {
                foreach (tblPromo promo in CBPViewModel.CurrentCustomerOrder.tblCustomer.tblPromoes)
                {
                    if (promo.MedicineID == CBPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineID)
                    {
                        appliedPromo = promo;
                        break;
                    }
                }
            }

            orderDetailVO.PromoPercentToString = appliedPromo.PromoPercent.ToString();
        }
    }
}
