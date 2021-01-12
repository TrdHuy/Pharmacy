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
    /// Interaction logic for DiscountByMedicinePage.xaml
    /// </summary>
    public partial class DiscountByMedicinePage : Page
    {
        public DiscountByMedicinePage()
        {
            InitializeComponent();
            DataContext = new DiscountByMedicinePageViewModel();
        }

        private void cbxCustomer_GotFocus(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.IsDropDownOpen = true;
        }

        private void cbxCustomer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.IsDropDownOpen = true;
            (DataContext as DiscountByMedicinePageViewModel).SelectedCustomerChangedCommand.Execute(sender, e, cbxCustomer);
        }

        private void cbxCustomer_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Back || e.Key == Key.Delete)
            {
                (DataContext as DiscountByMedicinePageViewModel).SelectedCustomer = -1;
                (DataContext as DiscountByMedicinePageViewModel).SelectedCustomerChangedCommand.Execute(sender, e, cbxCustomer);
            }
        }
    }
}
