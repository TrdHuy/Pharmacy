using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.WarehouseManagement
{
    public class GetInventoryDataAction : AbstractQueryAction
    {
        public GetInventoryDataAction()
        {
            _action = GetInventoryData;
        }

        private SQLQueryResult GetInventoryData(PharmacyDBContext appDBContext, object[] paramaters)
        {
            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            DateTime? fromDate = null;
            DateTime? toDate = null;

            if (paramaters != null)
            {
                try
                {
                    fromDate = (DateTime)paramaters[0];
                    toDate = (DateTime)paramaters[1];
                }
                catch
                {
                }
            }

            try
            {
                List<tblMedicine> LstMedicine = appDBContext.tblMedicines
                        .Where(o => o.IsActive)
                        .ToList();
                ObservableCollection<object> output = new ObservableCollection<object>();
                int count = 0;
                foreach (var item in LstMedicine)
                {
                    var imprtQuan = item.tblWarehouseImportDetails?
                        .Where(o =>
                        {
                            if (fromDate != null && toDate != null)
                            {
                                return o.IsActive
                                && DateTime.Compare(o.tblWarehouseImport.ImportTime, (DateTime)fromDate) >= 0
                                && DateTime.Compare(o.tblWarehouseImport.ImportTime, (DateTime)toDate) <= 0;
                            }
                            return o.IsActive;
                        })
                        .Sum(o => o.Quantity);

                    var exportQuan = item.tblOrderDetails?
                        .Where(o =>
                        {
                            if (fromDate != null && toDate != null)
                            {
                                return o.IsActive
                                && DateTime.Compare(o.tblOrder.OrderTime, (DateTime)fromDate) >= 0
                                && DateTime.Compare(o.tblOrder.OrderTime, (DateTime)toDate) <= 0;
                            }
                            return o.IsActive;
                        })
                        .Sum(o => o.Quantity);
                    double quantity = Convert.ToDouble(imprtQuan) - Convert.ToDouble(exportQuan);
                    output.Add(new
                    {
                        ID = count++,
                        Quantity = quantity,
                        MedName = item.MedicineName,
                        MedId = item.MedicineID,
                        MedUnit = item.tblMedicineUnit.MedicineUnitName,
                        MedType = item.tblMedicineType.MedicineTypeName,
                        ImportDate = item.tblWarehouseImportDetails.Max(iprtDetail => iprtDetail.tblWarehouseImport?.ImportTime),
                    });
                }
                //List<tblWarehouseImport> warehouseImportLst = appDBContext.tblWarehouseImports
                //        .Where(o => o.IsActive)
                //        .OrderByDescending(o => o.ImportTime)
                //        .ToList();
                //var res = warehouseImportLst
                //    .SelectMany(warehouseImport => warehouseImport.tblWarehouseImportDetails
                //    , (warehouseImport, importDetail) =>
                //    {
                //        return new
                //        {
                //            Med = importDetail.tblMedicine,
                //            MedType = importDetail.tblMedicine.tblMedicineType,
                //            MedName = importDetail.tblMedicine.MedicineName,
                //            ImprtDate = warehouseImport.ImportTime,
                //            MedUnit = importDetail.tblMedicine.tblMedicineUnit,
                //            Quantity = importDetail.Quantity
                //        };
                //    })
                //    .GroupBy(item => item.Med, item => item,
                //    (med, itemLst) =>
                //    {
                //        return new
                //        {
                //            ID = count++,
                //            TotalQuantity = itemLst.Sum(item => item.Quantity),
                //            Med = med,
                //            ImportDate = itemLst.Max(item => item.ImprtDate)
                //        };
                //    });


                result = new SQLQueryResult(output, MessageQueryResult.Done);
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
