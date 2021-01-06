using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.UserManagementPage.UserInstantiationPage
{
    public class MSW_UMP_UIP_CameraButtonAction : Base.UIEventHandler.Action.IAction
    {
        private UserInstantiationPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as UserInstantiationPageViewModel;

            if (!CanChooseNewImage())
            {
                return false;
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
                        FileIOUtil.SaveUserImageFile(_viewModel.UserNameText, userBit);
                        _viewModel.Invalidate("NewUser");
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
                return false;
            }
          

            return true;
        }

        private bool CanChooseNewImage()
        {
            if (String.IsNullOrEmpty(_viewModel.UserNameText))
            {
                App.Current.ShowApplicationMessageBox("Vui lòng nhập tên tài khoản trước!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
                return false;
            }

            if (!_viewModel.IsDoneEvaluateUserName)
            {
                App.Current.ShowApplicationMessageBox("Vui lòng chờ trong lúc đánh giá tính khả thi của tên tài khoản!",
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
