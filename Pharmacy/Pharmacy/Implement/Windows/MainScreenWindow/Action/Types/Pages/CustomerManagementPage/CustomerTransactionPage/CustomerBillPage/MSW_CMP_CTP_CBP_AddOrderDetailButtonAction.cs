using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerTransaction.CustomerBillPage;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerTransactionPage.CustomerBillPage
{
    public class MSW_CMP_CTP_CBP_AddOrderDetailButtonAction : Base.UIEventHandler.Action.IAction
    {
        private CustomerBillPageViewModel _viewModel;
        private DataGrid ctrl;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as CustomerBillPageViewModel;
            ctrl = dataTransfer[1] as DataGrid;

            if (!_viewModel.IsAddOrderDetailCanPerform)
            {
                if (String.IsNullOrEmpty(_viewModel.MedicineOV.Quantity) || _viewModel.MedicineOV.Quantity.Equals("0"))
                {
                    App.Current.ShowApplicationMessageBox("Kiểm tra lại số lượng!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!!");
                    _viewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                    return false;
                }
                else
                {
                    App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường đang bỏ trống!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!!");
                    _viewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                }

                return false;
            }

            CreateNewOrderDetail();

            return true;
        }

        private void CreateNewOrderDetail()
        {
            try
            {
                OrderDetailOV orderDetailVO = new OrderDetailOV();
                orderDetailVO.MedicineName = _viewModel.MedicineOV.CurrentSelectedMedicine.MedicineName;
                orderDetailVO.MedicineID = _viewModel.MedicineOV.CurrentSelectedMedicine.MedicineID;
                orderDetailVO.MedicineUnitName = _viewModel.MedicineOV.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName;
                orderDetailVO.Quantity = Convert.ToDouble(_viewModel.MedicineOV.Quantity);
                orderDetailVO.UnitPrice = _viewModel.MedicineOV.CurrentSelectedMedicine.AskingPrice;
                orderDetailVO.UnitBidPrice = _viewModel.MedicineOV.CurrentSelectedMedicine.BidPrice;
                GetPromo(orderDetailVO);
                orderDetailVO.TotalPrice = Convert.ToDecimal(Convert.ToDouble(_viewModel.MedicineOV.Quantity) *
                   Convert.ToDouble(_viewModel.MedicineOV.CurrentSelectedMedicine.AskingPrice) *
                   (100 - orderDetailVO.PromoPercent) / 100);

                OrderDetailOV checkExistedVO = null;
                try
                {
                    if (_viewModel.CurrentOrderDetails.Count > 0)
                    {
                        checkExistedVO = _viewModel.CurrentOrderDetails.First(VO => VO.MedicineID.Equals(orderDetailVO.MedicineID));
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
                    _viewModel.CurrentOrderDetails.Add(orderDetailVO);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                _viewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
            }

        }

        private void GetPromo(OrderDetailOV orderDetailVO)
        {
            tblPromo appliedPromo = new tblPromo();
            if (_viewModel.CurrentCustomerOrder != null)
            {
                foreach (tblPromo promo in _viewModel.CurrentCustomerOrder.tblCustomer.tblPromoes)
                {
                    if (promo.MedicineID == _viewModel.MedicineOV.CurrentSelectedMedicine.MedicineID)
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
