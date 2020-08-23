using Graded_Unit_2.CustomControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages
{
    /// <summary>
    /// Displays users results from frame picker
    /// </summary>
    public sealed partial class ResultsPage : Page
    {
        //Attributes
        private AppManager manager;
        private ObservableCollection<Frame> frames;

        //Constructor
        public ResultsPage()
        {
            this.InitializeComponent();
            frames = new ObservableCollection<Graded_Unit_2.Frame>();
        }

        //Sets manager and then sets current frames
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
            List<Frame> frames = manager.generateFrameList(true);
            this.frames.Clear();
            foreach (Frame frame in frames)
            {
                this.frames.Add(frame);
            }
            if (frames.Count == 0)
            {
                var dialog = new WarningDialog();
                dialog.Title = "Warning";
                dialog.Text = "You have 0 results from the frame picker. Do you want to try the frame picker again? If you select no you will navigate back to the Frame Browser";
                dialog.PrimaryButtonText = "Yes";
                dialog.SecondaryButtonText = "No";
                ContentDialogResult result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    manager.destroyPatient();
                    manager.setMode(Mode.FrameSelector);
                    manager.navigateMain(typeof(AboutYouPage));
                }
                else
                {
                    manager.destroyPatient();
                    manager.setMode(Mode.Browse);
                    manager.navigateMain(typeof(NavPage));
                }
            }
            else
            {
                //This is never really used for this app. 
                //But the idea might be, with the consent of the patient
                //To store the patient class isntance created allong with the result frames
                //And use the data to perfect the frame picker. 
                manager.setPatientFrames(frames);
            }
        }

        //Open frame viewer page when a frame is clicked
        private void frame_Click(object sender, ItemClickEventArgs e)
        {
            Frame frame = (Frame)e.ClickedItem;
            manager.setCurrentFrame(frame);
            manager.navigateMain(typeof(FrameViewerPage));
        }

        //Based on http://stackoverflow.com/questions/41139535/gridview-item-dynamic-width-according-to-screensize-in-uwp
        //Displays frames in 3 rows
        private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            var columns = Math.Ceiling(ActualWidth / 300);
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemWidth = e.NewSize.Width / columns;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemHeight = this.ActualHeight / 3;
        }
    }
}
