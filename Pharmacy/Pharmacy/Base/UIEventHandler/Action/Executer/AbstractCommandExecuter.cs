using Pharmacy.Base.Utils;
using System.Collections.Generic;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public abstract class AbstractCommandExecuter : ICommandExecuter
    {
        private bool _isCompleted = false;
        private bool _isCanceled = false;
        private List<object> _dataTransfer;
        private string _actionID;
        private string _builderID;

        public event NotifyIsCanceledChangedHandler IsCanceledChanged;
        public event NotifyIsCompletedChangedHandler IsCompletedChanged;

        public ILogger Logger { get; private set; }

        public bool IsCompleted
        {
            get => _isCompleted;
            protected set
            {
                var oldValue = _isCompleted;
                _isCompleted = value;
                if (_isCompleted)
                {
                    ClearCache();
                }
                if (oldValue != value)
                    IsCompletedChanged?.Invoke(this, new ExecuterStatusArgs(value, oldValue));
            }
        }

        public bool IsCanceled
        {
            get => _isCanceled;
            protected set
            {
                var oldValue = _isCanceled;
                _isCanceled = value;
                if (_isCanceled)
                {
                    ClearCache();
                }
                if (oldValue != value)
                    IsCanceledChanged?.Invoke(this, new ExecuterStatusArgs(value, oldValue));

            }
        }

        public IList<object> DataTransfer { get => _dataTransfer; }

        public string ActionID { get => _actionID; }
        public string BuilderID { get => _builderID; }

        public AbstractCommandExecuter(string actionID, string builderID, ILogger logger)
        {
            this.Logger = logger;
            _actionID = actionID;
            _builderID = builderID;
        }

        public void OnDestroy()
        {
            ClearCache();
            ExecuteOnDestroy();
        }

        public void OnCancel()
        {
            if (!IsCompleted)
            {
                IsCanceled = true;
                ExecuteOnCancel();
            }
        }

        public bool Execute(object dataTransfer)
        {
            AssignDataTransfer(dataTransfer);

            if (CanExecute(dataTransfer))
            {
                //Execute the command
                ExecuteCommand();
                SetCompleteFlagAfterExecuteCommand();
                return true;
            }
            return false;
        }

        private void AssignDataTransfer(object dataTransfer)
        {
            //Assign data to Cache
            _dataTransfer = new List<object>();
            var cast = dataTransfer as object[];
            if (cast != null)
            {
                foreach (object data in cast)
                {
                    _dataTransfer.Add(data);
                }
            }
            else if (dataTransfer != null)
            {
                _dataTransfer.Add(dataTransfer);
            }
        }

        public bool AlterExecute(object dataTransfer)
        {
            AssignDataTransfer(dataTransfer);

            if (CanExecute(dataTransfer))
            {
                //Execute alternative command
                ExecuteAlternativeCommand();
                SetCompleteFlagAfterExecuteCommand();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Clear the data transfer
        /// </summary>
        private void ClearCache()
        {
            if (_dataTransfer != null)
            {
                _dataTransfer.Clear();
                _dataTransfer = null;
            }
        }


        /// <summary>
        /// Set completed flag for some command, because when some ExecuteVM() was call
        /// it may be async method, so should let inherited child overide the flag
        /// by their own.
        /// 
        /// And the flag will be true as default.
        /// </summary>
        protected abstract void SetCompleteFlagAfterExecuteCommand();


        /// <summary>
        /// The main method for executer, everything need to be executed will happen here
        /// </summary>
        protected abstract void ExecuteCommand();


        /// <summary>
        /// Check posibility of command with transfered data
        /// </summary>
        /// <param name="dataTransfer">data passed into executer</param>
        /// <returns>true if meet condition and execute the command</returns>
        protected abstract bool CanExecute(object dataTransfer);


        /// <summary>
        /// The alternative method for executer, everything need to be executed will happen here
        /// </summary>
        protected abstract void ExecuteAlternativeCommand();


        /// <summary>
        /// Destroy a command executer, normally will clear the cache
        /// </summary>
        protected abstract void ExecuteOnDestroy();


        /// <summary>
        /// Cancel a command executer while it is running 
        /// </summary>
        protected abstract void ExecuteOnCancel();
    }
}
