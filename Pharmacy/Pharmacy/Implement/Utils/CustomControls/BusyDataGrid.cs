using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pharmacy.Implement.Utils.CustomControls
{
    public class BusyDataGrid : DataGrid
    {
        #region WaitingIconSource
        public static readonly DependencyProperty WaitingIconSourceProperty =
        DependencyProperty.Register(
                "WaitingIconSource",
                typeof(ImageSource),
                typeof(BusyDataGrid),
                new FrameworkPropertyMetadata(
                        default(ImageSource),
                        FrameworkPropertyMetadataOptions.AffectsMeasure |
                        FrameworkPropertyMetadataOptions.AffectsRender,
                        null,
                        null),
                null);

        public ImageSource WaitingIconSource
        {
            get { return (ImageSource)GetValue(WaitingIconSourceProperty); }
            set { SetValue(WaitingIconSourceProperty, value); }
        }
        #endregion

        #region IsBusy
        public static readonly DependencyProperty IsBusyProperty =
           DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyDataGrid),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnIsBusyChangeCallback)));

        private static void OnIsBusyChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BusyDataGrid ctrl = d as BusyDataGrid;
            var newVal = (bool)e.NewValue;
            var oldVal = (bool)e.OldValue;

            ctrl.OnIsBusyChanged(newVal, oldVal);

        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }
        #endregion

        protected virtual void OnIsBusyChanged(bool newVal, bool oldVal)
        {
            
        }

        public BusyDataGrid()
        {
        }
    }
}
