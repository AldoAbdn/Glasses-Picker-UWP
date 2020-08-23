using Graded_Unit_2.Pages.Sharing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
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
    /// Shows details and image of a frame
    /// </summary>
    public sealed partial class FrameViewerPage : Page
    {
        //Attributes
        private AppManager manager;
        public Frame frame { get; set; }
        //Dependency Properties
        public String DimensionString
        {
            get { return (String)GetValue(DimensionStringProperty); }
            set { SetValue(DimensionStringProperty, value); }
        }
        public static readonly DependencyProperty DimensionStringProperty = DependencyProperty.Register("DimensionString", typeof(String), typeof(FrameViewerPage), null);

        //Constructor
        public FrameViewerPage()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }

        //Sets manager and dimension text
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
            frame = manager.getCurrentFrame();
            DimensionString = frame.getFrameProperties().eyeSize + " x " + frame.getFrameProperties().bridgeSize + " x " + frame.getFrameProperties().sideLength;
            Bindings.Update();
        }

        //Virtual try on click
        private void virtualTryOn_Click(object sender, RoutedEventArgs e)
        {
            //If mode is results, checks if patientImages has already been set
            //If set navigate straight to virtualtryon page
            //else navigate to FaceImagesPage to set patientImages
            if (manager.getMode() == Mode.Results)
            {
                PatientImages images = null;
                try
                {
                    images = manager.getPatientImages();
                }
                catch { }
                
                if (images != null)
                {
                    manager.navigateMain(typeof(VirtualTryOnPage));
                }
                else
                {
                    manager.navigateMain(typeof(FaceImagesPage));
                }
            }
            else
            {
                manager.navigateMain(typeof(FaceImagesPage));
            }
        }
         
        //Opens share dialog
        private async void share_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage btmap = (BitmapImage)frame.frameImages.getViews()[0];
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(btmap.UriSource); 
            var dialog = new ShareDialog(file);
            await dialog.ShowAsync();
        }

        //Lets user exit frame picker
        private void back_Click(object sender, RoutedEventArgs e)
        {
            manager.navigateMain(typeof(NavPage));
        }
    }
}
