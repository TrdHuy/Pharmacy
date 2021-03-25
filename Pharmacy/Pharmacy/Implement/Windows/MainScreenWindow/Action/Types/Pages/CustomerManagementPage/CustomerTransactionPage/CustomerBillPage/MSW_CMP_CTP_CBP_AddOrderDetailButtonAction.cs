using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using System.Linq;
using System;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    internal class MSW_CMP_CTP_CBP_AddOrderDetailButtonAction : MSW_CMP_CTP_CBP_ButtonAction
    {
        public MSW_CMP_CTP_CBP_AddOrderDetailButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            if (!CBPViewModel.IsAddOrderDetailCanPerform)
            {
                if (String.IsNullOrEmpty(CBPViewModel.MedicineOV.Quantity) || CBPViewModel.MedicineOV.Quantity.Equals("0"))
                {
                    App.Current.ShowApplicationMessageBox("Kiểm tra lại số lượng!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!!");
                    CBPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                    return;
                }
                else
                {
                    App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường đang bỏ trống!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!!");
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
                OrderDetailOV orderDetailVO = new OrderDetailOV();
                orderDetailVO.MedicineName = CBPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineName;
                orderDetailVO.MedicineID = CBPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineID;
                orderDetailVO.MedicineUnitName = CBPViewModel.MedicineOV.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName;
                orderDetailVO.Quantity = Convert.ToDouble(CBPViewModel.MedicineOV.Quantity);
                orderDetailVO.UnitPrice = CBPViewModel.MedicineOV.CurrentSelectedMedicine.AskingPrice;
                orderDetailVO.UnitBidPrice = CBPViewModel.MedicineOV.CurrentSelectedMedicine.BidPrice;
                GetPromo(orderDetailVO);
                orderDetailVO.TotalPrice = Convert.ToDecimal(Convert.ToDouble(CBPViewModel.MedicineOV.Quantity) *
                   Convert.ToDouble(CBPViewModel.MedicineOV.CurrentSelectedMedicine.AskingPrice) *
                   (100 - orderDetailVO.PromoPercent) / 100);

                OrderDetailOV checkExistedVO = null;
                try
                {
                    if (CBPViewModel.CurrentOrderDetails.Count > 0)
                    {
                        checkExistedVO = CBPViewModel.CurrentOrderDetails.First(VO => VO.MedicineID.Equals(orderDetailVO.MedicineID));
                    }
                }
                catch (Exception e)
                {

                }


                if (checkExistedVO != null)
                {
                    checkExistedVO.Quantity += orderDetailVO.Quantity;
                    checkExistedVO.TotalPrice += orderDetailVO.TotalPrice;
                    //ctrl.Items.Refresh();
                    //_viewModel.Invalidate(_viewModel, "MedicineCost");
                    //_viewModel.Invalidate(_viewModel, "TotalCost");
                    //_viewModel.Invalidate(_viewModel, "RestAmount");

                }
                else
                {
                    CBPViewModel.CurrentOrderDetails.Add(orderDetailVO);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                CBPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                CBPViewModel.MedicineOV.CurrentSelectedMedicine = null;
                CBPViewModel.MedicineOV.Quantity = null;
                CBPViewModel.MedicineOV.Invalidate("CurrentSelectedMedicine");
                CBPViewModel.MedicineOV.Invalidate("Quantity");
            }

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

            orderDetailVO.PromoPercent = appliedPromo.PromoPercent;
        }
    }
}
