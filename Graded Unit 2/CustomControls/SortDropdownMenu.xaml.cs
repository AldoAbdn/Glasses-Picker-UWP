using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Graded_Unit_2.CustomControls
{
    /// <summary>
    /// Dropdown menu. This is used to pick a sort keyword on FrameBrowserPage
    /// </summary>
    public sealed partial class SortDropdownMenu : UserControl
    {
        public event RoutedEventHandler ButtonClickEventHandler;

        public SortDropdownMenu()
        {
            this.InitializeComponent();
        }

        public List<String> generateSortWords()
        {
            List<String> sortWords = new List<String>();
            foreach (ToggleButton tb in spMain.Children)
            {
                if (tb.IsChecked == true)
                {
                    sortWords.Add((String)tb.Content);
                }
            }
            return sortWords;
        }

        private void reset()
        {
            rbPoleNumber.IsChecked = true;
        }

        private void toggleButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickEventHandler(sender, e);
        }

        private void btnDropdownToggle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (spMain.Visibility == Visibility.Collapsed)
                spMain.Visibility = Visibility.Visible;
            else
                spMain.Visibility = Visibility.Collapsed;
        }

        private void btnDropdownToggle_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            reset();
            ButtonClickEventHandler(sender, e);
            spMain.Visibility = Visibility.Collapsed;
        }
    }
}
