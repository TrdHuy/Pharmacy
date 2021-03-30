using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.CustomerManagementPage.CustomerInstantiation.OVs
{
    internal class MSW_CMP_CIP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_CIP_ButtonCommandOV");

        protected override Logger logger => L;

        private bool _isSaveButtonRunning = false;

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

        public MSW_CMP_CIP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SaveButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsSaveButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_SAVE_BUTTON
                    , paramaters
                    , new BuilderLocker(BuilderStatus.TaskHandling, true)) as ICommandExecuter;
            });
            CameraButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CAMERA_BUTTON
                    , paramaters);
            });
            CancleButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CANCLE_BUTTON
                    , paramaters);
            });
        }
    }
}
