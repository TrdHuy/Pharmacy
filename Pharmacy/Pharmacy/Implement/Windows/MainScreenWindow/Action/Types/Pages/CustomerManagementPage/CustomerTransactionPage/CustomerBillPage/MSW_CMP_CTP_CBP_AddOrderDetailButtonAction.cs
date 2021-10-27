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
        private double _quantityLeft;
        private double _quantityTillNowLeft;
        private bool _useQuantityLeft = false;
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

                var inputQuantity = Convert.ToDouble(CBPViewModel.MedicineOV.Quantity);
                _queryObserver = new SQLQueryCustodian((res) =>
                {
                    _quantityLeft = Convert.ToDouble(res.Result);
                    if (checkExistedVO != null)
                    {
                        _quantityLeft -= checkExistedVO.Quantity;
                    }
                });
                DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_MEDICINE_QUANTITY_OF_INVOICE_CREATION_DATE_CMD_KEY,
                    _queryObserver,
                    CBPViewModel.MedicineOV.CurrentSelectedMedicine,
                    CBPViewModel.CurrentCustomerOrder.OrderTime);

                _queryObserver = new SQLQueryCustodian((res) =>
                {
                    _quantityTillNowLeft = Convert.ToDouble(res.Result);
                    if (checkExistedVO != null)
                    {
                        _quantityTillNowLeft -= checkExistedVO.Quantity;
                    }
                });
                DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_MEDICINE_QUANTITY_TILL_NOW_EXCEPT_EDITTING_ORDER_DETAIL_CMD_KEY,
                    _queryObserver,
                    CBPViewModel.MedicineOV.CurrentSelectedMedicine,
                    checkExistedVO?.OrderDetailID);

                var optsSource = new List<OsirisButton>();


                if (inputQuantity > _quantityLeft && _quantityLeft > 0)
                {
                    optsSource.Add(new OsirisButton() { TextContent = "Chọn số lượng sản phẩm tối đa" });
                    optsSource.Add(new OsirisButton() { TextContent = "Chọn số lượng sản phẩm mong muốn (trong kho sẽ bị âm)" });
                    optsSource.Add(new OsirisButton() { TextContent = "Hủy" });

                    var result = App.Current.ShowApplicationMultiOptionMessageBox("Tính đến thời điểm tạo hóa đơn: "
                        + CBPViewModel.CurrentCustomerOrder.OrderTime.ToString("dd/MM/yyyy HH:mm") + "\n"

                        + (_quantityLeft <= 0 ? "Sản phẩm này trong kho đã hết (hoặc bị âm) " : "Sản phẩm này trong kho còn")
                        + _quantityLeft + "("
                        + CBPViewModel.MedicineOV.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName + ")\n"

                        + "Tính đến nay:\n"
                        + (_quantityTillNowLeft <= 0 ? "Sản phẩm này trong kho đã hết (hoặc bị âm) " : "Sản phẩm này trong kho còn")
                        + _quantityTillNowLeft + "("
                        + CBPViewModel.MedicineOV.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName + ")\n\n"

                        + "Bạn có muốn tiếp tục nhập?",
                    optsSource,
                    AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                    if (result == 2 || result == -1)
                    {
                        CBPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                        return;
                    }
                    else if (result == 0)
                    {
                        _useQuantityLeft = true;
                    }

                }
                else if (_quantityLeft <= 0)
                {
                    optsSource.Add(new OsirisButton() { TextContent = "Chọn số lượng sản phẩm mong muốn (trong kho sẽ bị âm)" });
                    optsSource.Add(new OsirisButton() { TextContent = "Hủy" });

                    var result = App.Current.ShowApplicationMultiOptionMessageBox("Tính đến thời điểm tạo hóa đơn: "
                        + CBPViewModel.CurrentCustomerOrder.OrderTime.ToString("dd/MM/yyyy HH:mm") + "\n"

                        + (_quantityLeft <= 0 ? "Sản phẩm này trong kho đã hết (hoặc bị âm) " : "Sản phẩm này trong kho còn ")
                        + _quantityLeft + "("
                        + CBPViewModel.MedicineOV.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName + ")\n"

                        + "Tính đến nay:\n"
                        +(_quantityTillNowLeft <= 0 ? "Sản phẩm này trong kho đã hết (hoặc bị âm) " : "Sản phẩm này trong kho còn")
                        + _quantityTillNowLeft + "("
                        + CBPViewModel.MedicineOV.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName + ")\n\n"

                        + "Bạn có muốn tiếp tục nhập?",
                    optsSource,
                    AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                    if (result == 1 || result == -1)
                    {
                        CBPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                        return;
                    }
                }

                if (checkExistedVO != null)
                {
                    checkExistedVO.QuantityToString = (checkExistedVO.Quantity 
                        + Convert.ToDouble(_useQuantityLeft ? _quantityLeft.ToString() : CBPViewModel.MedicineOV.Quantity)).ToString();
                    orderDetaiDataGrid.Items.Refresh();
                    CBPViewModel.Invalidate(CBPViewModel.MedicineOV, "MedicineCost");
                    CBPViewModel.Invalidate(CBPViewModel.MedicineOV, "TotalCost");
                    CBPViewModel.Invalidate(CBPViewModel.MedicineOV, "RestAmount");

                }
                else
                {
                    MSW_CMP_CTP_CBP_OderDetailOV orderDetailVO = new MSW_CMP_CTP_CBP_OderDetailOV(CBPViewModel);
                    orderDetailVO.Medicine = CBPViewModel.MedicineOV.CurrentSelectedMedicine;
                    orderDetailVO.Order = CBPViewModel.CurrentCustomerOrder;
                    orderDetailVO.MedicineName = CBPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineName;
                    orderDetailVO.MedicineID = CBPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineID;
                    orderDetailVO.MedicineUnitName = CBPViewModel.MedicineOV.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName;
                    orderDetailVO.QuantityToString = _useQuantityLeft ? _quantityLeft.ToString() : CBPViewModel.MedicineOV.Quantity;
                    orderDetailVO.UnitPrice = CBPViewModel.MedicineOV.CurrentSelectedMedicine.AskingPrice;
                    orderDetailVO.UnitBidPrice = CBPViewModel.MedicineOV.CurrentSelectedMedicine.BidPrice;
                    GetPromo(orderDetailVO);

                    CBPViewModel.CurrentOrderDetails.Add(orderDetailVO);
                }
                CBPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                CBPViewModel.MedicineOV.CurrentSelectedMedicine = null;
                CBPViewModel.MedicineOV.Quantity = null;
            }
            catch (Exception e)
            {

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

            orderDetailVO.PromoPercentToString = appliedPromo.PromoPercent.ToString();
        }
    }
}
