using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.DatabaseManager;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SellingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SellingPage
{
    public class MSW_SP_AddOrderDetailAction : Base.UIEventHandler.Action.IAction
    {
        private SellingPageViewModel _viewModel;
        private SQLQueryCustodian _queryObserver;
        private DataGrid ctrl;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as SellingPageViewModel;
            ctrl = dataTransfer[1] as DataGrid;

            if (!_viewModel.IsAddOrderDetailCanPerform)
            {
                App.Current.ShowApplicationMessageBox("Kiểm tra lại các trường đang bỏ trống!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!!");
                _viewModel.IsAddOrderDeatailButtonRunning = false;
                return false;
            }

            ShouldCreateNewCustomer();

            return true;
        }

       
        private void ShouldCreateNewCustomer()
        {
            if (_viewModel.CurrentSelectedCustomer == null)
            {
                var x = App.Current.ShowApplicationMessageBox("Khách hàng hiện chưa trong cơ sở dữ liệu, bạn có muốn thêm mới?",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.YesNo,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Question,
                    OwnerWindow.MainScreen,
                    "Thông báo!!");
                if (x == HPSolutionCCDevPackage.netFramework.AnubisMessgaeResult.ResultNo)
                {
                    _viewModel.IsAddOrderDeatailButtonRunning = false;
                    return;
                }
                else
                {
                    tblCustomer newCustomer = new tblCustomer()
                    {
                        CustomerName = _viewModel.CustomerName,
                        Phone = _viewModel.CustomerPhone,
                        Address = _viewModel.CustomerAddress,
                        IsActive = true
                    };

                    _queryObserver = new SQLQueryCustodian(AddNewCustomerToDataBaseCallBack);
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

        private void AddNewCustomerToDataBaseCallBack(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {
                tblCustomer newCustomer = queryResult.Result as tblCustomer;
                _viewModel.ForceAssignCurentSelectedUser = true;
                _viewModel.CustomerItemSource.Add(newCustomer);
                _viewModel.CurrentSelectedCustomer = newCustomer;
                _viewModel.ForceAssignCurentSelectedUser = false;

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
                _viewModel.IsAddOrderDeatailButtonRunning = false;
            }
        }

        private void GetPromo(OrderDetailVO orderDetailVO)
        {
            tblPromo appliedPromo = new tblPromo();
            if (_viewModel.CurrentSelectedCustomer != null)
            {
                foreach (tblPromo promo in _viewModel.CurrentSelectedCustomer.tblPromoes)
                {
                    if (promo.MedicineID == _viewModel.CurrentSelectedMedicine.MedicineID)
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
                OrderDetailVO orderDetailVO = new OrderDetailVO();
                orderDetailVO.MedicineName = _viewModel.CurrentSelectedMedicine.MedicineName;
                orderDetailVO.MedicineID = _viewModel.CurrentSelectedMedicine.MedicineID;
                orderDetailVO.MedicineUnitName = _viewModel.CurrentSelectedMedicine.tblMedicineUnit.MedicineUnitName;
                orderDetailVO.Quantity = Convert.ToDouble(_viewModel.Quantity);
                orderDetailVO.UnitPrice = _viewModel.CurrentSelectedMedicine.AskingPrice;
                GetPromo(orderDetailVO);
                orderDetailVO.TotalPrice = Convert.ToDecimal(Convert.ToDouble(_viewModel.Quantity) *
                   Convert.ToDouble(_viewModel.CurrentSelectedMedicine.AskingPrice) *
                   (100 - orderDetailVO.PromoPercent) / 100);

                OrderDetailVO checkExistedVO = null;
                try
                {
                    if (_viewModel.CustomerOrderDetailItemSource.Count > 0)
                    {
                        checkExistedVO = _viewModel.CustomerOrderDetailItemSource.First(VO => VO.MedicineID.Equals(orderDetailVO.MedicineID));
                    }
                }
                catch (Exception e)
                {

                }


                if (checkExistedVO != null)
                {
                    checkExistedVO.Quantity += orderDetailVO.Quantity;
                    checkExistedVO.TotalPrice += orderDetailVO.TotalPrice;
                    ctrl.Items.Refresh();
                    _viewModel.Invalidate("MedicineCost");
                    _viewModel.Invalidate("TotalCost");
                    _viewModel.Invalidate("RestAmount");

                }
                else
                {
                    _viewModel.CustomerOrderDetailItemSource.Add(orderDetailVO);
                }
            }
            catch(Exception e)
            {

            }
            finally
            {
                _viewModel.IsAddOrderDeatailButtonRunning = false;
            }

        }

    }
}
