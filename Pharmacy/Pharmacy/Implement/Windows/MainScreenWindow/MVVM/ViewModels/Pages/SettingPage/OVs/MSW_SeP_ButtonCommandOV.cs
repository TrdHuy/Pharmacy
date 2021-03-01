using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SettingPage.OVs
{
    public class MSW_SeP_ButtonCommandOV : BaseViewModel
    {
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private bool _isSaveButtonRunning;

        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand CancleButtonCommand { get; set; }
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
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }

        public MSW_SeP_ButtonCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            CancleButtonCommand = new RunInputCommand(CancleButtonClickEvent);
        }

        private void SaveButtonClickEvent(object paramaters)
        {
            IsSaveButtonRunning = true;
            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_SeP_SAVE_BUTTON
                , dataTransfer
                , new FactoryLocker(LockReason.TaskHandling, true));
        }

        private void CancleButtonClickEvent(object obj)
        {
        }
    }
}
