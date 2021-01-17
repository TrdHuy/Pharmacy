using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Pharmacy.Implement.Utils.CustomControls
{
    class DatePickerCustom : DatePicker
    {
        public DatePickerCustom()
        {
            this.Language = XmlLanguage.GetLanguage("vi-VN");
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Delete || e.Key == Key.Back)
            {
                SelectedDate = null;
                DisplayDate = DateTime.Today;
            }
            base.OnKeyUp(e);
        }
    }
}
