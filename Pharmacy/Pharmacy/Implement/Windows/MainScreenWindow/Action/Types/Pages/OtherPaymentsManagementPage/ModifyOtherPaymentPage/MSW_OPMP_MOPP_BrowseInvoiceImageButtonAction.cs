using Pharmacy.Implement.Utils;
using System;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Forms;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.OtherPaymentsManagementPage.ModifyOtherPaymentPage
{
    internal class MSW_OPMP_MOPP_BrowseInvoiceImageButtonAction : MSW_OPMP_MOPP_ButtonAction
    {
        public MSW_OPMP_MOPP_BrowseInvoiceImageButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {

            OpenFileDialog openDialog = FileIOUtil.OpenFile("File ảnh|*.bmp;*.jpg;*.jpeg;*.png", "", "Chọn ảnh hóa đơn!");
            var result = openDialog.ShowDialog();

            try
            {
                switch (result)
                {
                    case DialogResult.OK:
                        var file = openDialog.FileName;
                        MOPPViewModel.InvoiceImageURL = file;
                        MOPPViewModel.Invalidate("InvoiceImageURL");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                App.Current.ShowApplicationMessageBox(e.Message,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                    OwnerWindow.MainScreen,
                    "Lỗi!");
                return;
            }

            return;
        }
    }
}