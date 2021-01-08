﻿using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Definitions;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.MedicineManagementPage.ModifyMedicinePage
{
    public class MSW_MMP_MMP_CameraButtonAction : Base.UIEventHandler.Action.IAction
    {
        private MSW_PageController _pageHost = MSW_PageController.Instance;
        private ModifyMedicinePageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as ModifyMedicinePageViewModel;

            if (!CanChooseNewImage())
            {
                return false;
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
                        _viewModel.MedicineImageFileName = file;
                        _viewModel.MedicineImageSource = medicineBit.ToImageSource();
                        _viewModel.Invalidate("MedicineImageSource");
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
                return false;
            }


            return true;
        }

        private bool CanChooseNewImage()
        {
            if (String.IsNullOrEmpty(_viewModel.MedicineID))
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