using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InventoryManagementPage;
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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.WarehouseManagement
{
    /// <summary>
    /// Interaction logic for InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : QuotableEventPage
    {
        public InventoryPage()
        {
            InitializeComponent();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((InventoryManagementPageViewModel)DataContext).EventCommandOV.SearchTextChangedCommand.Execute(sender, e, InventoryDataGrid, this);
        }

        private void MedTypeSearchbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((InventoryManagementPageViewModel)DataContext).EventCommandOV.SearchMedTypeCommand.Execute(sender, e, InventoryDataGrid, this);
        }
    }
}
