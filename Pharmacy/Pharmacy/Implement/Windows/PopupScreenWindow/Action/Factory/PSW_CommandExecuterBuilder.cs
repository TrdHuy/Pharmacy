using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Windows.BaseWindow.Action.Builder;
using Pharmacy.Implement.Windows.PopupScreenWindow.Action.Types.Pages.BugReportPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.Action.Factory
{
    internal class PSW_CommandExecuterBuilder : BaseCommandExecuterBuilder
    {
        public override IViewModelCommandExecuter BuildViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null)
        {
            IViewModelCommandExecuter viewModelCommandExecuter = base.BuildViewModelCommandExecuter(keyTag,viewModel,logger);
            
            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_PSW_BRP_SEND_REPORT_BUTTON:
                    viewModelCommandExecuter = new PSW_BRP_SendReportAction(keyTag, WindowTag.WINDOW_TAG_MAIN_SCREEN, viewModel, logger);
                    break;
                default:
                    break;
            }
            return viewModelCommandExecuter;
        }
    }
}
