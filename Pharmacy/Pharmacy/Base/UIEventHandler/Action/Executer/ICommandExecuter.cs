using Pharmacy.Base.Utils;
using System.Collections.Generic;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface ICommandExecuter : IAction, IDestroyable
    {
        /// <summary>
        ///  Dữ liệu được truyền vào trong lệnh
        /// </summary>
        IList<object> DataTransfer { get; }

        /// <summary>
        /// Kiểm tra liệu lệnh này đã được thực thi thành công chưa 
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// Kiểm tra liệu lệnh này có bị hủy trong lúc đang thực thi hay không 
        /// </summary>
        bool IsCancled { get; }

        /// <summary>
        /// Triển khai action thay thế cho 1 đối tượng  được định nghĩa trước
        /// </summary>
        /// <returns></returns>
        bool AlterExecute(object dataTransfer);

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
