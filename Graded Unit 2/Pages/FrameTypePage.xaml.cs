using Graded_Unit_2.CustomControls;
using Graded_Unit_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    /// Lets user pick favourite frame type
    /// </summary>
    public sealed partial class FrameTypePage : Page
    {
        //Attributes
        private AppManager manager;
        private List<FrameType> Types;

        //Constructor
        public FrameTypePage()
        {
            this.InitializeComponent();
            this.Types = new List<FrameType>() { new FrameType("Fullrim", "ms-appx:///Images/FrameImages/18G1.jpg"), new FrameType("Supra", "ms-appx:///Images/FrameImages/14G1.jpg"), new FrameType("Rimless", "ms-appx:///Images/FrameImages/13F1.jpg") };
        }

        //Sets manager
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
        }

        //Gets selected types from GridView
        private List<String> getSelectedTypes()
        {
            List<String> types = new List<string>();
            var items = gvTypes.SelectedItems;
            foreach (FrameType type in items)
            {
                types.Add(type.type);
            }
            return types;
        }

        //Checks that at least one type has been selected and then continues
        private void continue_Click(object sender, RoutedEventArgs e)
        {
            if (gvTypes.SelectedItems.Count == 0)
            {
                validationDialog("You must select at least one type");
            }
            else
            {
                List<String> types = getSelectedTypes();
                manager.addPatientDetail(Detail.Type, types);
                manager.navigateMain(typeof(SunglassPage));
            }
        }

        //Shows a dialog
        private async void validationDialog(String message)
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = message;
            dialog.SecondaryButtonText = "";
            var result = await dialog.ShowAsync();
        }

        //Lets user exit frame picker
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            manager.quitFacePicker();
        }

        private void gvTypes_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            int rows = 1;
            int cols = 3;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemWidth = e.NewSize.Width / cols;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemHeight = e.NewSize.Height / rows;
        }
    }
}
