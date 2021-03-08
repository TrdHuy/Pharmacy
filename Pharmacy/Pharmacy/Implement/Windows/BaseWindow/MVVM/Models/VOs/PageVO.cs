using Pharmacy.Implement.Utils.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.BaseWindow.MVVM.Models.VOs
{
    public class PageVO
    {
        public Uri PageUri { get; set; }

        public long LoadingDelayTime { get; set; }
        
        public PageVO(Uri uri, long delayTime = 2000)
        {
            PageUri = uri;
            LoadingDelayTime = delayTime;
        }
    }
}
