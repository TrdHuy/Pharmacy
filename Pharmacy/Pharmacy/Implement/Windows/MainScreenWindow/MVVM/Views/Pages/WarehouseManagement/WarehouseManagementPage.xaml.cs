using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.WarehouseManagementPage;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.WarehouseManagement
{
    /// <summary>
    /// Interaction logic for WarehouseManagementPage.xaml
    /// </summary>
    public partial class WarehouseManagementPage : Page
    {
        public WarehouseManagementPage()
        {
            InitializeComponent();
            DataContext = new WarehouseManagementPageViewModel();
        }

        private void txtFilterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilter(sender, e);
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter(sender, e);
        }

        private void UpdateFilter(object sender, EventArgs eventArgs)
        {
            (DataContext as WarehouseManagementPageViewModel).FilterChangedCommand.Execute(sender,
                eventArgs,
                new object[] { txtFilterText, dprStartDateFilter, dprEndDateFilter, DataGrid });
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as WarehouseManagementPageViewModel).ShowWarehouseImportInfoCommand.Execute(sender, e, DataGrid, this);
        }
    }
}
