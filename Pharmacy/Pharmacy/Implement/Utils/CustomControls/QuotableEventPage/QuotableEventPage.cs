using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Implement.Utils.CustomControls.QuotableEventPage
{
    public class QuotableEventPage : Page
    {
        public QuotableEventPage()
        {
            Unloaded -= Page_Unloaded;
            Unloaded += Page_Unloaded;

            Loaded -= Page_Loaded;
            Loaded += Page_Loaded;

            SizeChanged -= Page_SizeChanged;
            SizeChanged += Page_SizeChanged;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var destroyableObject = DataContext as IQuotableEvent;
            if (destroyableObject != null)
            {
                destroyableObject.OnApplyTemplate(this);
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var destroyableObject = DataContext as IQuotableEvent;
            if (destroyableObject != null)
            {
                destroyableObject.OnSizeChanged(this);
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            var destroyableObject = DataContext as IQuotableEvent;
            if (destroyableObject != null)
            {
                destroyableObject.OnUnloaded(this);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var destroyableObject = DataContext as IQuotableEvent;
            if (destroyableObject != null)
            {
                destroyableObject.OnLoaded(this);
            }
        }

        public override void EndInit()
        {
            base.EndInit();
            var destroyableObject = DataContext as IQuotableEvent;
            if (destroyableObject != null)
            {
                destroyableObject.OnEndInit(this);
            }
        }
    }
}
