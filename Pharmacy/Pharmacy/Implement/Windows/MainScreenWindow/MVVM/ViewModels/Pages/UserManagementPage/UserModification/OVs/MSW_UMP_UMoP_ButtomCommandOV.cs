using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.UserManagementPage.UserModification.OVs
{
    internal class MSW_UMP_UMoP_ButtomCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_UMP_UMoP_ButtomCommandOV");
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
                    _keyActionListener.LockMSW_ActionFactory(false, FactoryStatus.Unlock);
                }
                InvalidateOwn();
            }
        }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CancleButtonCommand { get; set; }
        public RunInputCommand CameraButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_UMP_UMoP_ButtomCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            SaveButtonCommand = new RunInputCommand((paramaters) =>
            {
                IsSaveButtonRunning = true;
                OnKey(KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_SAVE_BUTTON
                , paramaters
                , new FactoryLocker(FactoryStatus.TaskHandling, true));
            });
            CameraButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CAMERA_BUTTON
                , paramaters);
            });
            CancleButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_UMP_UMoP_CANCLE_BUTTON
                , paramaters);
            });
        }

    }
}

