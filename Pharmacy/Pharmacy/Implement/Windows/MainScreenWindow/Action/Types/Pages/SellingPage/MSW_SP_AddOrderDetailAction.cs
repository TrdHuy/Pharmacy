using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using System;
using System.Linq;
using System.Windows.Controls;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage
{
    internal class MSW_SP_AddOrderDetailAction : MSW_SP_ButtonAction
    {
        private SQLQueryCustodian _queryObserver;
        private DataGrid orderDetaiDataGrid;

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
                    "Thông báo!!");
                    SPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                    return;
                }
                else
                {
                    App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường đang bỏ trống!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!!");
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
                    "Thông báo!!");
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
                   "Thông báo!!");
                CreateNewOrderDetail();
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi thêm khách hàng mới!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Lỗi!!");
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

            orderDetailVO.PromoPercent = appliedPromo.PromoPercent;
        }

        private void CreateNewOrderDetail()
        {
            try
            {
                OrderDetailOV orderDetailVO = new OrderDetailOV();
                orderDetailVO.MedicineName = SPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineName;
                orderDetailVO.MedicineID = SPViewModel.MedicineOV.CurrentSelectedMedicine.MedicineID;
                orderDetailVO.MedicineUnitName = SPViewModel.MedicineOV.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName;
                orderDetailVO.Quantity = Convert.ToDouble(SPViewModel.MedicineOV.Quantity);
                orderDetailVO.UnitPrice = SPViewModel.MedicineOV.CurrentSelectedMedicine.AskingPrice;
                orderDetailVO.UnitBidPrice = SPViewModel.MedicineOV.CurrentSelectedMedicine.BidPrice;
                GetPromo(orderDetailVO);
                orderDetailVO.TotalPrice = Convert.ToDecimal(Convert.ToDouble(SPViewModel.MedicineOV.Quantity) *
                   Convert.ToDouble(SPViewModel.MedicineOV.CurrentSelectedMedicine.AskingPrice) *
                   (100 - orderDetailVO.PromoPercent) / 100);

                OrderDetailOV checkExistedVO = null;
                try
                {
                    if (SPViewModel.CustomerOrderDetailItemSource.Count > 0)
                    {
                        checkExistedVO = SPViewModel.CustomerOrderDetailItemSource.First(VO => VO.MedicineID.Equals(orderDetailVO.MedicineID));
                    }
                }
                catch (Exception e)
                {

                }


                if (checkExistedVO != null)
                {
                    checkExistedVO.Quantity += orderDetailVO.Quantity;
                    checkExistedVO.TotalPrice += orderDetailVO.TotalPrice;
                    orderDetaiDataGrid.Items.Refresh();
                    SPViewModel.Invalidate(SPViewModel.MedicineOV, "MedicineCost");
                    SPViewModel.Invalidate(SPViewModel.MedicineOV, "TotalCost");
                    SPViewModel.Invalidate(SPViewModel.MedicineOV, "RestAmount");

                }
                else
                {
                    SPViewModel.CustomerOrderDetailItemSource.Add(orderDetailVO);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                SPViewModel.ButtonCommandOV.IsAddOrderDeatailButtonRunning = false;
                SPViewModel.MedicineOV.CurrentSelectedMedicine = null;
                SPViewModel.MedicineOV.Quantity = null;
                SPViewModel.MedicineOV.Invalidate("CurrentSelectedMedicine");
                SPViewModel.MedicineOV.Invalidate("Quantity");
            }

        }

    }
}
