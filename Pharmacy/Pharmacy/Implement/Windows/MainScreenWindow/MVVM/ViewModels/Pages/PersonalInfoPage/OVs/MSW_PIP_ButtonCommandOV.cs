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
                    _keyActionListener.LockMSW_ActionFactory(false, FactoryStatus.Unlock);
                }
                InvalidateOwn();
            }
        }

        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CancleButtonCommand { get; set; }
        public RunInputCommand CameraButtonCommand { get; set; }

        protected override Logger logger => L;

        public MSW_PIP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            SaveButtonCommand = new RunInputCommand((paramaters) =>
            {
                IsSaveButtonRunning = true;
                OnKey(KeyFeatureTag.KEY_TAG_MSW_PIP_SAVE_BUTTON
                , paramaters
                , new FactoryLocker(FactoryStatus.TaskHandling, true));
            });
            CancleButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_PIP_CANCLE_BUTTON
                , paramaters);
            });
            CameraButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_PIP_CAMERA_BUTTON
                , paramaters);
            });
        }

    }
}

