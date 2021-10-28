using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using System;
using System.Linq;
using System.Windows.Controls;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Collections.Generic;
using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage
{
    internal class MSW_SP_AddOrderDetailAction : MSW_SP_ButtonAction
    {
        private SQLQueryCustodian _queryObserver;
        private DataGrid orderDetaiDataGrid;
        private double _quantityLeft;
        private bool _useQuantityLeft = false;

        public MSW_SP_AddOrderDetailAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            orderDetaiDataGrid = DataTransfer[0] as DataGrid;

            if (!SPViewModel.IsAddOrderDetailCanPerform)
            {

                if (String.IsNullOrEmpty(SPViewModel.MedicineOV.Quantity) || SPViewModel.MedicineOV.Quantity.Equals("0"))
                {
                    App.Current.ShowApplicationMessageBox("Kiểm tra lại số lượng!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Cảnh báo!");
                    SPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                    return;
                }
                else
                {
                    App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường đang bỏ trống!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Cảnh báo!");
                    SPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                }
                return;
            }
            ShouldCreateNewCustomer();

            return;
        }


        private void ShouldCreateNewCustomer()
        {
            if (SPViewModel.CustomerOV.CurrentSelectedCustomer == null)
            {
                var x = App.Current.ShowApplicationMessageBox("Khách hàng hiện chưa trong cơ sở dữ liệu, bạn có muốn thêm mới?",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo");
                if (x == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultNo)
                {
                    SPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                    return;
                }
                else
                {
                    tblCustomer newCustomer = new tblCustomer()
                    {
                        CustomerName = SPViewModel.CustomerOV.CustomerName,
                        Phone = SPViewModel.CustomerOV.CustomerPhone,
                        Address = SPViewModel.CustomerOV.CustomerAddress,
                        IsActive = true
                    };

                    _queryObserver = new SQLQueryCustodian(AddNewCustomerToDataBaseCallBack,
                        AddNewCustomerToDataBaseForceCallBack,
                        typeof(MSW_SP_AddOrderDetailAction));
                    DbManager.Instance.ExecuteQueryAsync(SQLCommandKey.ADD_NEW_CUSTOMER_CMD_KEY,
                        PharmacyDefinitions.ADD_NEW_CUSTOMER_DELAY_TIME,
                        _queryObserver,
                        newCustomer,
                        "");
                }
            }
            else
            {
                CreateNewOrderDetail();
            }
        }

        private void AddNewCustomerToDataBaseForceCallBack(SQLQueryResult queryResult)
        {
            // Handle something here 
        }

        private void AddNewCustomerToDataBaseCallBack(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                tblCustomer newCustomer = queryResult.Result as tblCustomer;
                SPViewModel.CustomerItemSource.Add(newCustomer);
                SPViewModel.CustomerOV.CurrentSelectedCustomer = newCustomer;

                App.Current.ShowApplicationMessageBox("Thêm khách hàng mới thành công!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo");
                CreateNewOrderDetail();
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi thêm khách hàng mới. Vui lòng liên hệ CSKH để biết thêm thông tin!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Lỗi!");
                SPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
            }
        }

        private void GetPromo(OrderDetailOV orderDetailVO)
        {
            tblPromo appliedPromo = new tblPromo();
            if (SPViewModel.CustomerOV.CurrentSelectedCustomer != null)
            {
                foreach (tblPromo promo in SPViewModel.CustomerOV.CurrentSelectedCustomer.tblPromoes)
                {
                    if (promo.MedicineID == SPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineID)
                    {
                        appliedPromo = promo;
                        break;
                    }
                }
            }

            orderDetailVO.PromoPercentToString = appliedPromo.PromoPercent.ToString();
        }

        private void CreateNewOrderDetail()
        {
            try
            {
                OrderDetailOV checkExistedVO = null;
                try
                {
                    if (SPViewModel.CustomerOrderDetailItemSource.Count > 0)
                    {
                        checkExistedVO = SPViewModel.CustomerOrderDetailItemSource.First(VO =>
                        VO.MedicineID.Equals(SPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineID));
                    }
                }
                catch (Exception e)
                {

                }

                if (checkExistedVO != null)
                {
                    checkExistedVO.QuantityToString = (checkExistedVO.Quantity
                        + Convert.ToDouble(_useQuantityLeft ? _quantityLeft.ToString() : SPViewModel.MedicineOV.Quantity)).ToString();
                    orderDetaiDataGrid.Items.Refresh();
                    SPViewModel.Invalidate(SPViewModel.MedicineOV, "MedicineCost");
                    SPViewModel.Invalidate(SPViewModel.MedicineOV, "TotalCost");
                    SPViewModel.Invalidate(SPViewModel.MedicineOV, "RestAmount");

                    ClearAddedMedicineInfo();
                }
                else
                {
                    MSW_SP_OrderDetailOV orderDetailVO = new MSW_SP_OrderDetailOV(SPViewModel);
                    orderDetailVO.MedicineName = SPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineName;
                    orderDetailVO.Medicine = SPViewModel.MedicineOV.CurrentSelectedMedicine;
                    orderDetailVO.MedicineID = SPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineID;
                    orderDetailVO.MedicineUnitName = SPViewModel.MedicineOV.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName;
                    orderDetailVO.QuantityToString = _useQuantityLeft ? _quantityLeft.ToString() : SPViewModel.MedicineOV.Quantity;
                    orderDetailVO.UnitPrice = SPViewModel.MedicineOV.CurrentSelectedMedicine.AskingPrice;
                    orderDetailVO.UnitBidPrice = SPViewModel.MedicineOV.CurrentSelectedMedicine.BidPrice;
                    GetPromo(orderDetailVO);

                    if (orderDetailVO.Quantity > 0)
                    {
                        SPViewModel.CustomerOrderDetailItemSource.Add(orderDetailVO);
                        ClearAddedMedicineInfo();
                    }
                }

                SPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;                
            }
            catch (Exception e)
            {

            }
        }

        private void ClearAddedMedicineInfo()
        {
            SPViewModel.MedicineOV.CurrentSelectedMedicine = null;
            SPViewModel.MedicineOV.Quantity = null;
        }
    }
}
