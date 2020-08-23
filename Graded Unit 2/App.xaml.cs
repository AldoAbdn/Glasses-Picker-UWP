using Graded_Unit_2.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static Graded_Unit_2.Frame;

namespace Graded_Unit_2
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 

        private AppManager manager;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            //I use this to change the theme between light and dark.
            //This can only be set before load and not during runtime
            RequestedTheme = Windows.UI.Xaml.ApplicationTheme.Dark;
        }

        //This creates the list of frames from the CSV file.
        //I create the list hesre as when I had it in the framePicker class, the first time the app opens there was no frames displayed
        //I think this was because the funciton that loads the csv file runs async so i dont think the frames had been loaded yet
        private async Task<List<Frame>> generateFrames()
        {
            List<Frame> frames = new List<Frame>();
            //Opens csv file that contains frame details. each line is a record. 
            List<String> data = new List<String>();
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///CSV/Frames.csv"));
            Stream fileStream = await file.OpenStreamForReadAsync();
            using (StreamReader fileReader = new StreamReader(fileStream))
            {
                string line;
                string[] row;
                fileReader.ReadLine();
                //Reads file line by line
                while ((line = fileReader.ReadLine()) != null)
                {
                    //Splits csv file
                    row = line.Split(',');
                    //Sets properties
                    FrameProperties frameProperties = new FrameProperties(row[0], row[1], row[2], row[3], row[4], row[5], row[6], Convert.ToInt32(row[7]), Convert.ToInt32(row[8]), Convert.ToInt32(row[9]), Convert.ToInt64(row[10]), row[11], row[12], Convert.ToBoolean(Convert.ToInt16(row[13])), Convert.ToBoolean(Convert.ToInt16(row[14])), row[15].Split(' '), row[16].Split(' '));
                    //Sets images and attaches imageFailed event. This sets the image uri to a default is no image is found at uri from CSV file
                    var img1 = new BitmapImage();
                    var img2 = new BitmapImage();
                    var img3 = new BitmapImage();
                    img1.ImageFailed += imageFailed;
                    img2.ImageFailed += imageFailed;
                    img3.ImageFailed += imageFailed;
                    img1.UriSource = new Uri("ms-appx://" + row[17], UriKind.Absolute);
                    img2.UriSource = new Uri("ms-appx://" + row[18], UriKind.Absolute);
                    img3.UriSource = new Uri("ms-appx://" + row[19], UriKind.Absolute);
                    FrameImages frameImages = new FrameImages(new List<ImageSource>() { img1, img2, img3 }, img2, img3);
                    //Creates new frame instance and adds to list
                    Graded_Unit_2.Frame frame = new Graded_Unit_2.Frame(frameProperties, frameImages);
                    frames.Add(frame);
                }
            }
            return frames;
        }


        //Sets image uri to a default if image failed to load original uri
        private void imageFailed(object sender, RoutedEventArgs e)
        {
            var bitmap = (BitmapImage)sender;
            bitmap.UriSource = new Uri("ms-appx:///Images/imageUnavailable.png", UriKind.Absolute);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
//#if DEBUG
//            if (System.Diagnostics.Debugger.IsAttached)
//            {
//                this.DebugSettings.EnableFrameRateCounter = true;
//            }
//#endif
            Windows.UI.Xaml.Controls.Frame rootFrame = Window.Current.Content as Windows.UI.Xaml.Controls.Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Windows.UI.Xaml.Controls.Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter

                    //Setup frame list, create manager.
                    //Navigate to NavPage
                    var frames = await generateFrames();
                    manager = new AppManager(frames);
                    manager.setMainFrame(rootFrame);
                    rootFrame.Navigate(typeof(NavPage), manager);
                }
                //Makes sure app opens in fullscreen mode
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
