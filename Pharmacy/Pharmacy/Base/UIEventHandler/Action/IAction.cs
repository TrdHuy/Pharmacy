
using Pharmacy.Base.Utils;

namespace Pharmacy.Base.UIEventHandler.Action
{
    public interface IAction
    {
        /// <summary>
        /// Triển khai action cho 1 đối tượng  được định nghĩa trước
        /// </summary>
        bool Execute(object dataTransfer);

        /// <summary>
        /// ID of Action
        /// </summary>
        string ActionID { get; }
 
        /// <summary>
        /// Name of action
        /// </summary>
        string ActionName { get; }
        
        /// <summary>
        /// Builder id of Action
        /// </summary>
        string BuilderID { get; }

        /// <summary>
        /// action logger
        /// </summary>
        ILogger Logger { get; }
    }
}
