
namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface ICommandExecuter : IAction
    {
        /// <summary>
        /// Kiểm tra liệu lệnh này có thể thực thi hay không 
        /// </summary>
        bool CanExecute(object dataTransfer);

        /// <summary>
        /// Kiểm tra liệu lệnh này đã được thực thi thành công chưa 
        /// </summary>
        bool IsCompleted { get; set; }

        /// <summary>
        /// Kiểm tra liệu lệnh này có bị hủy trong lúc đang thực thi hay không 
        /// </summary>
        bool IsCancled { get; set; }

        /// <summary>
        /// Set completed flag for some command, because when some ExecuteVM() was call
        /// it may be async method, so should let inherited child overide the flag
        /// by their own.
        /// </summary>
        void SetCompleteFlagAfterExecuteCommand();

        /// <summary>
        /// The main method for executer, everything need to be executed will happen here
        /// </summary>
        /// <param name="dataTransfer"> data passed into executer</param>
        void ExecuteCommand(object dataTransfer);
    }
}
