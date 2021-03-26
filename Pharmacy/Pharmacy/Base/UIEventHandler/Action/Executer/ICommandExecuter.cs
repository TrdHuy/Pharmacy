using Pharmacy.Base.Observable.ObserverPattern;
using System.Collections.Generic;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface ICommandExecuter : IAction
    {
        /// <summary>
        ///  Dữ liệu được truyền vào trong lệnh
        /// </summary>
        IList<object> DataTransfer { get; }

        /// <summary>
        /// Kiểm tra liệu lệnh này có thể thực thi hay không 
        /// </summary>
        bool CanExecute(object dataTransfer);

        /// <summary>
        /// Kiểm tra liệu lệnh này đã được thực thi thành công chưa 
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// Kiểm tra liệu lệnh này có bị hủy trong lúc đang thực thi hay không 
        /// </summary>
        bool IsCancled { get; }

        /// <summary>
        /// Set completed flag for some command, because when some ExecuteVM() was call
        /// it may be async method, so should let inherited child overide the flag
        /// by their own.
        /// </summary>
        void SetCompleteFlagAfterExecuteCommand();

        /// <summary>
        /// The main method for executer, everything need to be executed will happen here
        /// </summary>
        void ExecuteCommand();


        /// <summary>
        /// When command can't be executed, an other alternative command will be executed 
        /// </summary>
        void ExecuteAlternativeCommand();

        event NotifyIsCompletedChangedHandler IsCompletedChanged;
        event NotifyIsCanceledChangedHandler IsCanceledChanged;
    }

    public delegate void NotifyIsCompletedChangedHandler(object sender, ExecuterStatusArgs arg);
    public delegate void NotifyIsCanceledChangedHandler(object sender, ExecuterStatusArgs arg);

    public class ExecuterStatusArgs
    {
        public bool OldValue { get; set; }
        public bool NewValue { get; set; }

        public ExecuterStatusArgs(bool newVal, bool oldVal)
        {
            OldValue = oldVal;
            NewValue = newVal;
        }
    }
}
