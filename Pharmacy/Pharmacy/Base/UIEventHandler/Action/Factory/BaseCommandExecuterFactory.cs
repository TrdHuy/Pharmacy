using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Base.UIEventHandler.Action.Factory
{
    public class BaseCommandExecuterFactory : BaseActionFactory
    {
        public override IAction CreateAction(string builderID, string keyID, BaseViewModel viewModel = null, ILogger logger = null)
        {
            IAction action = base.CreateAction(builderID, keyID, viewModel, logger);

            if (action == null)
            {

                IActionBuilder builder = null;
                try
                {
                    builder = _builders[builderID];
                }
                catch
                {
                    return null;
                }

                if (builder is ICommandExecuterBuilder)
                {
                    var commandExecuterBuilder = builder as ICommandExecuterBuilder;

                    if (!commandExecuterBuilder.Locker.IsLock)
                    {
                        if (viewModel == null)
                        {
                            action = commandExecuterBuilder.BuildCommandExecuter(keyID, logger);
                        }
                        else
                        {
                            action = commandExecuterBuilder.BuildViewModelCommandExecuter(keyID, viewModel, logger);
                        }
                    }
                    else
                    {
                        if (viewModel == null)
                        {
                            action = commandExecuterBuilder.BuildAlternativeCommandExecuterWhenBuilderIsLock(keyID, logger);
                        }
                        else
                        {
                            action = commandExecuterBuilder.BuildAlternativeViewModelCommandExecuterWhenBuilderIsLock(keyID, viewModel, logger);
                        }
                    }
                }
            }
            return action;
        }
    }
}
