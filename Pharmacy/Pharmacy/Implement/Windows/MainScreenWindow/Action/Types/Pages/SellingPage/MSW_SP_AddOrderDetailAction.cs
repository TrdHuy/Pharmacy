using Pharmacy.Implement.Utils.DatabaseManager;
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
        private bool _isAddCustomerSuccessfully = true;

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
                return false;
            }

            if (!IsShouldAddOrderDetail())
            {
                return false;
            }

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
            return true;
        }

        private bool IsShouldAddOrderDetail()
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
                    return false;
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
                    DbManager.Instance.ExecuteQuery(SQLCommandKey.ADD_NEW_CUSTOMER_CMD_KEY,
                        _queryObserver,
                        newCustomer,
                        "");

                    if (!_isAddCustomerSuccessfully)
                    {
                        return false;
                    }
                    else
                    {
                        _viewModel.CustomerItemSource.Add(newCustomer);
                        _viewModel.CurrentSelectedCustomer = newCustomer;
                    }
                }
            }
            return true;
        }

        private void AddNewCustomerToDataBaseCallBack(SQLQueryResult queryResult)
        {
            if (queryResult.MesResult == MessageQueryResult.Done)
            {

                App.Current.ShowApplicationMessageBox("Thêm khách hàng mới thành công!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                   OwnerWindow.MainScreen,
                   "Thông báo!!");
            }
            else
            {
                App.Current.ShowApplicationMessageBox("Lỗi thêm khách hàng mới!",
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Lỗi!!");
                _isAddCustomerSuccessfully = false;
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
    }
}
