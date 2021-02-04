using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.Selling
{
    public class AddNewCustomerOrderDetailAction : AbstractQueryAction
    {
        private const int SINGLE_HANDLER = 1;
        private const int MULTI_HANDLER = 2;
        private int handle = 0;
        public AddNewCustomerOrderDetailAction()
        {
            _action = AddNewCustomerOrderDetail;
        }

        private SQLQueryResult AddNewCustomerOrderDetail(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                switch (handle)
                {
                    case SINGLE_HANDLER:
                        tblOrderDetail newOrder = paramaters[0] as tblOrderDetail;
                        appDBContext.tblOrderDetails.Add(newOrder);
                        appDBContext.SaveChanges();
                        result = new SQLQueryResult(newOrder, MessageQueryResult.Done, "Thêm chi tiết đơn hàng thành công!");
                        break;
                    case MULTI_HANDLER:
                        var newOrders = paramaters[0] as IEnumerable<tblOrderDetail>;
                        appDBContext.tblOrderDetails.AddRange(newOrders);
                        appDBContext.SaveChanges();
                        result = new SQLQueryResult(newOrders, MessageQueryResult.Done, "Thêm chi tiết đơn hàng thành công!");
                        break;
                    default:
                        break;

                }
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
                result = new SQLQueryResult(null, MessageQueryResult.Aborted, "Lỗi thêm chi tiết đơn hàng!");
            }
            catch (Exception e)
            {
                result = new SQLQueryResult(null, MessageQueryResult.Aborted, "Lỗi thêm chi tiết đơn hàng!");
                ShowErrorMessageBox(e);
            }
            
            return result;
        }

        public override bool CanExecute(object[] paramaters)
        {
            handle = 0;

            if (paramaters.Length > 1)
            {
                throw new InvalidOperationException("AddNewCustomerOrderDetailAction: can not pass more than one argument of paramaters");
            }
            if (paramaters.Length == 0)
            {
                throw new InvalidOperationException("AddNewCustomerOrderDetailAction: need a paramater as tblOderDetail or IEnumerable<tblOderDetail>");
            }

            if (!paramaters[0].GetType().GetInterfaces().Any(o => o.Name.Contains(typeof(IEnumerable<tblOrderDetail>).Name)) &&
                 !paramaters[0].GetType().Name.Contains(typeof(tblOrderDetail).Name))
            {
                throw new InvalidOperationException("AddNewCustomerOrderDetailAction: paramater must be type of tblOderDetail or IEnumerable<tblOderDetail>");
            }

            if (paramaters[0].GetType().GetInterfaces().Any(o => o.Name.Contains(typeof(IEnumerable<tblOrderDetail>).Name)))
            {
                handle = MULTI_HANDLER;
            }
            else if (paramaters[0].GetType().Name.Contains(typeof(tblOrderDetail).Name))
            {
                handle = SINGLE_HANDLER;
            }
            return base.CanExecute(paramaters);
        }
    }
}
