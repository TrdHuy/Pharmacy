using Pharmacy.Implement.Utils.CustomControls;
using Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.ViewModels;
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

namespace Pharmacy.Implement.Windows.PopupScreenWindow.MVVM.Views
{
    /// <summary>
    /// Interaction logic for PopupScreenWindow.xaml
    /// </summary>
    public partial class PopupScreenWindow : DashboardWindow
    {
        public PopupScreenWindow()
        {
            InitializeComponent();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var context = DataContext as PopupScreenWindowViewModel;
            if(context != null)
            {
                this.MinHeight = context.WindowHeight;
                this.MinWidth = context.WindowWidth;
                this.Height = context.WindowHeight;
                this.Width = context.WindowWidth;
            }
        }
    }
}
