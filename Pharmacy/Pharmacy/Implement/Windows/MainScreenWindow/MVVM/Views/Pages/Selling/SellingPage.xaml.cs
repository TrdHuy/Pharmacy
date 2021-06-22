using HPSolutionCCDevPackage.netFramework;
using Pharmacy.Implement.Utils.CustomControls.QuotableEventPage;
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

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views.Pages.Selling
{
    /// <summary>
    /// Interaction logic for SellingPage.xaml
    /// </summary>
    public partial class SellingPage : QuotableEventPage
    {
        public SellingPage()
        {
            InitializeComponent();
        }

        private void AkerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var ctrl = sender as AkerTextBox;

            int a = 1;
        }
    }
}
