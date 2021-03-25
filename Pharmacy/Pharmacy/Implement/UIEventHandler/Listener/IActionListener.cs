using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Base.UIEventHandler.Action;
using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pharmacy.Base.UIEventHandler.Listener
{
    interface IActionListener
    {
        /// <summary>
        /// Thực hiện hành động click vao 1 button
        /// </summary>
        /// <typeparam name="windowTag">là chuỗi key để xác định window nào đang gọi</typeparam>
        /// <typeparam name="keyFeature">là chuỗi key để xác định đó là feature gì</typeparam>
        /// <typeparam name="obj">data transfer giữa các class</typeparam>
        void OnKey(string windowTag, string keyFeature, object obj);

        /// <summary>
        /// Thực hiện hành động click vao 1 button
        /// </summary>
        /// <typeparam name="windowTag">là chuỗi key để xác định window nào đang gọi</typeparam>
        /// <typeparam name="keyFeature">là chuỗi key để xác định đó là feature gì</typeparam>
        /// <typeparam name="obj">data transfer giữa các class</typeparam>
        /// <typeparam name="locker">khóa factory sau khi tạo action</typeparam>
        void OnKey(string windowTag, string keyFeature, object obj, FactoryLocker locker);

        /// <summary>
        /// Thực hiện hành động click vao 1 button
        /// </summary>
        /// <typeparam name="viewModel">view model đang gọi onkey</typeparam>
        /// <typeparam name="windowTag">là chuỗi key để xác định window nào đang gọi</typeparam>
        /// <typeparam name="logger">ghi log</typeparam>
        /// <typeparam name="keyFeature">là chuỗi key để xác định đó là feature gì</typeparam>
        /// <typeparam name="obj">data transfer giữa các class</typeparam>
        void OnKey(BaseViewModel viewModel, ILogger logger, string windowTag, string keyFeature, object obj);

        /// <summary>
        /// Thực hiện hành động click vao 1 button
        /// </summary>
        /// <typeparam name="viewModel">view model đang gọi onkey</typeparam>
        /// <typeparam name="logger">ghi log</typeparam>
        /// <typeparam name="obj">data transfer giữa các class</typeparam>
        /// <typeparam name="locker">khóa factory sau khi tạo action</typeparam>
        void OnKey(BaseViewModel viewModel, ILogger logger, string windowTag, string keyFeature, object obj, FactoryLocker locker);

        /// <summary>
        /// Thực hiện khi 1 button bị hủy lệnh giữa chừng
        /// </summary>
        /// <typeparam name="windowTag">là chuỗi key để xác định window nào đang gọi</typeparam>
        /// <typeparam name="keyFeature">là chuỗi key để xác định đó là feature gì</typeparam>
        void OnKeyDestroy(string windowTag, string keyFeature);
    }
}
