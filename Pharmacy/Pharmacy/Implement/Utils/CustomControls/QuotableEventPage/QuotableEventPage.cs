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
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            var destroyableObject = DataContext as IQuotableEvent;
            if (destroyableObject != null)
            {
                destroyableObject.OnUnloaded();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var destroyableObject = DataContext as IQuotableEvent;
            if (destroyableObject != null)
            {
                destroyableObject.OnLoaded();
            }
        }

        public override void BeginInit()
        {
            base.BeginInit();
            var destroyableObject = DataContext as IQuotableEvent;
            if (destroyableObject != null)
            {
                destroyableObject.OnBeginInit();
            }
        }

        public override void EndInit()
        {
            base.EndInit();
            var destroyableObject = DataContext as IQuotableEvent;
            if (destroyableObject != null)
            {
                destroyableObject.OnEndInit();
            }
        }
    }
}
