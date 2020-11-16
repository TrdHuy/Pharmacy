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
    public class HPSL_Button : Button
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(HPSL_Button));

        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(HPSL_Button));

        public static readonly DependencyProperty ImageHeightProperty =
          DependencyProperty.Register("ImageHeight", typeof(double), typeof(HPSL_Button));

        public static readonly DependencyProperty ImageElipseBorderHeightProperty =
          DependencyProperty.Register("ImageElipseBorderHeight"
              , typeof(double)
              , typeof(HPSL_Button)
              , new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    OnICBHChange));

        public static readonly DependencyProperty ImageElipseBorderWidthProperty =
                  DependencyProperty.Register("ImageElipseBorderWidth"
                      , typeof(double)
                      , typeof(HPSL_Button)
                      , new FrameworkPropertyMetadata(
                        0.0,
                        FrameworkPropertyMetadataOptions.AffectsRender,
                        OnICBHChange));

        public HPSL_Button() : base()
        {
            this.DefaultStyleKey = typeof(HPSL_Button);
        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public double ImageHeight
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public double ImageElipseBorderHeight
        {
            get { return (double)GetValue(ImageElipseBorderHeightProperty); }
            set { SetValue(ImageElipseBorderHeightProperty, value); }
        }

        public double ImageElipseBorderWidth
        {
            get { return (double)GetValue(ImageElipseBorderWidthProperty); }
            set { SetValue(ImageElipseBorderWidthProperty, value); }
        }

        private static void OnICBHChange
            (DependencyObject source, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
