using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using Pharmacy.Implement.UIEventHandler;
using Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Alternative;

namespace Pharmacy.Implement.Windows.BaseWindow.Action.Builder
{
    internal class BaseCommandExecuterBuilder : AbstractCommandExecuterBuilder
    {
        public override ICommandExecuter BuildAlternativeCommandExecuterWhenBuilderIsLock(string keyTag, ILogger logger = null)
        {
            return null;
        }

        public override IViewModelCommandExecuter BuildAlternativeViewModelCommandExecuterWhenBuilderIsLock(string keyTag, BaseViewModel viewModel, ILogger logger = null)
        {
            return null;
        }

        public override ICommandExecuter BuildCommandExecuter(string keyTag, ILogger logger = null)
        {
            ICommandExecuter commandExecuter;
            switch (keyTag)
            {
                case KeyFeatureTag.KEY_TAG_EXISTED_SAME_RUNNING_ACTION_BUTTON:
                    commandExecuter = new MSW_ActionIsAlreadyExistedAction(keyTag, WindowTag.WINDOW_TAG_BASE_WINDOW, logger);
                    break;
                default:
                    commandExecuter = null;
                    break;
            }
            return commandExecuter;
        }
        public override IViewModelCommandExecuter BuildViewModelCommandExecuter(string keyTag, BaseViewModel viewModel, ILogger logger = null)
        {
            return null;
        }
    }
}
