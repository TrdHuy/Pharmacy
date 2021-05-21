using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.WarehouseManagement
{
    public class SetWarehouseImportDeactiveAction : AbstractQueryAction
    {
        public SetWarehouseImportDeactiveAction()
        {
            _action = SetWarehouseImportDeactive;
        }

        private SQLQueryResult SetWarehouseImportDeactive(PharmacyDBContext appDBContext, object[] paramaters)
        {
            long id = (long)paramaters[0];
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                var x = appDBContext.tblWarehouseImports.Where(o => o.ImportID == id).FirstOrDefault();
                x.IsActive = false;
                foreach (var item in x.tblWarehouseImportDetails)
                {
                    bool needUpdate = item.IsActive;
                    item.IsActive = false;
                    if (needUpdate)
                    {
                        //Cập nhật giá nhập thuốc dựa vào giá của đơn nhập gần nhất sau khi xóa thông tin ở đơn hiện tại
                        var prevDetail = item.tblMedicine.tblWarehouseImportDetails.Where(o => o.IsActive).OrderByDescending(o => o.tblWarehouseImport.ImportTime).FirstOrDefault();
                        if (prevDetail != null)
                            item.tblMedicine.BidPrice = prevDetail.Price;
                    }
                }
                appDBContext.SaveChanges();

                result = new SQLQueryResult(null, MessageQueryResult.Done);
                return result;
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message);
                result = new SQLQueryResult(null, MessageQueryResult.Aborted);
            }
            return result;
        }
    }
}
