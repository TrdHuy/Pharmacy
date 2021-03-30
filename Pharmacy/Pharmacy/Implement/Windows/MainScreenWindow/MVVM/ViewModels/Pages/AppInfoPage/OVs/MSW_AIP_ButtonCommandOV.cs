using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.AppInfoPage.OVs
{
    internal class MSW_AIP_ButtonCommandOV : MSW_ButtonCommandOV
    {
        private static Logger L = new Logger("MSW_AIP_ButtonCommandOV");

        private bool _isAppUpdateButtonRunning = false;
        private bool _isContactUsButtonRunning = false;

        protected override Logger logger => L;
        public bool IsAppUpdateButtonRunning
        {
            get
            {
                return _isAppUpdateButtonRunning;
            }
            set
            {
                _isAppUpdateButtonRunning = value;
                if (ParentsModel != null && ParentsModel is AppInfoPageViewModel)
                {
                    ((AppInfoPageViewModel)ParentsModel).UpdateFlagActionRunning();
                }
            }
        }
        public bool IsContatUsButtonRunning
        {
            get
            {
                return _isContactUsButtonRunning;
            }
            set
            {
                _isContactUsButtonRunning = value;
                if (ParentsModel != null && ParentsModel is AppInfoPageViewModel)
                {
                    ((AppInfoPageViewModel)ParentsModel).UpdateFlagActionRunning();
                }
            }
        }
        public bool IsBugReportButtonRunning { get; set; }
        public bool IsHpssHomePageButtonRunning { get; set; }

        public CommandExecuterModel AppUpdateButtonCommand { get; set; }
        public CommandExecuterModel ContactUsButtonCommand { get; set; }
        public CommandExecuterModel BugReportButtonCommand { get; set; }
        public CommandExecuterModel HpssHomePageButtonCommand { get; set; }

        public MSW_AIP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            AppUpdateButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsAppUpdateButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_AIP_APP_UPDATE_BUTTON
                    , paramaters);
            });
            ContactUsButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                IsContatUsButtonRunning = true;
                return OnKey(KeyFeatureTag.KEY_TAG_MSW_AIP_CONTACT_US_BUTTON
                    , paramaters);
            });

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            AppUpdateButtonCommand.OnDestroy();
        }

    }
}
