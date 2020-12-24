using Pharmacy.Implement.Utils.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model
{
    public class PageOV
    {
        public Uri PageUri { get; set; }

        public long LoadingDelayTime { get; set; }
        
        public PageOV(Uri uri, long delayTime = 2000)
        {
            PageUri = uri;
            LoadingDelayTime = delayTime;
        }
    }
}
