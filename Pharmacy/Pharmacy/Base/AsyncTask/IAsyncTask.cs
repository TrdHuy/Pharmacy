using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Base.AsyncTask
{
    public interface IAsyncTask
    {
        /// <summary>
        /// Thời gian delay (theo millisecon) khi thực hiện async action
        /// </summary
        long DelayTime { get; }

        /// <summary>
        /// Kết quả trả về sau khi triển khai async action
        /// </summary
        AsyncTaskResult Result { get; }

        /// <summary>
        /// Xác định xem liệu task này có thể triển khai được hay không
        /// </summary>
        Func<bool> CanExecute { get; }

        /// <summary>
        /// Triển khai task cho 1 đối tượng  được định nghĩa trước
        /// </summary>
        Task<AsyncTaskResult> Execute { get; }

        /// <summary>
        /// Xử lý call back sau khi async task được triển khai 
        /// </summary>
        Action<AsyncTaskResult> CallbackHandler { get; }
    }
}
