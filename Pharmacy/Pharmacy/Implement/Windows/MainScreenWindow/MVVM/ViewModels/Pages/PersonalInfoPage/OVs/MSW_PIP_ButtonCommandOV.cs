using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;


namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.PersonalInfoPage.OVs
{
    internal class MSW_PIP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_PIP_ButtonCommandOV");
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
        public CommandExecuterModel CancleButtonCommand { get; set; }
        public CommandExecuterModel CameraButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_PIP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            SaveButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsSaveButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_PIP_SAVE_BUTTON
                , paramaters
                , new BuilderLocker(BuilderStatus.TaskHandling, true)) as ICommandExecuter;
            });
            CancleButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_PIP_CANCLE_BUTTON
                , paramaters);
            });
            CameraButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_PIP_CAMERA_BUTTON
                , paramaters);
            });
        }

    }
}

