using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.Report
{
    public class GetAllActiveCustomerOrdersByDateAction : AbstractQueryAction
    {
        public GetAllActiveCustomerOrdersByDateAction()
        {
            _action = GetAllActiveCustomerOrdersByDateData;
        }

        private SQLQueryResult GetAllActiveCustomerOrdersByDateData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            DateTime startDate = (DateTime)paramaters[0];
            DateTime endDate = (DateTime)paramaters[1];

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblOrders.
                    Where<tblOrder>(order => order.IsActive && order.OrderTime >= startDate && order.OrderTime <= endDate).ToList();
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
