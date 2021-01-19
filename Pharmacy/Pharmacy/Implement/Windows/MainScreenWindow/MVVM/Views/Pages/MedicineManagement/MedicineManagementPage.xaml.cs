using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.MedicineManagementPage;
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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.MedicineManagement
{
    /// <summary>
    /// Interaction logic for MedicineManagementPage.xaml
    /// </summary>
    public partial class MedicineManagementPage : Page
    {
        public MedicineManagementPage()
        {
            InitializeComponent();
            DataContext = new MedicineManagementPageViewModel();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((MedicineManagementPageViewModel)DataContext).SearchTextChangedCommand.Execute(sender, e, DataGrid, this);
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MedicineManagementPageViewModel)DataContext).ShowMedicineInfoCommand.Execute(sender, e, DataGrid, this);
        }
    }
}
