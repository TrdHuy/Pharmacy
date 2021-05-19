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
    internal class PSW_BRP_FinderLogPathButtonAction : PSW_BRP_ButtonAction
    {
        public PSW_BRP_FinderLogPathButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger)
        {
        }

        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();

            OpenFileDialog openDialog = FileIOUtil.OpenFile("File txt|*.txt", "", "Chọn log file!");
            openDialog.InitialDirectory = Logger.GetLogDirectory();
            var result = openDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                BRPViewModel.IssueInfoOV.LogPathText = openDialog.FileName;
            }
        }
    }
}
