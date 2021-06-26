using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.IO.Compression;
using Microsoft.Win32;
using Pharmacy.Base.Utils;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.SettingPage
{
    internal class MSW_SeP_BackupDBButtonAction : MSW_SeP_ButtonAction
    {
        public MSW_SeP_BackupDBButtonAction(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, viewModel, logger) { }

        private SaveFileDialog _dialog;

        protected override void ExecuteCommand()
        {
            _dialog = new SaveFileDialog();
            _dialog.Title = "Lưu tập tin sao lưu";
            _dialog.FileName = "MedicineManagement_Backup_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
            _dialog.Filter = "Zip Archive|*.zip";
            _dialog.RestoreDirectory = true;

            if (_dialog.ShowDialog() == true)
            {
                try
                {
                    System.Data.SqlClient.SqlConnection.ClearAllPools();
                    ZipFile.CreateFromDirectory(Environment.CurrentDirectory, _dialog.FileName);
                    App.Current.ShowApplicationMessageBox("Sao lưu hoàn tất!",
                 HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                 HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                 OwnerWindow.MainScreen,
                 "Thông báo");
                }
                catch (Exception ex)
                {
                    App.Current.ShowApplicationMessageBox(ex.Message,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                   HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Error,
                   OwnerWindow.MainScreen,
                   "Lỗi!");
                }
            }
            SPViewModel.ButtonCommandOV.IsBackupDBButtonRunning = false;
        }
    }
}
