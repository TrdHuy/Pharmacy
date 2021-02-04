using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.CustomerManagement.CustomerBill
{
    public class SetCustomerOrderDeactiveAction : AbstractQueryAction
    {
        public SetCustomerOrderDeactiveAction()
        {
            _action = SetCustomerOrderDeactive;
        }

        private SQLQueryResult SetCustomerOrderDeactive(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblOrder cusOrder = paramaters[0] as tblOrder;
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblOrders.Where(order => order.OrderID == cusOrder.OrderID).
                    First();
                x.IsActive = false;

                foreach(tblOrderDetail oD in cusOrder.tblOrderDetails)
                {
                    oD.IsActive = false;
                }

                appDBContext.SaveChanges();
                result = new SQLQueryResult(null, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                App.Current.ShowApplicationMessageBox(e.Message);
            }
            return result;
        }

        public override bool CanExecute(object[] paramaters)
        {

            if (paramaters.Length > 1)
            {
                throw new InvalidOperationException("SetCustomerOrderDeactiveAction: can not pass more than one argument of paramaters");
            }
            if (paramaters.Length == 0)
            {
                throw new InvalidOperationException("SetCustomerOrderDeactiveAction: need a paramater as tblOrder or IEnumerable<tblOrder>");
            }

            if (!paramaters[0].GetType().Name.Contains(typeof(tblOrder).Name))
            {
                throw new InvalidOperationException("SetCustomerOrderDeactiveAction: paramater must be type of tblOrder");
            }

            return base.CanExecute(paramaters);
        }
    }
}

