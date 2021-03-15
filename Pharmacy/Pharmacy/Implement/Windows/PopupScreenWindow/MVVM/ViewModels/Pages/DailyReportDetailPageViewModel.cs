using Pharmacy.Base.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages
{
    public class DailyReportDetailPageViewModel : PSW_BasePageViewModel 
    {
        private const double DESIGN_HEIGHT = 420d;
        private const double DESIGN_WIDTH = 800d;

        public DailyReportDetailPageViewModel()
        {
            DesignHeight = DESIGN_HEIGHT;
            DesignWidth = DESIGN_WIDTH;
        }
    }
}
