using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Models.VOs;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels.Pages;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Views.Pages;
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
        public EventHandleCommand ChartAxisLabelSelectionChanged { get; set; }

        public MSW_RP_EventCommandOV(BaseViewModel parentModel) : base(parentModel)
        {
            ReportPageLoaded = new EventHandleCommand(OnReportPageLoaded);
            ChartAxisLabelSelectionChanged = new EventHandleCommand(OnChartAxisLabelSelectionChanged);
        }

        private void OnReportPageLoaded(object sender, EventArgs e, object paramater)
        {
            var vm = ParentsModel as ReportPageViewModel;
            if (vm != null)
            {
                vm.Chart = ((object[])paramater)[0] as BType_Chart;
            }
        }

        private void OnChartAxisLabelSelectionChanged(object sender, EventArgs e, object paramater)
        {
            var dataContext = new DailyReportDetailPageViewModel();
            App.Current.ShowPopupScreenWindow(new PopupScreenWindowViewModel(
                    new PSW_ContentVO()
                    {
                        Content = new DailyReportDetailPage()
                        {
                            DataContext = dataContext
                        },
                        DesignHeight = dataContext.DesignHeight,
                        DesignWidth = dataContext.DesignWidth
                    }
                    , 1000
                ));
        }

    }
}
