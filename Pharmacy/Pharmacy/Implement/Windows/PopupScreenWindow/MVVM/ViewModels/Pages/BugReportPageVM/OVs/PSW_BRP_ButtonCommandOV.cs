using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.PSW_BasePageVM.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages.BugReportPageVM.OVs
{
    internal class PSW_BRP_ButtonCommandOV : PSW_ButtonCommandOV
    {
        private static Logger L = new Logger("PSW_BRP_ButtonCommandOV");

        protected override Logger logger => L;

        public CommandExecuterModel SendReportButtonCommand { get; set; }

        public PSW_BRP_ButtonCommandOV(BaseViewModel parentsModel) : base(parentsModel)
        {
            SendReportButtonCommand = new CommandExecuterModel((paramaters) =>
            {
                return OnKey(KeyFeatureTag.KEY_TAG_PSW_BRP_SEND_REPORT_BUTTON
                , paramaters);
            });
        }

    }
}