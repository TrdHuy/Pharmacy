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
                    _keyActionListener.LockMSW_ActionFactory(false, FactoryStatus.Unlock);
                }
                InvalidateOwn();
            }
        }

        public RunInputCommand CancelButtonCommand { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CameraButtonCommand { get; set; }

        protected override Logger logger => L;
        public MSW_MMP_AMP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SaveButtonCommand = new RunInputCommand((paramaters) =>
            {
                IsSaveButtonRunning = true;
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_SAVE_BUTTON
                    , paramaters
                    , new FactoryLocker(FactoryStatus.TaskHandling, true));
            });
            CameraButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CAMERA_BUTTON
                    , paramaters);
            });
            CancelButtonCommand = new RunInputCommand((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_MMP_AMP_CANCEL_BUTTON
                    , paramaters);
            });
        }

    }
}
