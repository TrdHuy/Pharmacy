using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
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

        protected override IAction CreateActionFromCurrentWindow(string keyTag)
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

        protected override IAction CreateActionFromCurrentWindow(BaseViewModel viewModel, ILogger logger, string keyTag)
        {
            return null;
        }

        protected override IAction CreateAlternativeActionFromCurrentWindow(string keyTag)
        {
            return null;
        }

        protected override IAction CreateAlternativeActionFromCurrentWindow(BaseViewModel viewModel, ILogger logger, string keyTag)
        {
            return null;
        }
    }
}
