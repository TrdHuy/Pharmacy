using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Config;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage.OVs
{
    public class MSW_RP_ButtonCommandOV : BaseViewModel
    {
        private KeyActionListener _keyActionListener = KeyActionListener.Instance;
        private bool _isStartStatisticButtonRunning;

        public bool IsStartStatisticButtonRunning
        {
            get
            {

                return _isStartStatisticButtonRunning;
            }
            set
            {
                _isStartStatisticButtonRunning = value;
                if (!value)
                {
                    _keyActionListener.LockMSW_ActionFactory(false, LockReason.Unlock);
                }
                InvalidateOwn();
            }
        }

        public RunInputCommand StartStatisticButtonCommand { get; set; }
        public RunInputCommand SaveButtonCommand { get; set; }
        public RunInputCommand PrintButtonCommand { get; set; }
        public RunInputCommand ReportButtonCommand { get; set; }
        public RunInputCommand BackButtonCommand { get; set; }
        public RunInputCommand ClearButtonCommand { get; set; }

        public MSW_RP_ButtonCommandOV(BaseViewModel parentModel) : base(parentModel)
        {
            StartStatisticButtonCommand = new RunInputCommand(StartStatisticButtonClickEvent);
            SaveButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            PrintButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            ReportButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            BackButtonCommand = new RunInputCommand(SaveButtonClickEvent);
            ClearButtonCommand = new RunInputCommand(SaveButtonClickEvent);
        }

        private void SaveButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;
            _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                , KeyFeatureTag.KEY_TAG_MSW_NOT_IMPLEMENTED_BUTTON
                , dataTransfer);
        }

        private void StartStatisticButtonClickEvent(object paramaters)
        {
            object[] dataTransfer = new object[2];
            dataTransfer[0] = ParentsModel;
            dataTransfer[1] = paramaters;

            if (RUNE.IS_SUPPORT_STATISTICAL_CHART)
            {
                IsStartStatisticButtonRunning = true;
                _keyActionListener.OnKey(WindowTag.WINDOW_TAG_MAIN_SCREEN
                   , KeyFeatureTag.KEY_TAG_MSW_RP_START_STATISTIC_BUTTON
                   , dataTransfer
                   , new FactoryLocker(LockReason.TaskHandling, true));
            }
        }
    }
}
