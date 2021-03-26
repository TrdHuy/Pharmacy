using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Base.UIEventHandler.Action.Factory
{
    public class BaseCommandExecuterFactory : BaseActionFactory
    {
        private bool IsNeedToBuildDestroyableViewModelAction { get; set; }
        public void TurnDestroyableActionBuilder(bool on)
        {
            IsNeedToBuildDestroyableViewModelAction = on;
        }

        /// <summary>
        /// When call command executer CreateAction, should turn on/off the Destroyable 
        /// view model flag to get exactly type of action.
        /// Note that: default the flag is false
        /// </summary>
        /// <param name="builderID"></param>
        /// <param name="keyID"></param>
        /// <param name="viewModel"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
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
                            action = commandExecuterBuilder.CreateCommandExecuter(keyID, logger);
                        }
                        else
                        {
                            if (!IsNeedToBuildDestroyableViewModelAction)
                            {
                                action = commandExecuterBuilder.CreateViewModelCommandExecuter(keyID, viewModel, logger);
                            }
                            else
                            {
                                action = commandExecuterBuilder.CreateDestroyableViewModelCommandExecuter(keyID, viewModel, logger);
                            }

                            // set the flag to default value
                            IsNeedToBuildDestroyableViewModelAction = false;
                        }
                    }
                    else
                    {
                        if (viewModel == null)
                        {
                            action = commandExecuterBuilder.CreateAlternativeCommandExecuterWhenBuilderIsLock(keyID, logger);
                        }
                        else
                        {
                            if (!IsNeedToBuildDestroyableViewModelAction)
                            {
                                action = commandExecuterBuilder.CreateAlternativeViewModelCommandExecuterWhenBuilderIsLock(keyID, viewModel, logger);
                            }
                            else
                            {
                                action = commandExecuterBuilder.CreateAlternativeDestroyableViewModelCommandExecuterWhenBuilderIsLock(keyID, viewModel, logger);
                            }

                            // set the flag to default value
                            IsNeedToBuildDestroyableViewModelAction = false;
                        }
                    }
                }
            }

            return action;
        }
    }
}
