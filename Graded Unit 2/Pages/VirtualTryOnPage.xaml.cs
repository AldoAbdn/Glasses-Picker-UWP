using Graded_Unit_2.Pages.Sharing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
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
    /// Lets user preview a frame on top of an image of themselves. front and side view
    /// Lets user share images
    /// This section is a little unfinished, as frame images really need to be properly cut out for this to look good
    /// I am not good with photo editing software and i dont have photoshop
    /// </summary>
    public sealed partial class VirtualTryOnPage : Page
    {
        //Attributes
        private AppManager manager;
        private WriteableBitmap frontImage;
        private WriteableBitmap sideImage;
        private FrameImages frameImages;
        private bool _showBackground = true;
        private bool showBackground { get { return _showBackground; } set { _showBackground = value; toggleBackground(); } }

        //Constructor
        public VirtualTryOnPage()
        {
            this.InitializeComponent();
        }

        //Setters 
        //Called at start
        private void setImages()
        {
            if (manager.getMode() == Mode.Results)
            {
                frontImage = manager.getPatientImages().frontImage;
                sideImage = manager.getPatientImages().sideImage;
            }
            else 
            {
                frontImage = manager.getVirtualTryOnImages().frontImage;
                sideImage = manager.getVirtualTryOnImages().sideImage;
            }

        }

        //Sets front and side images, used by radio buttons mostly to swap 
        private void setFrontImages()
        {
            frameImage.Source = frameImages.getFrontView();
            if (showBackground && frontImage != null)
                canvasBackground.ImageSource = frontImage;
        }

        private void setSideImages()
        {
            frameImage.Source = frameImages.getSideView();
            if (showBackground && sideImage != null)
                canvasBackground.ImageSource = sideImage;           
        }

        //Toggles background on and off
        private void toggleBackground()
        {
            if (showBackground)
            {
                if (frontImageSelector.IsChecked == true)
                    setFrontImages();
                else
                    setSideImages();
            }
            else
            {
                canvasBackground.ImageSource = null;
            }
        }

        //Sets manager
        //Gets patientImages or VirtualTryOnPages
        //Depends on mode
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
            frameImages = manager.getCurrentFrameImages();
            setImages();
            frontImageSelector.IsChecked = true;
        }

        //Swaps between front image and side image
        private void imageSelector_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Name == "frontImageSelector")
            {
                setFrontImages();
            }
            else if (rb.Name == "sideImageSelector")
            {
                setSideImages();
            }
        }

        //Used by click and holding events for width buttons
        //To change frame width
        private void incrementWidth(Button btn)
        {
            if (btn.Name == "btnWidthPlus")
            {
                frameImage.Width += 5;
            }
            else
            {
                if (frameImage.Width > 5)
                    frameImage.Width -= 5;
            }
        }

        //Used by click and holding event for height buttons
        //To change frame height
        private void incrementHeight(Button btn)
        {
            if (btn.Name == "btnHeightPlus")
                frameImage.Height += 5;
            else
            {
                if (frameImage.Height > 5)
                    frameImage.Height -= 5;
            }
        }

        //Used to increase and decrease frame image width
        private void frameWidth_Clicked(object sender, RoutedEventArgs e)
        {
            incrementWidth((Button)sender);
        }

        //Used to increate and decrease frame image height
        private void frameHeight_Clicked(object sender, RoutedEventArgs e)
        {
            incrementHeight((Button)sender);
        }

        //Used to increase and decrease frame image width
        private void frameWidth_Holding(object sender, HoldingRoutedEventArgs e)
        {
            incrementWidth((Button)sender);
        }

        //Used to increate and decrease frame image height
        private void frameHeight_Holding(object sender, HoldingRoutedEventArgs e)
        {
            incrementHeight((Button)sender);
        }

        //Lets user share current image
        private async void share_Click(object sender, RoutedEventArgs e)
        {
            //Get temp folder and create a new temp file
            var localFolder = ApplicationData.Current.TemporaryFolder;
            StorageFile file = await localFolder.CreateFileAsync("photo.jpg", CreationCollisionOption.ReplaceExisting);

            //http://stackoverflow.com/questions/22313245/save-canvas-from-windows-store-app-as-image-file
            var renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(mainCanvas);
            var pixels = await renderTargetBitmap.GetPixelsAsync();

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await
                    BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                byte[] bytes = pixels.ToArray();
                encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                                     BitmapAlphaMode.Ignore,
                                     (uint)mainCanvas.ActualWidth, (uint)mainCanvas.ActualHeight,
                                     96, 96, bytes);

                await encoder.FlushAsync();
            }
            //Displays twitter/facebook share dialog
            var dialog = new ShareDialog(file);
            await dialog.ShowAsync();
        }

        //Shows dialog used for when user tries to navigate away from page
        private async Task<bool> showWarningDialog()
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = "If you leave Virtual Try On, your images will be lost and you will have to retake them. Are you sure you want to do this?";
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                return true;
            else
                return false;
        }

        //Navigates back to frame viewer page
        private async void back_Click(object sender, RoutedEventArgs args)
        {
            if (manager.getMode() != Mode.Results)
            {
                if (await showWarningDialog())
                    manager.navigateMain(typeof(FrameViewerPage));
            }
            else
            {
                manager.navigateMain(typeof(FrameViewerPage));
            }
        }

        //Lets user retake images by navigating back to FaceImagesPage
        private void retakeImages_Click(object sender, RoutedEventArgs e)
        {
            manager.navigateMain(typeof(FaceImagesPage));
        }

        //Allows frame image to be dragged
        private void Image_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //Predicts new coordinates in grid
            var newTop = Canvas.GetTop(frameImage) + e.Delta.Translation.Y;
            var newLeft = Canvas.GetLeft(frameImage) + e.Delta.Translation.X;
            //Stops image getting out of bounds of grid
            if (newTop <= 0 && newLeft <= 0)
            {
                newTop = 0;
                newLeft = 0;
            }
            else if (newTop >= mainCanvas.ActualHeight - frameImage.ActualHeight && newLeft >= mainCanvas.ActualWidth - frameImage.ActualWidth)
            {
                newTop = mainCanvas.ActualHeight - frameImage.ActualHeight;
                newLeft = mainCanvas.ActualWidth - frameImage.ActualWidth;
            }
            else if (newTop >= mainCanvas.ActualHeight - frameImage.ActualHeight && newLeft <= 0)
            {
                newTop = mainCanvas.ActualHeight - frameImage.ActualHeight;
                newLeft = 0;
            }
            else if (newTop <= 0 && newLeft >= mainCanvas.ActualWidth - frameImage.ActualWidth)
            {
                newTop = 0;
                newLeft = mainCanvas.ActualWidth - frameImage.ActualWidth;
            }
            else if (newTop <= 0)
                newTop = 0;
            else if (newLeft <= 0)
                newLeft = 0;
            else if (newTop > mainCanvas.ActualHeight - frameImage.ActualHeight)
                newTop = mainCanvas.ActualHeight - frameImage.ActualHeight;
            else if (newLeft > mainCanvas.ActualWidth - frameImage.ActualWidth)
                newLeft = mainCanvas.ActualWidth - frameImage.ActualWidth;
            //Sets new coordinates
            Canvas.SetTop(frameImage, newTop);
            Canvas.SetLeft(frameImage, newLeft);
        }

        //Set frames initial position
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            double left = (mainCanvas.ActualWidth - frameImage.ActualWidth) / 2;
            double top = (mainCanvas.ActualHeight - frameImage.ActualHeight) / 2;
            Canvas.SetLeft(frameImage, left);
            Canvas.SetTop(frameImage, top);
        }

        private void ToggleBackground_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleButton btnToggleBackground = (ToggleButton)sender;
            if ((bool)btnToggleBackground.IsChecked)
                showBackground = true;
            else
                showBackground = false;
        }

        //Sets frame image to center on grid resize, so that it doesnt end up off screen
        private void mainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetTop(frameImage, (mainCanvas.ActualHeight - frameImage.ActualHeight) / 2);
            Canvas.SetLeft(frameImage, (mainCanvas.ActualWidth - frameImage.ActualWidth) / 2);
        }
    }
}
