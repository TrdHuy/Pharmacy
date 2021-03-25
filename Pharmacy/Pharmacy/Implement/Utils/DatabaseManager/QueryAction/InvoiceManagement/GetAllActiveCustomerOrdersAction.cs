using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.InvoiceManagement
{
    public class GetAllActiveCustomerOrdersAction : AbstractQueryAction
    {
        public GetAllActiveCustomerOrdersAction()
        {
            _action = GetAllActiveCustomerOrdersData;
        }

        private SQLQueryResult GetAllActiveCustomerOrdersData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblOrders.
                    Where<tblOrder>(order => order.IsActive).OrderByDescending(o=>o.OrderTime);
                result = new SQLQueryResult(x, MessageQueryResult.Done);
            }
            catch (Exception e)
            {
                ShowErrorMessageBox(e);
                result = new SQLQueryResult(null, MessageQueryResult.Aborted, "Lỗi load dữ liệu hóa đơn!");

            }
            return result;
        }
    }
}
