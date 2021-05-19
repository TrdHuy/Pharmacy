using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.Action.Types.Pages.BugReportPage
{
    internal class PSW_BRP_FinderVideoPathButtonAction : PSW_BRP_ButtonAction
    {
        private const long LIMITED_ATTACH_FILE_SIZE = 20000000;
        public PSW_BRP_FinderVideoPathButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger)
        {
        }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            OpenFileDialog openDialog = FileIOUtil.OpenFile("video, hình ảnh hoặc file nén|*.mp3;*.mp4;*.png;*.jpg;*.rar;*.zip", "", "Chọn video, hình ảnh!");


            openDialog.InitialDirectory = Logger.GetLogDirectory();
            var result = openDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(openDialog.FileName);
                if(fi.Length > LIMITED_ATTACH_FILE_SIZE)
                {
                    App.Current.ShowApplicationMessageBox("kích thước file không được vượt quá 20MB",
                        HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                        HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info);
                }
                else
                {
                    BRPViewModel.IssueInfoOV.VideoPathText = openDialog.FileName;
                }
            }
        }
    }
}
