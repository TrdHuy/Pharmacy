using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.Utils;

namespace Pharmacy.Base.UIEventHandler.Action
{

    /// <summary>
    /// Destroyable command is uesed for the command that the execute method was async.
    /// When the method awaiting the result of some tasks, we can destroy the command
    /// and abandon those tasks
    /// </summary>
    public abstract class AbstractDestroyableViewModelCommandExecuter : AbstractCommandExecuter, IDestroyableViewModelCommandExecuter
    {
        public virtual BaseViewModel ViewModel { get; protected set; }

        public AbstractDestroyableViewModelCommandExecuter(string actionID, string builderID, BaseViewModel viewModel, ILogger logger) : base(actionID, builderID, logger)
        {
            this.ViewModel = viewModel;
        }

        public void OnDestroy()
        {
            if (!IsCompleted)
            {
                ExecuteOnDestroy();
            }
        }

        /// <summary>
        /// Set completed flag for some command, because when some ExecuteVM() was call
        /// it may be async method, so should let inherited child overide the flag
        /// by their own.
        /// 
        /// And the flag will be true as default.
        /// </summary>
        public override void SetCompleteFlagAfterExecuteCommand()
        {
            IsCompleted = true;
        }


        /// <summary>
        /// Check posibility of command with transfered data
        /// default = true
        /// </summary>
        /// <param name="dataTransfer">data passed into executer</param>
        /// <returns>true if meet condition and execute the command</returns>
        public override bool CanExecute(object dataTransfer)
        {
            return true;
        }

        protected virtual void ExecuteOnDestroy()
        {
            IsCancled = true;
        }

    }
}
