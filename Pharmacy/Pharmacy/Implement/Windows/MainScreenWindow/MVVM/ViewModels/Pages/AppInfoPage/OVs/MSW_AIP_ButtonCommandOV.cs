using Pharmacy.Base.MVVM.ViewModels;
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

        protected override Logger logger => L;

        public CommandModel AppUpdateButtonCommand { get; set; }
        public CommandModel CustomerSupportButtonCommand { get; set; }
        public CommandModel BugReportButtonCommand { get; set; }
        public CommandModel HpssHomePageButtonCommand { get; set; }

        public MSW_AIP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            AppUpdateButtonCommand = new CommandModel((paramaters) =>
            {
                OnKey(KeyFeatureTag.KEY_TAG_MSW_AIP_APP_UPDATE_BUTTON
                    , paramaters);
            }
            , () =>
            {
                OnKeyDestroy(WindowTag.WINDOW_TAG_MAIN_SCREEN, KeyFeatureTag.KEY_TAG_MSW_AIP_APP_UPDATE_BUTTON);
            });


        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            AppUpdateButtonCommand.OnDestroy();
        }

    }
}
