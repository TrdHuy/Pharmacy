using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.Extensions;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.PersonalInfoPage
{
    internal class MSW_PIP_CameraButtonAction : MSW_PIP_ButtonAction
    {
        public MSW_PIP_CameraButtonAction(BaseViewModel viewModel, ILogger logger) : base(viewModel, logger) { }

        public override void ExecuteCommand(object dataTransfer)
        {
            base.ExecuteCommand(dataTransfer);

            OpenFileDialog openDialog = FileIOUtil.OpenFile("File ảnh|*.bmp;*.jpg;*.jpeg;*.png", "", "Chọn ảnh đại diện của bạn!");
            var result = openDialog.ShowDialog();

            try
            {
                switch (result)
                {
                    case DialogResult.OK:
                        var file = openDialog.FileName;
                        Bitmap userBit = (Bitmap)Image.FromFile(file);
                        PIPViewModel.UserImageFileName = file;
                        PIPViewModel.UserImageSource = userBit.ToImageSource();
                        userBit.Dispose();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            return;
        }
    }
}
