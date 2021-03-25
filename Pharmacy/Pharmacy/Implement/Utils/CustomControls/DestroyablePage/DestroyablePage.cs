using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pharmacy.Implement.Utils.CustomControls.DestroyablePage
{
    public class DestroyablePage : Page
    {
        public DestroyablePage()
        {
            this.Unloaded -= OnDestroyablePage_Unloaded;
            this.Unloaded += OnDestroyablePage_Unloaded;
        }

        private void OnDestroyablePage_Unloaded(object sender, RoutedEventArgs e)
        {
            var destroyableObject = DataContext as IDestroyable;
            if(destroyableObject != null)
            {
                destroyableObject.OnDestroy();
            }
        }
    }
}
