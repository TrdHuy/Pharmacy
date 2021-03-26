using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Windows.BaseWindow.Action.Builder;
using Pharmacy.Implement.Windows.LoginScreenWindow.Action.Type;

namespace Pharmacy.Implement.Windows.LoginScreenWindow.Action.Factory
{
    internal class LSW_CommandExecuterBuilder : BaseCommandExecuterBuilder
    {
        public override IViewModelCommandExecuter CreateViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null)
        {
            IViewModelCommandExecuter viewModelCommandExecuter;

            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_LSW_LOGIN_FEATURE:
                    viewModelCommandExecuter = new LSW_SystemLoginAction(keyTag, WindowTag.WINDOW_TAG_LOGIN_SCREEN, viewModel, logger);
                    break;
                case KeyFeatureTag.KEY_TAG_LSW_CUSTOMER_SERVICE_FEATURE:
                    viewModelCommandExecuter = null;
                    break;
                case KeyFeatureTag.KEY_TAG_LSW_BUG_REPORT_FEATURE:
                    viewModelCommandExecuter = null;
                    break;
                default:
                    viewModelCommandExecuter = null;
                    break;
            }
            return viewModelCommandExecuter;
        }
    }
}
