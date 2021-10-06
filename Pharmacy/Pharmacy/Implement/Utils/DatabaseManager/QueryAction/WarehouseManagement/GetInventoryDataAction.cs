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
            try
            {
                int count = 0;
                List<tblWarehouseImport> warehouseImportLst = appDBContext.tblWarehouseImports
                        .Where(o => o.IsActive)
                        .OrderByDescending(o => o.ImportTime)
                        .ToList();
                var res = warehouseImportLst
                    .SelectMany(warehouseImport => warehouseImport.tblWarehouseImportDetails
                    , (warehouseImport, importDetail) =>
                    {
                        return new
                        {
                            Med = importDetail.tblMedicine,
                            MedType = importDetail.tblMedicine.tblMedicineType,
                            MedName = importDetail.tblMedicine.MedicineName,
                            ImprtDate = warehouseImport.ImportTime,
                            MedUnit = importDetail.tblMedicine.tblMedicineUnit,
                            Quantity = importDetail.Quantity
                        };
                    })
                    .GroupBy(item => item.Med, item => item,
                    (med, itemLst) =>
                    {
                        return new
                        {
                            ID = count++,
                            TotalQuantity = itemLst.Sum(item => item.Quantity),
                            Med = med,
                            ImportDate = itemLst.Max(item => item.ImprtDate)
                        };
                    });
                ObservableCollection<object> output = new ObservableCollection<object>(res);

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
