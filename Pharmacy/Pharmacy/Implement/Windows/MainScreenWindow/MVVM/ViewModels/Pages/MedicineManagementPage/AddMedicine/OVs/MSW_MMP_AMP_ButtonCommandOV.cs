using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage.AddMedicine.OVs
{
    internal class MSW_MMP_AMP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_MMP_AMP_ButtonCommandOV");
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

        public CommandExecuterModel CancelButtonCommand { get; set; }
        public CommandExecuterModel SaveButtonCommand { get; set; }
        public CommandExecuterModel CameraButtonCommand { get; set; }

        protected override Logger logger => L;
        public MSW_MMP_AMP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SaveButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsSaveButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_SAVE_BUTTON
                    , paramaters
                    , new BuilderLocker(BuilderStatus.TaskHandling, true)) as ICommandExecuter;
            });
            CameraButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CAMERA_BUTTON
                    , paramaters);
            });
            CancelButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CANCEL_BUTTON
                    , paramaters);
            });
        }

    }
}
