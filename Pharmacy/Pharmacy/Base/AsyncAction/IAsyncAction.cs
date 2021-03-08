using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.AsyncAction
{
    public interface IAsyncAction
    {
        /// <summary>
        /// Thời gian delay (theo millisecon) khi thực hiện async action
        /// </summary
        long DelayTime { get; }

        /// <summary>
        /// Tham số để triển khai cho async action
        /// </summary
        object[] Paramaters { get; }

        /// <summary>
        /// Kết quả trả về sau khi triển khai async action
        /// </summary
        AsyncActionResult Result { get; }

        /// <summary>
        /// Xác định xem liệu action này có thể triển khai được hay không
        /// </summary>
        bool CanExecute();

        /// <summary>
        /// Triển khai action cho 1 đối tượng  được định nghĩa trước
        /// </summary>
        bool Execute();

        /// <summary>
        /// Xử lý call back sau khi async action được triển khai 
        /// </summary>
        bool CallbackHandler();
    }
}
