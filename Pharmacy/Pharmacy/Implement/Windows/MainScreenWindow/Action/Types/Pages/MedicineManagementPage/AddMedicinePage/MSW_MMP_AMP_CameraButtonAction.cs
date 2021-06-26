using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.AddMedicinePage
{
    internal class MSW_MMP_AMP_CameraButtonAction : MSW_MMP_AMP_ButtonAction
    {

        public MSW_MMP_AMP_CameraButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }


        protected override void ExecuteCommand()
        {
            if (!CanChooseNewImage())
            {
                return;
            }

            OpenFileDialog openDialog = FileIOUtil.OpenFile("File ảnh|*.bmp;*.jpg;*.jpeg;*.png", "", "Chọn ảnh thuốc!");
            var result = openDialog.ShowDialog();

            try
            {
                switch (result)
                {
                    case DialogResult.OK:
                        var file = openDialog.FileName;
                        Bitmap medicineBit = (Bitmap)Image.FromFile(file);
                        AMPViewModel.MedicineImageFileName = file;
                        AMPViewModel.MedicineImageSource = medicineBit.ToImageSource();
                        AMPViewModel.Invalidate("MedicineImageSource");
                        medicineBit.Dispose();
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
                return ;
            }


            return;
        }

        private bool CanChooseNewImage()
        {
            if (String.IsNullOrEmpty(AMPViewModel.MedicineID))
            {
                App.Current.ShowApplicationMessageBox("Vui lòng nhập mã thuốc trước!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Cảnh báo!");
                return false;
            }

            if (AMPViewModel.MedicineIDCheckingStatus != (int)MedicineIDCheckingStatusMessage.MedicineIDAccepted)
            {
                App.Current.ShowApplicationMessageBox("Vui lòng chờ trong lúc đánh giá tính khả dụng của mã thuốc!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo");
                return false;
            }

            return true;
        }
    }
}