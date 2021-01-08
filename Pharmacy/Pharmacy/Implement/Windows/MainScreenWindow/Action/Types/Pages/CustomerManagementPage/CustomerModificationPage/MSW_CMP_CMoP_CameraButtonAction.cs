using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.CustomerManagementPage.CustomerModificationPage
{
    public class MSW_CMP_CMoP_CameraButtonAction : Base.UIEventHandler.Action.IAction
    {
        private CustomerModificationPageViewModel _viewModel;

        public bool Execute(object[] dataTransfer)
        {
            _viewModel = dataTransfer[0] as CustomerModificationPageViewModel;

            OpenFileDialog openDialog = FileIOUtil.OpenFile("File ảnh|*.bmp;*.jpg;*.jpeg;*.png", "", "Chọn ảnh đại diện của bạn!");
            var result = openDialog.ShowDialog();

            try
            {
                switch (result)
                {
                    case DialogResult.OK:
                        var file = openDialog.FileName;
                        Bitmap cusBit = (Bitmap)Image.FromFile(file);
                        _viewModel.CustomerImageFileName = file;
                        _viewModel.CustomerImageSource = cusBit.ToImageSource();
                        cusBit.Dispose();
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

    }
}
