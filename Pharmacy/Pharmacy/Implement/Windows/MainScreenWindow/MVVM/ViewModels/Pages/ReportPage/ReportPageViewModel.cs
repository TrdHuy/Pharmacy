using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MSW_BasePageVM.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage.OVs;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.SettingPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage
{
    public enum StatisticalData
    {
        Revenue = 1,
        Medicine = 2
    }
    public enum StatisticalType
    {
        Income = 1,
        Outcome = 2,
        Profit = 3,
        Quantity = 50
    }
    public class ReportPageViewModel : MSW_BasePageViewModel
    {
        private static Logger L = new Logger("ReportPageViewModel");

        private object _selectedStatisticalData;
        private object _selectedStatisticalType;

        #region Bindable properties field
        public MSW_RP_ButtonCommandOV ButtonCommandOV { get; set; }
        public MSW_RP_ChartOV ChartOV { get; set; }
        public MSW_RP_EventCommandOV EventCommandOV { get; set; }

        public ObservableCollection<StatisticalDataOV> StatisticalDataSource { get; set; }
        public ObservableCollection<StatisticalTypeOV> StatisticalTypeSource { get; set; }
        public ObservableCollection<object> SelectedItems { get; set; } = new ObservableCollection<object>();

        public object SelectedStatisticalType
        {
            get
            {
                return _selectedStatisticalType;
            }
            set
            {
                _selectedStatisticalType = value;
                InvalidateOwn();
            }
        }
        public object SelectedStatisticalData
        {
            get
            {
                return _selectedStatisticalData;
            }
            set
            {
                var oldValue = _selectedStatisticalData;
                _selectedStatisticalData = value;
                OnSelectedStatisticalDataChanged(oldValue, value);
                InvalidateOwn();
            }
        }

        public Visibility DateTimePickerAreaVisibility
        {
            get
            {
                if (SelectedStatisticalData == null)
                {
                    return Visibility.Collapsed;
                }

                return ((StatisticalDataOV)SelectedStatisticalData).Data == StatisticalData.Revenue ?
                    Visibility.Visible : Visibility.Collapsed;
            }
        }
        #endregion

        public StatisticalData CurrentStatisticalData
        {
            get
            {
                return SelectedStatisticalData == null ? StatisticalData.Revenue :
                    ((StatisticalDataOV)SelectedStatisticalData).Data;
            }
        }
        public BType_Chart Chart { get; set; }

        protected override Logger logger => L;

        protected override void OnInitializing()
        {
            InstantiateItem();
            ButtonCommandOV = new MSW_RP_ButtonCommandOV(this);
            EventCommandOV = new MSW_RP_EventCommandOV(this);
            ChartOV = new MSW_RP_ChartOV(this);
        }

        protected override void OnInitialized()
        {
        }

        private void InstantiateItem()
        {
            InstantiateStatisticalDataSource();
            InstantiateStatisticalTypeSource();
        }

        private void InstantiateStatisticalTypeSource()
        {
            StatisticalTypeSource = new ObservableCollection<StatisticalTypeOV>();

            if (SelectedStatisticalData == null)
            {
                return;
            }

            if (((StatisticalDataOV)SelectedStatisticalData).Data == StatisticalData.Revenue)
            {
                StatisticalTypeSource.Add(new StatisticalTypeOV("Thu", StatisticalType.Income));
                StatisticalTypeSource.Add(new StatisticalTypeOV("Chi", StatisticalType.Outcome));
                StatisticalTypeSource.Add(new StatisticalTypeOV("Lợi nhuận",StatisticalType.Profit));
            }
            else if (((StatisticalDataOV)SelectedStatisticalData).Data == StatisticalData.Medicine)
            {
                StatisticalTypeSource.Add(new StatisticalTypeOV("Số lượng",StatisticalType.Quantity));
            }

            SelectedStatisticalType = StatisticalTypeSource[0];

            Invalidate("StatisticalTypeSource");

        }

        private void InstantiateStatisticalDataSource()
        {
            StatisticalDataSource = new ObservableCollection<StatisticalDataOV>();
            StatisticalDataSource.Add(new StatisticalDataOV("Doanh thu", StatisticalData.Revenue));
            StatisticalDataSource.Add(new StatisticalDataOV("Thuốc", StatisticalData.Medicine));
            SelectedStatisticalData = StatisticalDataSource[0];
        }


        private void OnSelectedStatisticalDataChanged(object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                Invalidate("DateTimePickerAreaVisibility");
            }
            if (newValue != null)
            {
                InstantiateStatisticalTypeSource();
            }
        }

        internal bool IsStatisticIncomeType()
        {
            return SelectedItems.Where(o => ((StatisticalTypeOV)o).SType == StatisticalType.Income).Count() > 0;
        }

        internal bool IsStatisticOutcomeType()
        {
            return SelectedItems.Where(o => ((StatisticalTypeOV)o).SType == StatisticalType.Outcome).Count() > 0;
        }

        internal bool IsStatisticProfitType()
        {
            return SelectedItems.Where(o => ((StatisticalTypeOV)o).SType == StatisticalType.Profit).Count() > 0;
        }

        internal bool IsStatisticQuantityType()
        {
            return SelectedItems.Where(o => ((StatisticalTypeOV)o).SType == StatisticalType.Quantity).Count() > 0;
        }
    }

    public class StatisticalDataOV : BaseViewModel
    {
        public string DataName { get; set; }
        public StatisticalData Data { get; set; }

        public StatisticalDataOV(string name, StatisticalData data)
        {
            DataName = name;
            Data = data;
        }

        public override string ToString()
        {
            return DataName;
        }
    }

    public class StatisticalTypeOV : BaseViewModel
    {
        public string Type { get; set; }
        public StatisticalType SType { get; set; }
        public StatisticalTypeOV(string name, StatisticalType stype)
        {
            Type = name;
            SType = stype;
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
