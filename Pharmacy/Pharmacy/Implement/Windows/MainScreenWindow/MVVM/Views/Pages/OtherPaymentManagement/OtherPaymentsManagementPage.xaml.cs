using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.OtherPaymentManagement
{
    /// <summary>
    /// Interaction logic for OtherPaymentsManagementPage.xaml
    /// </summary>
    public partial class OtherPaymentsManagementPage : QuotableEventPage
    {
        public OtherPaymentsManagementPage()
        {
            InitializeComponent();
            DataContext = new OtherPaymentsManagementPageViewModel();
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter(sender, e);
        }

        private void UpdateFilter(object sender, EventArgs eventArgs)
        {
            if (DataContext != null)
            {
                (DataContext as OtherPaymentsManagementPageViewModel).FilterChangedCommand.Execute(sender,
                    eventArgs,
                    new object[] { cbxFilterText, dprStartDateFilter, dprEndDateFilter, DataGrid });
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as OtherPaymentsManagementPageViewModel).ShowOtherPaymentInfoCommand.Execute(sender, e, DataGrid, this);
        }

        private void cbxFilterText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter(sender, e);
        }
    }
}
