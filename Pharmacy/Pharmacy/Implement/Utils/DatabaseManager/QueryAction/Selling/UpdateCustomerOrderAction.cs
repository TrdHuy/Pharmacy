using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.Selling
{
    public class UpdateCustomerOrderAction : AbstractQueryAction
    {
        public UpdateCustomerOrderAction()
        {
            _action = UpdateCustomerOrder;
        }

        private SQLQueryResult UpdateCustomerOrder(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                tblOrder customerOrder = paramaters[0] as tblOrder;
                var orderDB = appDBContext.tblOrders.First(o => o.OrderID == customerOrder.OrderID);

                if (orderDB != null)
                {
                    orderDB.OrderTime = customerOrder.OrderTime;
                    orderDB.OrderDescription = customerOrder.OrderDescription;
                    orderDB.IncludeVAT = customerOrder.IncludeVAT;
                    orderDB.IsActive = customerOrder.IsActive;
                    orderDB.PurchasePrice = customerOrder.PurchasePrice;
                    orderDB.TotalPrice = customerOrder.TotalPrice;
                    orderDB.UserID = customerOrder.UserID;
                    orderDB.CustomerID = customerOrder.CustomerID;
                    orderDB.tblOrderDetails = customerOrder.tblOrderDetails;
                    appDBContext.SaveChanges();
                    result = new SQLQueryResult(orderDB, MessageQueryResult.Done, "Cập nhật thông tin hóa đơn thành công!");
                }
                else
                {
                    result = new SQLQueryResult(null, MessageQueryResult.Aborted, "Không tìm thấy hóa đơn khi cập nhật cơ sở dữ liệu!");
                }
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
            }
            catch (Exception e)
            {
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                ShowErrorMessageBox(e);
            }

            return result;
        }

        public override bool CanExecute(object[] paramaters)
        {

            if (paramaters.Length > 1)
            {
                throw new InvalidOperationException("UpdateCustomerOrderAction: can not pass more than one argument of paramaters");
            }
            if (paramaters.Length == 0)
            {
                throw new InvalidOperationException("UpdateCustomerOrderAction: need a paramater as " + typeof(tblOrder).Name);
            }

            if (!paramaters[0].GetType().Name.Contains(typeof(tblOrder).Name))
            {
                throw new InvalidOperationException("UpdateCustomerOrderAction: paramater must be type of tblOrder");
            }

            return base.CanExecute(paramaters);
        }
    }
}