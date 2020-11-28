using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.UIEventHandler.Action;
using Pharmacy.Implement.Windows.LoginScreenWindow.Action.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Action.Factory
{
    public class LSW_ActionFactory : KeyActionFactory
    {
        public override IAction CreateActionFromCurrentWindow(string keyTag)
        {
            IAction action;

            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_LSW_LOGIN_FEATURE:
                    action = new LSW_SystemLoginAction();
                    break;
                case KeyFeatureTag.KEY_TAG_LSW_CUSTOMER_SERVICE_FEATURE:
                    action = null;
                    break;
                case KeyFeatureTag.KEY_TAG_LSW_BUG_REPORT_FEATURE:
                    action = null;
                    break;
                default:
                    action = null;
                    break;
            }
            return action;
        }
    }
}
