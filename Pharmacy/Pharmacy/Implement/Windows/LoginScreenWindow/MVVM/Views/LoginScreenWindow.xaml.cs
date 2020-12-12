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

namespace Pharmacy.Implement.Windows.LoginScreenWindow.MVVM.Views
{
    /// <summary>
    /// Interaction logic for LoginScreenWindow.xaml
    /// </summary>
    public partial class LoginScreenWindow : Window
    {
        public LoginScreenWindow()
        {
            InitializeComponent();
        }

        public static implicit operator LoginScreenWindow(Lazy<LoginScreenWindow> v)
        {
            throw new NotImplementedException();
        }
    }
}
