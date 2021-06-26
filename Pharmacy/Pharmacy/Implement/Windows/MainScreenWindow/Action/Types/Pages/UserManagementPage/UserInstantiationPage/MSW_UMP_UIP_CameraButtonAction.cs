using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
using System;
using System.Drawing;
using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using System.Windows.Forms;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserInstantiationPage
{
    internal class MSW_UMP_UIP_CameraButtonAction : MSW_UMP_UIP_ButtonAction
    {

        public MSW_UMP_UIP_CameraButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        protected override void ExecuteCommand()
        {
            if (!CanChooseNewImage())
            {
                return;
            }

            OpenFileDialog openDialog = FileIOUtil.OpenFile("File ảnh|*.bmp;*.jpg;*.jpeg;*.png", "", "Chọn ảnh đại diện của bạn!");
            var result = openDialog.ShowDialog();

            try
            {
                switch (result)
                {
                    case DialogResult.OK:
                        var file = openDialog.FileName;
                        Bitmap userBit = (Bitmap)Image.FromFile(file);
                        UIPViewModel.UserImageFileName = file;
                        UIPViewModel.UserImageSource = userBit.ToImageSource();
                        userBit.Dispose();
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
            if (String.IsNullOrEmpty(UIPViewModel.UserNameText))
            {
                App.Current.ShowApplicationMessageBox("Vui lòng nhập tên tài khoản trước!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo");
                return false;
            }

            if (!UIPViewModel.IsDoneEvaluateUserName)
            {
                App.Current.ShowApplicationMessageBox("Vui lòng chờ trong lúc đánh giá tính khả thi của tên tài khoản!",
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
