using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Windows.BaseWindow.Action.Types;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InventoryManagementPage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.InventoryManagementPage
{

    internal class MSW_InvMP_ExportExcelFileAction : BaseViewModelCommandExecuter
    {
        protected InventoryManagementPageViewModel IMPViewModel
        {
            get
            {
                return ViewModel as InventoryManagementPageViewModel;
            }
        }

        public MSW_InvMP_ExportExcelFileAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Lưu tập tin Excel";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Filter = "Excel|*.xlsx; *.xls";

            if (saveFileDialog.ShowDialog() == true)
            {
                ExcelPackage excelPackage = new ExcelPackage();
                var workSheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                workSheet.DefaultRowHeight = 12;

                //Header of table  
                workSheet.Row(1).Height = 30;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "STT";
                workSheet.Cells[1, 2].Value = "Looại thuốc";
                workSheet.Cells[1, 3].Value = "Tên thuốc";
                workSheet.Cells[1, 4].Value = "Ngày nhập gần nhất";
                workSheet.Cells[1, 5].Value = "Đơn vị";
                workSheet.Cells[1, 6].Value = "Tồn kho";

                for (int i = 1; i <= 6; i++)
                {
                    workSheet.Cells[1, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Color color = (Color)Application.Current.Resources["WordBlue"];
                    workSheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(color.A, color.R, color.G, color.B);
                    workSheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.White);
                }

                //Body of table
                int recordIndex = 2;
                foreach (var item in IMPViewModel.InventoryDataGridCache.Items)
                {
                    workSheet.Cells[recordIndex, 1].Value = GetPropValue(item, "ID");
                    workSheet.Cells[recordIndex, 2].Value = GetPropValue(item, "MedType");
                    workSheet.Cells[recordIndex, 3].Value = GetPropValue(item, "MedName");
                    workSheet.Cells[recordIndex, 4].Value = GetPropValue(item, "ImportDate");
                    workSheet.Cells[recordIndex, 4].Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                    workSheet.Cells[recordIndex, 5].Value = GetPropValue(item, "MedUnit");
                    workSheet.Cells[recordIndex, 6].Value = GetPropValue(item, "Quantity");
                    recordIndex++;
                }

                var modelTable = workSheet.Cells["A1:F" + (IMPViewModel.InventoryDataGridCache.Items.Count + 1)];

                // Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                modelTable.AutoFitColumns();

                excelPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                App.Current.ShowApplicationMessageBox("Đã xuất Excel thành công!\nTập tin excel đã xuất được lưu ở " + saveFileDialog.FileName,
                       HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                       HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                       OwnerWindow.MainScreen,
                       "Thông báo");
            }
        }
        public object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
