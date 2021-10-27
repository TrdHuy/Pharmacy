using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.CustomerManagement.CustomerBill
{
    public class GetMedicineQuantityTillNowExceptEdittingOrderDetailAction : AbstractQueryAction
    {
        public GetMedicineQuantityTillNowExceptEdittingOrderDetailAction()
        {
            _action = GetMedicineQuantity;
        }

        private SQLQueryResult GetMedicineQuantity(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            tblMedicine item = paramaters[0] as tblMedicine;
            long edittingOrderDetailID = -1;
            if (paramaters[1] != null)
            {
                edittingOrderDetailID = (long)paramaters[1];
            }


            try
            {
                var imprtQuan = item.tblWarehouseImportDetails?
                    .Where(o =>
                    {
                        return o.IsActive;
                    })
                    .Sum(o => o.Quantity);

                var exportQuan = item.tblOrderDetails?
                    .Where(o =>
                    {
                        if (edittingOrderDetailID != -1)
                            return o.IsActive && o.OrderDetailID != edittingOrderDetailID;
                        return o.IsActive;
                    })
                    .Sum(o => o.Quantity);

                double quantity = Convert.ToDouble(imprtQuan) - Convert.ToDouble(exportQuan);
                result = new SQLQueryResult(quantity, MessageQueryResult.Done);
            }
            catch (DbEntityValidationException e)
            {
                HandleDbEntityValidationException(e);
                result = new SQLQueryResult(null, MessageQueryResult.Aborted, "Lỗi lấy dữ liệu tồn kho!");
            }
            catch (Exception e)
            {
                result = new SQLQueryResult(null, MessageQueryResult.Aborted, "Lỗi lấy dữ liệu tồn kho!");
                ShowErrorMessageBox(e);
            }

            return result;
        }
    }
}
