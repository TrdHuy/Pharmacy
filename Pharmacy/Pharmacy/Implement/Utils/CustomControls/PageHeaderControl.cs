using Pharmacy.Implement.Utils.Extensions;
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
            DependencyProperty.Register("HeaderIcon", typeof(ImageSource), typeof(PageHeaderControl),
                new PropertyMetadata(Properties.Resources.default_app_icon.ToImageSource()));

        public static readonly DependencyProperty HeaderTextProperty =
           DependencyProperty.Register("HeaderText", typeof(string), typeof(PageHeaderControl)
               ,new PropertyMetadata("This is header text"));

        public static readonly DependencyProperty HeaderFontSizeProperty =
           DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(PageHeaderControl)
               , new PropertyMetadata(30d));

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

        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }
    }
}
