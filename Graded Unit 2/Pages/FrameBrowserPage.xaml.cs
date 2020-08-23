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
    /// Displays frames
    /// Bassed on current BrowserDetails object
    /// </summary>
    public sealed partial class FrameBrowserPage : Page
    {
        //Attributres
        private AppManager manager;
        private ObservableCollection<Frame> frames;
        private bool firstLoadComplete = false;

        //Constructor
        public FrameBrowserPage()
        {
            this.InitializeComponent();
            frames = new ObservableCollection<Graded_Unit_2.Frame>();
        }

        //Sets manager and curret frame list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
            //To speed up performance, I only do this on the very first load of the page
            //This makes switching between frame browser and frame picker very quick
            //I can do this because both framebrowser page and navpage are both cached
            //So they will keep the same state. The frames will now only be set when the filter or sort dropdowns are clicked
            if (!firstLoadComplete)
            {
                setFrames(manager.generateFrameList(false));
                firstLoadComplete = true;
            }
        }

        public void setFrames (List<Frame> frames)
        {
            this.frames.Clear();
            foreach (Frame frame in frames)
            {
                this.frames.Add(frame);
            }
            if (frames.Count == 0)
                noFramesDialog();
        }

        private async void noFramesDialog()
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = "No results, please try other filters. Right tap the filter button to reset it to its original state";
            dialog.SecondaryButtonText = "";
            dialog.PrimaryButtonText = "OK";
            await dialog.ShowAsync();
        }

        //Opens frameViewerpage for clicked frame
        private void frame_Click(object sender, ItemClickEventArgs e)
        {
            Frame frame = (Frame)e.ClickedItem;
            manager.setCurrentFrame(frame);
            manager.navigateMain(typeof(FrameViewerPage));
        }

        //Based on http://stackoverflow.com/questions/41139535/gridview-item-dynamic-width-according-to-screensize-in-uwp
        //I use this to resize the grid to display 3 rows all the time
        private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            var columns = Math.Ceiling(ActualWidth / 300);
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemWidth = e.NewSize.Width / columns;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemHeight = this.ActualHeight / 3;
        }
    }
}
