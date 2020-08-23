using Graded_Unit_2.Models;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages
{
    /// <summary>
    /// Lets user select if they are specifically looking for sunglasses or not
    /// </summary>
    public sealed partial class SunglassPage : Page
    {
        //Attributes
        private AppManager manager;
        private List<Sunglass> sunglass;

        //Constructor
        public SunglassPage()
        {
            this.InitializeComponent();
            sunglass = new List<Sunglass> { new Sunglass("Red", "No", false), new Sunglass("Green", "Yes", true) };
        }

        //Sets manager
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
        }

        //Lets user quit frame picker
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            manager.quitFacePicker();
        }

        private void gvSunglass_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            int rows = 1; int cols = 2;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemWidth = e.NewSize.Width / cols;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemHeight = e.NewSize.Height / rows;
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if (gvSunglass.SelectedItems.Count == 0)
            {
                validationDialog("You must select one answer");
            }
            else
            {
                var isSunglass = getSelectedFace();
                manager.addPatientDetail(Detail.Sunglass, isSunglass);
                manager.navigateMain(typeof(SideLengthPage));
            }
        }

        //Shows dialog
        private async void validationDialog(String message)
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = message;
            dialog.SecondaryButtonText = "";
            var result = await dialog.ShowAsync();
        }

        //Gets selected answer from GridView
        private bool getSelectedFace()
        {
            List<bool> isSunglass = new List<bool>();
            var items = gvSunglass.SelectedItems;
            foreach (Sunglass sunglass in items)
            {
                isSunglass.Add(sunglass.value);
            }
            return isSunglass[0];
        }

        private void gvSunglass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Any())
            {
                GridViewItem gvItem = gvSunglass.ContainerFromItem(e.AddedItems[0]) as GridViewItem;
                gvItem.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Cyan);
                gvItem.BorderThickness = new Thickness(5);
            }

            if (e.RemovedItems != null && e.RemovedItems.Any())
            {
                try
                {
                    GridViewItem gvItem = gvSunglass.ContainerFromItem(e.RemovedItems[0]) as GridViewItem;
                    gvItem.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Transparent);
                    gvItem.BorderThickness = new Thickness(0);
                }
                catch (Exception exc)
                {

                }
            }
        }
    }
}
