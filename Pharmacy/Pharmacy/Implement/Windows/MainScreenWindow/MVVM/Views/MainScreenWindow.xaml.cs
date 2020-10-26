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
using System.Windows.Shapes;

namespace Pharmacy.Implement.Windows.MainScreenWindow.MVVM.Views
{
    /// <summary>
    /// Interaction logic for LoginScreenWindow.xaml
    /// </summary>
    public partial class MainScreenWindow : Window
    {
        public MainScreenWindow()
        {
            InitializeComponent();
        }

        private void GridMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
