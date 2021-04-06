using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Pharmacy.Base.Utils
{
    public static class HPSSExtension
    {
        public static T FindChild<T>(this DependencyObject dO, string childName)
        where T : DependencyObject
        {
            if (dO == null) return null;

            T foundChild = null;

            int childrentCount = VisualTreeHelper.GetChildrenCount(dO);

            for (int i = 0; i < childrentCount; i++)
            {
                var child = VisualTreeHelper.GetChild(dO, i);

                T childType = child as T;
                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);

                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;

                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }
}
