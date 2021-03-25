using Pharmacy.Base.Utils;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public abstract class AbstractCommandExecuter : ICommandExecuter
    {
        private bool _isCompleted = false;
        private bool _isCancled = false;

        protected ILogger Log { get; set; }

        public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }
        public bool IsCancled { get => _isCancled; set => _isCancled = value; }

        public AbstractCommandExecuter(ILogger logger)
        {
            this.Log = logger;
        }

        public bool Execute(object dataTransfer)
        {
            if (CanExecute(dataTransfer))
            {
                ExecuteCommand(dataTransfer);
                SetCompleteFlagAfterExecuteCommand();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Set completed flag for some command, because when some ExecuteVM() was call
        /// it may be async method, so should let inherited child overide the flag
        /// by their own.
        /// 
        /// And the flag will be true as default.
        /// </summary>
        public abstract void SetCompleteFlagAfterExecuteCommand();


        /// <summary>
        /// The main method for executer, everything need to be executed will happen here
        /// </summary>
        /// <param name="dataTransfer"> data passed into executer</param>
        public abstract void ExecuteCommand(object dataTransfer);

        /// <summary>
        /// Check posibility of command with transfered data
        /// </summary>
        /// <param name="dataTransfer">data passed into executer</param>
        /// <returns>true if meet condition and execute the command</returns>
        public abstract bool CanExecute(object dataTransfer);

    }
}
