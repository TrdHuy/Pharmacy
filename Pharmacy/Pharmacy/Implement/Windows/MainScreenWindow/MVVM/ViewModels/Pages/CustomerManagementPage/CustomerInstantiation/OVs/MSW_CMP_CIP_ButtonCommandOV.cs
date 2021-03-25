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
                    _keyActionListener.LockMSW_ActionFactory(false, FactoryStatus.Unlock);
                }
                InvalidateOwn();
            }
        }

        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CameraButtonCommand { get; set; }
        public RunInputCommand CancleButtonCommand { get; set; }

        public MSW_CMP_CIP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SaveButtonCommand = new RunInputCommand((paramaters) =>
            {
                IsSaveButtonRunning = true;
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_SAVE_BUTTON
                    , paramaters
                    , new FactoryLocker(FactoryStatus.TaskHandling, true));
            });
            CameraButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CAMERA_BUTTON
                    , paramaters);
            });
            CancleButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_CMP_CIP_CANCLE_BUTTON
                    , paramaters);
            });
        }
    }
}
