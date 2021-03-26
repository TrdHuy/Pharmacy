using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.ModifyMedicinePage
{
    internal class MSW_MMP_MMP_CameraButtonAction : MSW_MMP_MMP_ButtonAction
    {
        public MSW_MMP_MMP_CameraButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        public override void ExecuteCommand()
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
                        MMPViewModel.MedicineImageFileName = file;
                        MMPViewModel.MedicineImageSource = medicineBit.ToImageSource();
                        MMPViewModel.Invalidate("MedicineImageSource");
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
                return;
            }


            return;
        }

        private bool CanChooseNewImage()
        {
            if (String.IsNullOrEmpty(MMPViewModel.MedicineID))
            {
                App.Current.ShowApplicationMessageBox("Vui lòng nhập mã thuốc trước!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                return false;
            }

            return true;
        }
    }
}