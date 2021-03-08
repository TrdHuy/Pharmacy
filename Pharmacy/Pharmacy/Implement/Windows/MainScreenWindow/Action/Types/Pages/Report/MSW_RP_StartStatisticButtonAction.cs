using Pharmacy.Base.AsyncAction;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;

namespace Pharmacy.Implement.Windows.MainScreenWindow.Action.Types.Pages.Report
{
    public class MSW_RP_StartStatisticButtonAction : MSW_RP_BaseButtonAction
    {
        public override bool Execute(object[] dataTransfer)
        {
            base.Execute(dataTransfer);

            int delaytime = GetDelayTime();
            AsyncAction loadChart = new AsyncAction(dataTransfer,
                LoadChart,
                CanLoadChart,
                LoadChartCallback,
                delaytime);

            AsyncActionExecuter.AsyncExecute(loadChart);

            return true;
        }

        private int GetDelayTime()
        {
            var delayTime = 0;

            if(ViewModel.SelectedItems != null)
            {
                foreach (var item in ViewModel.SelectedItems)
                {
                    var type = ((StatisticalTypeOV)item).SType;
                    switch (type)
                    {
                        case StatisticalType.Income:
                        case StatisticalType.Outcome:
                            delayTime += 500;
                            break;
                        case StatisticalType.Profit:
                            delayTime += 1000;
                            break;
                        case StatisticalType.Quantity:
                            delayTime += 1000;
                            break;
                    }
                }
            }
           
            return delayTime;
        }

        private bool CanLoadChart(AsyncActionResult asyncActionResult, object paramaters)
        {
            return true;
        }

        private bool LoadChartCallback(AsyncActionResult asyncActionResult, object paramaters)
        {
            if (asyncActionResult.MesResult == MessageAsyncActionResult.Finished)
            {
                App.Current.ShowApplicationMessageBox("Tổng hợp dữ liệu thành công!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                    OwnerWindow.MainScreen,
                    "Thông báo!");

                var series = asyncActionResult.Result as Collection<Series>;
                if (series != null)
                {
                    foreach (Series s in series)
                    {
                        ViewModel.Chart.Series.Add(s);
                    }
                }
            }
            else if (asyncActionResult.MesResult == MessageAsyncActionResult.Aborted)
            {
                App.Current.ShowApplicationMessageBox("Lỗi tổng hợp dữ liệu!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Success,
                    OwnerWindow.MainScreen,
                    "Thông báo!");
            }
            ViewModel.ButtonCommandOV.IsStartStatisticButtonRunning = false;

            return true;
        }

        private bool LoadChart(AsyncActionResult asyncActionResult, object paramaters)
        {
            if (ViewModel.SelectedItems.Count == 0)
            {
                App.Current.ShowApplicationMessageBox("Vui lòng chọn ít nhất một kiểu dữ liệu để tổng hợp!",
                    HPSolutionCCDevPackage.netFramework.AnubisMessageBoxType.Default,
                    HPSolutionCCDevPackage.netFramework.AnubisMessageImage.Info,
                    OwnerWindow.MainScreen,
                    "Thông báo!");

                asyncActionResult.MesResult = MessageAsyncActionResult.Cancled;
                return true;
            }

            ViewModel.Chart.Series.Clear();
            if (ViewModel.CurrentStatisticalData == StatisticalData.Revenue)
            {

                var series = new Collection<Series>();
                if (ViewModel.IsStatisticIncomeType())
                {
                    AddSeriesToSource(series, StatisticalType.Income);
                }

                if (ViewModel.IsStatisticOutcomeType())
                {
                    AddSeriesToSource(series, StatisticalType.Outcome);

                }

                if (ViewModel.IsStatisticProfitType())
                {
                    AddSeriesToSource(series, StatisticalType.Profit);
                }

                asyncActionResult.Result = series;
                asyncActionResult.MesResult = MessageAsyncActionResult.Finished;
            }
            return true;
        }

        private void AddSeriesToSource(Collection<Series> source, StatisticalType type)
        {
            var statisSeries = ViewModel.ChartOV.GetStatisticalRevenueSeries(type);
            if(statisSeries != null)
            {
                foreach (Series serie in statisSeries)
                {
                    source.Add(serie);
                }
            }
            
        }
    }

}
