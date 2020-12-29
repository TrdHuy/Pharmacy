using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Utils.CustomControls
{
    public class PageHeaderControl : UserControl
    {
        public static readonly DependencyProperty HeaderIconProperty =
            DependencyProperty.Register("HeaderIcon", typeof(ImageSource), typeof(PageHeaderControl));

        public static readonly DependencyProperty HeaderTextProperty =
           DependencyProperty.Register("HeaderText", typeof(string), typeof(PageHeaderControl));

        public PageHeaderControl()
        {

        }

        public ImageSource HeaderIcon
        {
            get { return (ImageSource)GetValue(HeaderIconProperty); }
            set { SetValue(HeaderIconProperty, value); }
        }

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }
    }
}
