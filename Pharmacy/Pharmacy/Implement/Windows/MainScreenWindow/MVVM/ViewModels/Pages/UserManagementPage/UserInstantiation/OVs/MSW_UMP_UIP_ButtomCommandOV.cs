using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage.UserInstantiation.OVs
{
    internal class MSW_UMP_UIP_ButtomCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_UMP_UIP_ButtomCommandOV");
        private bool _isSaveButtonRunning;

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
        public CommandExecuterModel SaveButtonCommand { get; set; }
        public CommandExecuterModel CameraButtonCommand { get; set; }
        public CommandExecuterModel CancleButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_UMP_UIP_ButtomCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            SaveButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsSaveButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_SAVE_BUTTON
                , paramaters
                , new BuilderLocker(BuilderStatus.TaskHandling, true)) as ICommandExecuter;
            });
            CameraButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_CAMERA_BUTTON
                , paramaters);
            });
            CancleButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_UMP_UIP_CANCLE_BUTTON
                , paramaters);
            });
        }

    }
}

