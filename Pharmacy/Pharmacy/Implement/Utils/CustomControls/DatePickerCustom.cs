using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace Pharmacy.Implement.Utils.CustomControls
{
    class DatePickerCustom : DatePicker
    {
        public DatePickerCustom()
        {
            this.Language = XmlLanguage.GetLanguage("vi-VN");
            BorderThickness = new System.Windows.Thickness(2);
            BorderBrush = Brushes.Black;
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

    class DateTimePickerCustom : DateTimePicker
    {
        public DateTimePickerCustom()
        {
            BorderThickness = new System.Windows.Thickness(2);
            BorderBrush = Brushes.Black;
            Value = DateTime.Now;
            this.Language = XmlLanguage.GetLanguage("vi-VN");
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Delete || e.Key == Key.Back)
            {
                Value = DateTime.Now;
            }
            base.OnKeyUp(e);
        }
    }
}
