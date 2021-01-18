using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.Selling
{
    public class AddNewCustomerOrderAction : AbstractQueryAction
    {
        public AddNewCustomerOrderAction()
        {
            _action = AddNewCustomerOrder;
        }

        private SQLQueryResult AddNewCustomerOrder(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblOrder newOrder = paramaters[0] as tblOrder;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);

            try
            {
                appDBContext.tblOrders.Add(newOrder);
                appDBContext.SaveChanges();
                result = new SQLQueryResult(newOrder, MessageQueryResult.Done, "Thêm hóa đơn mới thành công!");
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
                result = new SQLQueryResult(newOrder, MessageQueryResult.Aborted, "Lỗi thêm hóa đơn mới!");
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e);
                result = new SQLQueryResult(newOrder, MessageQueryResult.Aborted, "Lỗi thêm hóa đơn mới!");
            }

            return result;
        }

        public override bool CanExecute(object[] paramaters)
        {
            if (paramaters.Length > 1)
            {
                throw new InvalidOperationException(this.GetType().Name + ": can not pass more than one argument of paramaters");
            }
            if (paramaters.Length == 0)
            {
                throw new InvalidOperationException(this.GetType().Name + ": need a paramater as " + typeof(tblOrder).Name);
            }

            if (!paramaters[0].GetType().Name.Equals(typeof(tblOrder).Name))
            {
                throw new InvalidOperationException(this.GetType().Name + ": paramater must be type of tblOrder");
            }

            return base.CanExecute(paramaters);
        }
    }


}
