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
        void OnKey(string windowTag, string keyFeature, object[] obj);
    }
}
