using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils.DatabaseManager.QueryAction.Report
{
    public class GetInfoForComprehensiveReportAction : AbstractQueryAction
    {
        public GetInfoForComprehensiveReportAction()
        {
            _action = GetInfoForComprehensiveReport;
        }

        private SQLQueryResult GetInfoForComprehensiveReport(PharmacyDBContext appDBContext, object[] paramaters)
        {
            DateTime startDate = (DateTime)paramaters[0];
            DateTime endDate = (DateTime)paramaters[1];
            ReportPageViewModel reportPageViewModel = (ReportPageViewModel)paramaters[2];

            SQLQueryResult result = new SQLQueryResult(null, MessageQueryResult.Non);
            try
            {
                MSW_RP_ComprehensiveReportOV output = new MSW_RP_ComprehensiveReportOV(reportPageViewModel);
                var orders = appDBContext.tblOrders.Where<tblOrder>(order => order.IsActive && order.OrderTime >= startDate && order.OrderTime <= endDate).ToList();
                var warehouseImports = appDBContext.tblWarehouseImports.Where(o => o.IsActive && o.ImportTime >= startDate && o.ImportTime <= endDate).ToList();

                output.TongGiaTriNhap = warehouseImports.Sum(o => o.TotalPrice);
                output.TongGiaTriXuat = orders.Sum(o => o.TotalPrice);

                //Tinh tong no KH
                output.TongNoKH = 0;
                foreach (var item in appDBContext.tblCustomers.Where(o => o.IsActive))
                {
                    foreach (var order in item.tblOrders.Where(o => o.IsActive && o.OrderTime >= startDate && o.OrderTime <= endDate))
                    {
                        if (order.TotalPrice > order.PurchasePrice)
                            output.TongNoKH += order.TotalPrice - order.PurchasePrice;
                        else if (order.TotalPrice < order.PurchasePrice)
                            output.TongNoKH -= order.PurchasePrice - order.TotalPrice;
                    }
                }

                //Tinh tong no NCC
                output.TongNoNCC = 0;
                foreach (var item in appDBContext.tblSuppliers.Where(o => o.IsActive))
                {
                    foreach (var order in item.tblWarehouseImports.Where(o => o.IsActive && o.ImportTime >= startDate && o.ImportTime <= endDate))
                    {
                        if (order.TotalPrice > order.PurchasePrice)
                            output.TongNoNCC += order.TotalPrice - order.PurchasePrice;
                        else if (order.TotalPrice < order.PurchasePrice)
                            output.TongNoNCC -= order.PurchasePrice - order.TotalPrice;
                    }
                }

                output.ThuBanHang = output.TongGiaTriXuat;
                var incomeList = appDBContext.tblOtherPayments.Where(o => o.IsActive && o.PaymentType == 0 && o.PaymentTime >= startDate && o.PaymentTime <= endDate).ToList();
                output.ThuKhac = incomeList != null && incomeList.Count > 0 ? incomeList.Sum(o => o.TotalPrice) : 0;
                var outcomeList = appDBContext.tblOtherPayments.Where(o => o.IsActive && o.PaymentType == 1 && o.PaymentTime >= startDate && o.PaymentTime <= endDate).ToList();
                output.TongChi = outcomeList != null && outcomeList.Count > 0 ? outcomeList.Sum(o => o.TotalPrice) : 0;

                decimal loiNhuan = 0;
                foreach (var item in appDBContext.tblOrders.Where(o => o.IsActive && o.OrderTime >= startDate && o.OrderTime <= endDate))
                {
                    loiNhuan += item.TotalPrice - item.tblOrderDetails.Where(o => o.IsActive).Sum(o => o.UnitBidPrice * (decimal)o.Quantity);
                }
                output.LaiGop = output.ThuKhac + loiNhuan;

                result = new SQLQueryResult(output, MessageQueryResult.Done);
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
