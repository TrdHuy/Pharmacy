using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Implement.Utils.Converter;
using Pharmacy.Implement.Utils.DatabaseManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Media;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.ReportPage.OVs
{
    public class MSW_RP_ChartOV : BaseViewModel
    {
        private const int MINIMUM_DATA_POINTS = 5;
        private const int MAXIMUM_DATA_POINTS = 10;
        private const int DEFAULT_DATA_POINTS = 5;

        private readonly Color OUTCOME_COLOR = Color.FromArgb(255, 213, 74, 74);
        private readonly Color INCOME_COLOR = Color.FromArgb(255, 135, 170, 102);
        private readonly Color PROFIT_COLOR = Color.FromArgb(255, 105, 198, 255);

        public BType_Chart Chart { get; set; }

        public IEnumerable<tblOrder> CustomerOrder { get; set; }
        public IEnumerable<tblOtherPayment> BusinessPayment { get; set; }
        public RangeObservableCollection<RevenueChartOV> RevenueSource { get; set; }
        public bool IsShouldCaculateRevenueSource { get; set; }

        public MSW_RP_ChartOV(BaseViewModel parentModel) : base(parentModel)
        {
            IsShouldCaculateRevenueSource = true;
            InstantiateItems();
        }

        private void InstantiateItems()
        {
            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_CUSTOMER_ORDERS_CMD_KEY,
                new SQLQueryCustodian((queryResult) =>
                {
                    if (queryResult.MesResult == MessageQueryResult.Done)
                    {
                        CustomerOrder = queryResult.Result as IEnumerable<tblOrder>;
                    }
                }));

            DbManager.Instance.ExecuteQuery(SQLCommandKey.GET_ALL_ACTIVE_OTHER_PAYMENT_DATA_CMD_KEY,
                new SQLQueryCustodian((queryResult) =>
                {
                    if (queryResult.MesResult == MessageQueryResult.Done)
                    {
                        BusinessPayment = queryResult.Result as IEnumerable<tblOtherPayment>;
                    }
                }));
        }

        internal Collection<Series> GetStatisticalRevenueSeries(StatisticalType types)
        {
            if (CustomerOrder != null && BusinessPayment != null)
            {
                GetRevenueSource();
                var series = new Collection<Series>();
                switch (types)
                {
                    case StatisticalType.Income:
                        series.Add(new ColumnSeries()
                        {
                            Title = "Thu",
                            IndependentValueBinding = new Binding("DateTime") { Converter = new SystemDateTimeToStringConverter(), ConverterParameter = "DATE_ONLY" },
                            DependentValueBinding = new Binding("In"),
                            DataPointStyle = GetColumnIncomeProfiDataPointStyle(),
                            ItemsSource = RevenueSource
                        });
                        break;
                    case StatisticalType.Profit:
                        series.Add(new ColumnSeries()
                        {
                            Title = "Lợi nhuận",
                            IndependentValueBinding = new Binding("DateTime") { Converter = new SystemDateTimeToStringConverter(), ConverterParameter = "DATE_ONLY" },
                            DependentValueBinding = new Binding("Profit"),
                            DataPointStyle = GetColumnSerieProfiDataPointStyle(),
                            ItemsSource = RevenueSource
                        });
                        series.Add(new LineSeries()
                        {
                            Title = "Lợi nhuận",
                            IndependentValueBinding = new Binding("DateTime") { Converter = new SystemDateTimeToStringConverter(), ConverterParameter = "DATE_ONLY" },
                            DependentValueBinding = new Binding("Profit"),
                            ItemsSource = RevenueSource,
                            DataPointStyle = GetLineSerieDataPointStyle(),
                            LegendItemStyle = GetLegendItemLineSeriesStyle()
                        });
                        break;
                    case StatisticalType.Outcome:
                        series.Add(new ColumnSeries()
                        {
                            Title = "Chi",
                            IndependentValueBinding = new Binding("DateTime") { Converter = new SystemDateTimeToStringConverter(), ConverterParameter = "DATE_ONLY" },
                            DependentValueBinding = new Binding("Out"),
                            ItemsSource = RevenueSource,
                            DataPointStyle = GetColumnOutcomeProfiDataPointStyle(),
                        });
                        break;
                    default:
                        break;
                }
                return series;
            }
            return null;
        }

        private void GetRevenueSource()
        {
            if (!IsShouldCaculateRevenueSource)
            {
                return;
            }

            var IncomeFromCustomerReport = CustomerOrder.GroupBy(o => o.OrderTime.Date)
                .Select(o => new RevenueChartOV()
                {
                    DateTime = o.Key,
                    In = o.Sum(e => e.PurchasePrice)
                });

            var InOutcomeFromOtherPaymentReport = BusinessPayment.GroupBy(p => p.PaymentTime.Date)
                .Select(p => new RevenueChartOV()
                {
                    DateTime = p.Key,
                    Out = p.Where(pa => pa.PaymentType == 1)
                    .Sum(pa => pa.TotalPrice),
                    In = p.Where(pa => pa.PaymentType == 0)
                    .Sum(pa => pa.TotalPrice),
                });

            RevenueSource = new RangeObservableCollection<RevenueChartOV>(
                new ObservableCollection<RevenueChartOV>(
                    IncomeFromCustomerReport.Concat(InOutcomeFromOtherPaymentReport)
                    .GroupBy(o => o.DateTime)
                    .Select(o =>
                    {
                        var inCome = o.Sum(ov => ov.In);
                        var outCome = o.Sum(ov => ov.Out);
                        return new RevenueChartOV()
                        {
                            DateTime = o.Key,
                            In = inCome,
                            Out = outCome,
                            Profit = inCome - outCome
                        };
                    })
                    .OrderBy(r => r.DateTime))
                , MINIMUM_DATA_POINTS
                , MAXIMUM_DATA_POINTS
                , DEFAULT_DATA_POINTS);

            IsShouldCaculateRevenueSource = false;
        }



        #region Style generator helper
        private Style GetColumnOutcomeProfiDataPointStyle()
        {
            var style = new Style(typeof(ColumnDataPoint));
            Setter set_1 = new Setter()
            {
                Property = DataPoint.BackgroundProperty,
                Value = new SolidColorBrush(OUTCOME_COLOR)
            };
            style.Setters.Add(set_1);
            return style;
        }
        private Style GetColumnIncomeProfiDataPointStyle()
        {
            var style = new Style(typeof(ColumnDataPoint));
            Setter set_1 = new Setter()
            {
                Property = DataPoint.BackgroundProperty,
                Value = new SolidColorBrush(INCOME_COLOR)
            };
            style.Setters.Add(set_1);
            return style;
        }
        private Style GetColumnSerieProfiDataPointStyle()
        {
            var style = new Style(typeof(ColumnDataPoint));
            Setter set_1 = new Setter()
            {
                Property = DataPoint.BackgroundProperty,
                Value = new SolidColorBrush(PROFIT_COLOR)
            };
            style.Setters.Add(set_1);
            return style;
        }

        private Style GetLineSerieDataPointStyle()
        {
            var style = new Style(typeof(LineDataPoint));
            Setter set_1 = new Setter()
            {
                Property = DataPoint.BackgroundProperty,
                Value = new SolidColorBrush(PROFIT_COLOR)
            };
            Setter set_2 = new Setter()
            {
                Property = DataPoint.TemplateProperty,
                Value = new ControlTemplate()
            };
            style.Setters.Add(set_1);
            style.Setters.Add(set_2);
            return style;
        }

        private Style GetLegendItemLineSeriesStyle()
        {
            var style = new Style(typeof(LegendItem));
            Setter set_1 = new Setter()
            {
                Property = LegendItem.VisibilityProperty,
                Value = Visibility.Collapsed
            };
            style.Setters.Add(set_1);
            return style;
        }
        #endregion
    }

    public class RevenueChartOV : BaseViewModel
    {
        public decimal In { get; set; }
        public decimal Out { get; set; }
        public decimal Profit { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class MedicineChartOV : BaseViewModel
    {
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
    }

    public class RangeObservableCollection<T> : ObservableCollection<T>
    {
        private ObservableCollection<T> ob_Collection;
        private Range ObservableRange;

        public RangeObservableCollection(ObservableCollection<T> staticSource, int minRange, int maxRange, int defaultRange = 10)
        {
            ob_Collection = staticSource;
            ObservableRange = new Range(0, ob_Collection.Count() - 1, minRange, maxRange, defaultRange);
            ObservableRange.RangePropertyChange += OnRangePropertyChanged;
            InitRangeObservableCollection();
        }

        private void InitRangeObservableCollection()
        {
            for (int i = ObservableRange.Left; i <= ObservableRange.Right; i++)
            {
                base.Add(ob_Collection[i]);
            }
        }

        public void ExtendRight(int unit)
        {
            ObservableRange.Right += unit;
        }

        public void ExtendLeft(int unit)
        {
            ObservableRange.Left += unit;
        }

        public void ExtendBoth(int unitLeft, int unitRight)
        {
            ObservableRange.SetBothLeftRight(ObservableRange.Left + unitLeft, ObservableRange.Right + unitRight);
        }


        private void OnRangePropertyChanged(object sender, RangePropertyChangeEventArg e)
        {
            if (e.isLeftChange)
            {
                UpdateLeft(e);
            }
            else if (e.isRightChange)
            {
                UpdateRight(e);
            }
            else if (e.isBothChange)
            {
                UpdateLeft(e);
                UpdateRight(e);
            }
        }

        private void UpdateRight(RangePropertyChangeEventArg e)
        {
            if (e.newRightValue > e.oldRightValue)
            {
                for (int i = e.newRightValue; i > e.oldRightValue; i--)
                {
                    base.Add(ob_Collection[i]);
                }
            }
            else if (e.newRightValue < e.oldRightValue)
            {

                for (int i = e.oldRightValue; i > e.newRightValue; i--)
                {
                    if (base.Contains(ob_Collection[i]))
                        base.Remove(ob_Collection[i]);
                }
            }
        }

        private void UpdateLeft(RangePropertyChangeEventArg e)
        {
            if (e.newLeftValue < e.oldLeftValue)
            {
                for (int i = e.newLeftValue; i < e.oldLeftValue; i++)
                {
                    base.Insert(0, ob_Collection[i]);
                }
            }
            else if (e.newLeftValue > e.oldLeftValue)
            {
                for (int i = e.oldLeftValue; i < e.newLeftValue; i++)
                {
                    if (base.Contains(ob_Collection[i]))
                        base.Remove(ob_Collection[i]);
                }

            }
        }

        public void Add(IEnumerable<T> items)
        {
            foreach (var ob in items)
            {
                ob_Collection.Add(ob);
            }
        }

        public void Remove(IEnumerable<T> items)
        {
            foreach (var ob in items)
            {
                if (ob_Collection.Contains(ob))
                    ob_Collection.Remove(ob);
            }
        }

        public new void Add(T item)
        {
            ob_Collection.Add(item);
            ObservableRange.MaximumRight += 1;
        }

        public new void Remove(T item)
        {
            ob_Collection.Remove(item);
            ObservableRange.MaximumRight -= 1;
        }

        public new void Insert(int index, T item)
        {
            ob_Collection.Insert(index, item);
            ObservableRange.MaximumRight += 1;
        }
    }

    public class Range
    {
        public event RangePropertyChangeHandler RangePropertyChange;
        private int left;
        private int right;
        private int maximumRight;
        private int minimumLeft;

        public int Left
        {
            get
            {
                return left;
            }
            set
            {
                var old = left;
                var propertyChanged = UpdateRange(value, true);
                if (propertyChanged)
                {
                    RangePropertyChange?.Invoke(this, new RangePropertyChangeEventArg()
                    {
                        oldLeftValue = old,
                        newLeftValue = left,
                        isLeftChange = true
                    });
                }
            }
        }

        public int Right
        {
            get
            {
                return right;
            }
            set
            {
                var old = right;
                var propertyChanged = UpdateRange(value, false);
                if (propertyChanged)
                {
                    RangePropertyChange?.Invoke(this, new RangePropertyChangeEventArg()
                    {
                        oldRightValue = old,
                        newRightValue = right,
                        isRightChange = true
                    });
                }
            }
        }

        public int MinimumLeft
        {
            get
            {
                return minimumLeft;
            }
            set
            {
                minimumLeft = value;
                OnBoundChanged();
            }
        }


        public int MaximumRight
        {
            get
            {
                return maximumRight;
            }
            set
            {
                maximumRight = value;
                OnBoundChanged();
            }
        }
        public int MinimumRange { get; private set; }
        public int MaximumRange { get; private set; }

        public Range(int minLeft, int maxRight, int minRange, int maxRange, int defaultRange = 10)
        {
            if (maxRight < minLeft)
            {
                throw new InvalidOperationException("max must be greater than min");
            }

            if (maxRange < minRange)
            {
                throw new InvalidOperationException("max range must be greater than min range");
            }

            if (defaultRange < minRange || defaultRange > maxRange)
            {
                throw new InvalidOperationException("default range must be from min range to max range");
            }
            minimumLeft = minLeft;
            maximumRight = maxRight;
            MinimumRange = minRange < maxRight - minLeft ? minRange : maxRight - minLeft;
            MaximumRange = maxRange < maxRight - minLeft ? maxRange : maxRight - minLeft;

            left = minLeft;
            right = defaultRange < maxRight - minLeft ? defaultRange : maxRight - minLeft;
        }

        public void SetLeftOrRight(int newLeft, int newRight)
        {
            var oldLeft = left;
            var oldRight = right;
            var leftChanged = UpdateRange(newLeft, true);
            var rightChanged = UpdateRange(newRight, false);

            if (leftChanged && rightChanged)
            {
                RangePropertyChange?.Invoke(this, new RangePropertyChangeEventArg()
                {
                    oldLeftValue = oldLeft,
                    newLeftValue = newLeft,
                    oldRightValue = oldRight,
                    newRightValue = newRight,
                    isBothChange = true
                });
            }
            else if (leftChanged)
            {
                RangePropertyChange?.Invoke(this, new RangePropertyChangeEventArg()
                {
                    oldLeftValue = oldLeft,
                    newLeftValue = newLeft,
                    isLeftChange = true
                });
            }
            else if (rightChanged)
            {
                RangePropertyChange?.Invoke(this, new RangePropertyChangeEventArg()
                {
                    oldRightValue = oldRight,
                    newRightValue = newRight,
                    isRightChange = true
                });
            }
        }

        public void SetBothLeftRight(int newLeft, int newRight)
        {
            var oldLeft = left;
            var oldRight = right;

            // update right before update left to check (left 2 right) range condition 
            right = newRight <= MaximumRight ? newRight : MaximumRight;
            var leftChanged = UpdateRange(newLeft, true);

            // re-update right with (right 2 left) range conditon
            var rightChanged = UpdateRange(newRight, false);

            if (leftChanged && rightChanged)
            {
                RangePropertyChange?.Invoke(this, new RangePropertyChangeEventArg()
                {
                    oldLeftValue = oldLeft,
                    newLeftValue = newLeft,
                    oldRightValue = oldRight,
                    newRightValue = newRight,
                    isBothChange = true
                });
            }
            else
            {
                left = oldLeft;
                right = oldRight;
            }
        }

        private bool UpdateRange(int value, bool isLeft)
        {
            if (isLeft)
            {
                if (value >= MinimumLeft && value + MinimumRange <= Right && Right - value <= MaximumRange)
                {
                    left = value;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                if (value <= MaximumRight && value - MinimumRange >= Left && value - Left <= MaximumRange)
                {
                    right = value;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void OnBoundChanged()
        {
            bool maxRangeChanged = false;
            if(MaximumRange < maximumRight - minimumLeft)
            {
                maxRangeChanged = true;
                MaximumRange = maximumRight - minimumLeft;
            }

            if (maxRangeChanged)
            {
                if(MinimumRange > MaximumRange)
                {
                    MinimumRange = MaximumRange; 
                }
            }
        }

    }

    public class RangePropertyChangeEventArg
    {
        public int oldLeftValue;
        public int newLeftValue;
        public int oldRightValue;
        public int newRightValue;

        public bool isLeftChange;
        public bool isRightChange;
        public bool isBothChange;

    }
    public delegate void RangePropertyChangeHandler(object sender, RangePropertyChangeEventArg e);
}
