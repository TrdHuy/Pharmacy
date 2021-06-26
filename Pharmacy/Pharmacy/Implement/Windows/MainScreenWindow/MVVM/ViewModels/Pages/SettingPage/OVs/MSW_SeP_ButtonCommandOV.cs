using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SettingPage.OVs
{
    internal class MSW_SeP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_SeP_ButtonCommandOV");
        private bool _isSaveButtonRunning;
        private bool _isBackupDBButtonRunning;

        public CommandExecuterModel SaveButtonCommand { get; set; }
        public CommandExecuterModel CancleButtonCommand { get; set; }
        public CommandExecuterModel BackupDBButtonCommand { get; set; }
        public bool IsSaveButtonRunning
        {
            get
            {

                return _isSaveButtonRunning;
            }
            set
            {
                _isSaveButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, BuilderStatus.Unlock);
                }
                InvalidateOwn();
            }
        }
        public bool IsBackupDBButtonRunning
        {
            get
            {

                return _isBackupDBButtonRunning;
            }
            set
            {
                _isBackupDBButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, BuilderStatus.Unlock);
                }
                InvalidateOwn();
            }
        }

        protected override Logger logger => L;

        public MSW_SeP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            SaveButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsSaveButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_SeP_SAVE_BUTTON
                , paramaters
                , new BuilderLocker(BuilderStatus.TaskHandling, true)) as ICommandExecuter;
            });
            CancleButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_SeP_CANCLE_BUTTON
                , paramaters);
            });
            BackupDBButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsBackupDBButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_SeP_BACKUP_DB_BUTTON
                , paramaters
                , new BuilderLocker(BuilderStatus.TaskHandling, true)) as ICommandExecuter;
            });
        }

    }
}
