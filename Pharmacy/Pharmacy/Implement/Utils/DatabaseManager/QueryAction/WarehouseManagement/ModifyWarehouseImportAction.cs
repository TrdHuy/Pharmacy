using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.WarehouseManagement
{
    public class ModifyWarehouseImportAction : AbstractQueryAction
    {
        public ModifyWarehouseImportAction()
        {
            _action = ModifyWarehouseImport;
        }

        private SQLQueryResult ModifyWarehouseImport(PharmacyDBContext appDBContext, object[] paramaters)
        {
            tblWarehouseImport import = paramaters[0] as tblWarehouseImport;
            List<tblWarehouseImportDetail> details = paramaters[1] as List<tblWarehouseImportDetail>;
            string imageFolder = paramaters[2] as string;

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                tblWarehouseImport importInfo = appDBContext.tblWarehouseImports.Where(o => o.ImportID == import.ImportID).FirstOrDefault();
                importInfo.IsActive = true;
                importInfo.ImportDescription = import.ImportDescription;
                importInfo.TotalPrice = import.TotalPrice;
                importInfo.PurchasePrice = import.PurchasePrice;

                //Add or Modify active import details
                foreach (var item in details)
                {
                    tblWarehouseImportDetail detail = importInfo.tblWarehouseImportDetails.Where(o => o.MedicineID == item.MedicineID).FirstOrDefault();
                    if (detail != null)
                    {
                        detail.IsActive = true;
                        detail.Price = item.Price;
                        detail.Quantity = item.Quantity;
                    }
                    else
                    {
                        importInfo.tblWarehouseImportDetails.Add(item);
                    }
                }

                //Deactive import details
                foreach (var item in import.tblWarehouseImportDetails.Where(o => !o.IsActive))
                {
                    tblWarehouseImportDetail detail = importInfo.tblWarehouseImportDetails.Where(o => o.MedicineID == item.MedicineID).FirstOrDefault();
                    if (detail != null)
                    {
                        detail.IsActive = false;
                    }
                }

                appDBContext.SaveChanges();

                //Cập nhật giá vào thông tin thuốc
                foreach (var item in import.tblWarehouseImportDetails)
                {
                    if (item.IsActive)
                        item.tblMedicine.BidPrice = item.Price;
                    else
                    {
                        //Cập nhật giá nhập thuốc dựa vào giá của đơn nhập gần nhất sau khi xóa thông tin ở đơn hiện tại
                        var prevDetail = item.tblMedicine.tblWarehouseImportDetails.Where(o => o.IsActive).OrderByDescending(o => o.tblWarehouseImport.ImportTime).FirstOrDefault();
                        if (prevDetail != null)
                            item.tblMedicine.BidPrice = prevDetail.Price;
                    }
                }
                appDBContext.SaveChanges();

                if (imageFolder.Length > 0 && !SaveImageToFile(import.ImportID.ToString(), imageFolder, ImageType.WarehouseImport))
                {
                    result = new SQLQueryResult(null, MessageQueryResult.Aborted);
                    return result;
                }
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
