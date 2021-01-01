using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler.Listener;
using Pharmacy.Implement.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative
{
    public class MSW_AlternativeAction : IAction
    {
        public bool Execute(object[] dataTransfer)
        {
            App.Current.ShowApplicationMessageBox(KeyActionListener.Instance.GetMSWFactoryLockReason().GetStringValue(),
                HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Hand,
                OwnerWindow.MainScreen,
                "Thông báo!");
            return true;
        }
    }
}
