using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
using Pharmacy.Implement.Windows.MainScreenWindow.MVVM.ViewModels.Pages.InvoiceManagementPage;
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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.InvoiceManagement
{
    /// <summary>
    /// Interaction logic for InvoiceManagementPage.xaml
    /// </summary>
    public partial class InvoiceManagementPage : QuotableEventPage
    {
        public InvoiceManagementPage()
        {
            InitializeComponent();
        }

        private void StartDateFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ((InvoiceManagementPageViewModel)DataContext).EventCommandOV.StartDateChangedCommand.Execute(sender, e, OrderDataGrid, this);
        }

        private void EndDateFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ((InvoiceManagementPageViewModel)DataContext).EventCommandOV.EndDateChangedCommand.Execute(sender, e, OrderDataGrid, this);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((InvoiceManagementPageViewModel)DataContext).EventCommandOV.SearchTextChangedCommand.Execute(sender, e, OrderDataGrid, this);
        }
    }
}
