using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RFIDApp.Resource
{
    public partial class AppResource : ResourceDictionary
    {
        private void minimize_click(object sender, RoutedEventArgs e)
        {
            Window window = FindParent<Window>((DependencyObject)sender);
            if (window != null)
                window.WindowState = WindowState.Minimized;
        }

        private void maximize_click(object sender, RoutedEventArgs e)
        {
            Window window = FindParent<Window>((DependencyObject)sender);
            if (window != null)
                window.WindowState = WindowState.Maximized;
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
            Window window = FindParent<Window>((DependencyObject)sender);
            if (window != null)
                window.Close();
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            if (child == null)
                return null;

            DependencyObject parent = VisualTreeHelper.GetParent(child);
            if (parent is T)
                return (T)parent;

            return FindParent<T>(parent);
        }
    }
}
