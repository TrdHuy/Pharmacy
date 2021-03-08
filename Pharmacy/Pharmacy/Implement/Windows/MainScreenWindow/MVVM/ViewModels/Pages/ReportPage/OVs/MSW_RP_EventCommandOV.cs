using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils.InputCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage.OVs
{
    public class MSW_RP_EventCommandOV : BaseViewModel
    {
        public EventHandleCommand ReportPageLoaded { get; set; }
        public MSW_RP_EventCommandOV(BaseViewModel parentModel) : base(parentModel)
        {
            ReportPageLoaded = new EventHandleCommand(OnReportPageLoaded);
        }

        private void OnReportPageLoaded(object sender, EventArgs e, object paramater)
        {
            var vm = ParentsModel as ReportPageViewModel;
            if (vm != null)
            {
                vm.Chart = ((object[])paramater)[0] as BType_Chart;
            }
        }
    }
}
