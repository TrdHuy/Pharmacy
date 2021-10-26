using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.CustomerManagement.CustomerBill
{
    public class GetMedicineQuantityOfInvoiceCreationDateAction : AbstractQueryAction
    {
        public GetMedicineQuantityOfInvoiceCreationDateAction()
        {
            _action = GetMedicineQuantity;
        }

        private SQLQueryResult GetMedicineQuantity(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            tblMedicine item = paramaters[0] as tblMedicine;

            DateTime? toDate = null;

            if (paramaters != null)
            {
                try
                {
                    toDate = (DateTime)paramaters[1];
                }
                catch
                {
                }
            }

            try
            {
                var imprtQuan = item.tblWarehouseImportDetails?
                    .Where(o =>
                    {
                        if (toDate != null)
                        {
                            return o.IsActive
                            && DateTime.Compare(o.tblWarehouseImport.ImportTime, (DateTime)toDate) <= 0;
                        }
                        return o.IsActive;
                    })
                    .Sum(o => o.Quantity);

                var exportQuan = item.tblOrderDetails?
                    .Where(o =>
                    {
                        if (toDate != null)
                        {
                            return o.IsActive
                            && DateTime.Compare(o.tblOrder.OrderTime, (DateTime)toDate) < 0;
                        }
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
