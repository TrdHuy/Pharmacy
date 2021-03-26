using Pharmacy.Base.Utils;
using System.Collections.Generic;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public abstract class AbstractCommandExecuter : ICommandExecuter
    {
        private bool _isCompleted = false;
        private bool _isCancled = false;
        private List<object> _dataTransfer;
        private string _actionID;
        private string _builderID;

        public event NotifyIsCanceledChangedHandler IsCanceledChanged;
        public event NotifyIsCompletedChangedHandler IsCompletedChanged;

        protected ILogger Log { get; set; }

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
                    IsCompletedChanged(this, new ExecuterStatusArgs(value, oldValue));
            }
        }

        public bool IsCancled
        {
            get => _isCancled;
            protected set
            {
                var oldValue = _isCancled;
                _isCancled = value;
                if (_isCancled)
                {
                    ClearCache();
                }
                if (oldValue != value)
                    IsCanceledChanged(this, new ExecuterStatusArgs(value, oldValue));

            }
        }

        public IList<object> DataTransfer { get => _dataTransfer; }

        public string ActionID { get => _actionID; }
        public string BuilderID { get => _builderID; }

        public AbstractCommandExecuter(string actionID, string builderID, ILogger logger)
        {
            this.Log = logger;
            _actionID = actionID;
            _builderID = builderID;
        }

        public bool Execute(object dataTransfer)
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

            if (CanExecute(dataTransfer))
            {
                //Execute the command
                ExecuteCommand();
                SetCompleteFlagAfterExecuteCommand();
                return true;
            }
            else
            {
                //Execute alternative command
                ExecuteAlternativeCommand();
                SetCompleteFlagAfterExecuteCommand();
                return false;
            }
        }

        /// <summary>
        /// Clear the data transfer
        /// </summary>
        private void ClearCache()
        {
            _dataTransfer.Clear();
            _dataTransfer = null;
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
        public abstract void ExecuteCommand();


        /// <summary>
        /// Check posibility of command with transfered data
        /// </summary>
        /// <param name="dataTransfer">data passed into executer</param>
        /// <returns>true if meet condition and execute the command</returns>
        public abstract bool CanExecute(object dataTransfer);


        /// <summary>
        /// When command can't be executed, an other alternative command will be executed 
        /// </summary>
        public abstract void ExecuteAlternativeCommand();

    }
}
