using Graded_Unit_2.CustomControls;
using Graded_Unit_2.Events;
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
    /// Lets user pick what their face shape is
    /// </summary>
    public sealed partial class FaceShapePage : Page
    {
        //Attributes
        private AppManager manager;
        private List<FaceShape> FaceShapes;

        //Constructor
        public FaceShapePage()
        {
            this.InitializeComponent();
            FaceShapes = new List<FaceShape> { new FaceShape("Oval", "ms-appx:///Images/FaceShapes/Oval.png"), new FaceShape("Round", "ms-appx:///Images/FaceShapes/Round.jpg"), new FaceShape("Rectangular", "ms-appx:///Images/FaceShapes/Rectangular.jpg"), new FaceShape("Square", "ms-appx:///Images/FaceShapes/Square.jpg") };
        }

        //Gets selected faceshape from GridView
        private String getSelectedFace()
        {
            List<String> FaceShapes = new List<String>();
            var items = gvFaceShapes.SelectedItems;
            foreach (FaceShape FaceShape in items)
            {
                FaceShapes.Add(FaceShape.faceshape);
            }
            return FaceShapes[0];
        }

        //Sets manager
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
        }

        //Checks that at least one faceshape has been selected
        //If yes then navigates to next page
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if (gvFaceShapes.SelectedItems.Count == 0)
            {
                validationDialog("You must select one face shape");
            }
            else
            {
                var FaceShape = getSelectedFace();
                manager.addPatientDetail(Detail.FaceShape, FaceShape);
                manager.navigateMain(typeof(ColourPage));
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

        //Lets user quit framepicker
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            manager.quitFacePicker();
        }

        //Adaptive grid items
        private void gvFaceShapes_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            int rows = 1; int cols = 4;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemWidth = e.NewSize.Width / cols;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemHeight = e.NewSize.Height / rows;
        }

        //Makes border blue, this is becuase in signle select mode you don't get the little tick 
        private void gvFaceShapes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Any())
            {
                GridViewItem gvItem = gvFaceShapes.ContainerFromItem(e.AddedItems[0]) as GridViewItem;
                gvItem.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Cyan);
                gvItem.BorderThickness = new Thickness(5);
            }
            if (e.RemovedItems != null && e.RemovedItems.Any())
            {
                try{
                    GridViewItem gvItem = gvFaceShapes.ContainerFromItem(e.RemovedItems[0]) as GridViewItem;
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
