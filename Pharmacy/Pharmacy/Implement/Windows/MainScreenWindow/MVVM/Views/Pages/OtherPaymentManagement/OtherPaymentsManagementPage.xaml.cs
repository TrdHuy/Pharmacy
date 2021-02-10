using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.OtherPaymentsManagementPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.OtherPaymentManagement
{
    /// <summary>
    /// Interaction logic for OtherPaymentsManagementPage.xaml
    /// </summary>
    public partial class OtherPaymentsManagementPage : Page
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
            //(DataContext as WarehouseManagementPageViewModel).ShowWarehouseImportInfoCommand.Execute(sender, e, DataGrid, this);
        }

        private void cbxFilterText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter(sender, e);
        }
    }
}
