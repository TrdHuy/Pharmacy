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

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Views.UserControls
{
    /// <summary>
    /// Interaction logic for DownloadProgressControl.xaml
    /// </summary>
    public partial class DownloadProgressControl : UserControl
    {
        public DownloadProgressControl()
        {
            InitializeComponent();
            ProgressBar.ValueChanged += ProgressBar_ValueChanged;
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ProgressBar.Value == 100)
            {
                LoadingAnimation.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoadingAnimation.Visibility = Visibility.Visible;
            }
        }
    }
}
