using Pharmacy.Base.MVVM.ViewModels;
using Pharmacy.Config;
using Pharmacy.Implement.Utils.CustomControls;
using Pharmacy.Implement.Utils.InputCommand;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Model.OVs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InvoiceManagementPage.OVs
{
    public class MSW_IMP_EventCommandOV : BaseViewModel
    {
        private DateTime? _dateStart { get; set; }
        private DateTime? _dateEnd { get; set; }
        private string _filterText { get; set; }

        public EventCommandModel StartDateChangedCommand { get; set; }
        public EventCommandModel EndDateChangedCommand { get; set; }
        public EventCommandModel SearchTextChangedCommand { get; set; }

        public MSW_IMP_EventCommandOV(BaseViewModel parentVM) : base(parentVM)
        {
            StartDateChangedCommand = new EventCommandModel(OnStartDateChangedEvent);
            EndDateChangedCommand = new EventCommandModel(OnEndDateChangedEvent);
            SearchTextChangedCommand = new EventCommandModel(OnSearchTextChangedEvent);
        }

        private void OnSearchTextChangedEvent(object sender, EventArgs e, object paramater)
        {
            var ctrl = sender as TextBox;
            var orderGrid = (paramater as object[])[0] as DataGrid;

            _filterText = ctrl.Text;

            DoFilter(orderGrid);
        }

        private void OnStartDateChangedEvent(object sender, EventArgs e, object paramater)
        {
            var ctrl = sender as DatePickerCustom;
            var orderGrid = (paramater as object[])[0] as DataGrid;

            _dateStart = ctrl.SelectedDate;
            
            DoFilter(orderGrid);
        }

        private void OnEndDateChangedEvent(object sender, EventArgs e, object paramater)
        {
            var ctrl = sender as DatePickerCustom;
            var orderGrid = (paramater as object[])[0] as DataGrid;

            _dateEnd = ctrl.SelectedDate;

            DoFilter(orderGrid);
        }

        private void DoFilter(DataGrid orderGrid)
        {
            orderGrid.Items.Filter = new Predicate<object>(order => FilterOrderList(order as CustomerOrderOV));
        }

        private bool FilterOrderList(CustomerOrderOV order)
        {
            return FilterByDate(order) &&
                FilterByText(order);
        }

        private bool FilterByText(CustomerOrderOV order)
        {
            if (String.IsNullOrEmpty(_filterText))
            {
                return true;
            }

            return FilterOrderByCustomerName(order)
                || FilterOrderByMedicineName(order);
        }

        private bool FilterOrderByCustomerName(CustomerOrderOV order)
        {
            return RUNE.IS_SUPPORT_SEARCH_ORDER_BY_CUSTOMER_NAME ? (order.CustomerName.IndexOf(_filterText) != -1) : false;
        }

        private bool FilterOrderByMedicineName(CustomerOrderOV order)
        {
            return RUNE.IS_SUPPORT_SEARCH_ORDER_BY_MEDICINE_NAME ? (order.OrderDetail.IndexOf(_filterText) != -1) : false;
        }

        private bool FilterByDate(CustomerOrderOV order)
        {
            if (_dateStart == null || _dateEnd == null)
            {
                return true;
            }

            if (DateTime.Compare((DateTime)_dateEnd, (DateTime)_dateStart) < 0)
            {
                return true;
            }

            return DateTime.Compare(order.Order.OrderTime, (DateTime)_dateStart) > 0
                && DateTime.Compare(order.Order.OrderTime, (DateTime)_dateEnd) < 0 ;
        }
    }
}
