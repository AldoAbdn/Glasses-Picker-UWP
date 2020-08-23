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
using Windows.Storage;
using Windows.Media.Capture;
using Windows.ApplicationModel;
using System.Threading.Tasks;
using Windows.System.Display;
using Windows.Graphics.Display;
using Graded_Unit_2.Pages.FaceImagesPages;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media.MediaProperties;
using Windows.Devices.Sensors;
using Windows.Graphics.Imaging;
using Windows.Storage.FileProperties;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages
{
    /// <summary>
    /// Used to get face images for virtualTryOn
    /// </summary>
    public sealed partial class FaceImagesPage : Page
    {
        //Attributes
        private AppManager manager;
        private WriteableBitmap frontFaceImage;
        private WriteableBitmap sideFaceImage;
        private bool cancelled = false;
        public String FaceMode;
        //Image Preview
        MediaCapture mediaCapture;
        //Diplsy request - used to stop screen locking while camera is on
        private DisplayRequest displayRequest;
            
        //Constructor
        public FaceImagesPage()
        {
            this.InitializeComponent();
            FaceMode = "Front";
        }

        //Starts preview
        private async Task startPreview()
        {
            try
            {
                mediaCapture = new MediaCapture();
                await mediaCapture.InitializeAsync();
                PreviewControl.Source = mediaCapture;
                await mediaCapture.StartPreviewAsync();
                btnExit.IsEnabled = true;
            }
            catch(Exception e)
            {
                if (!cancelled)
                    await errorDialog();
            }
        }

        //Sets manager and starts preview
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            displayRequest = new DisplayRequest();
            displayRequest.RequestActive();
            await startPreview();
            frame.Navigate(typeof(TakePicture), this);
        }

        //Resets display orientation when finished
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.None;
            displayRequest.RequestRelease();
        }

        //Takes iamge and creates a writableBitmap from result
        //NEW TO ME
        private async Task<WriteableBitmap> takeImage()
        {
            try
            {
                var capture = await mediaCapture.PrepareLowLagPhotoCaptureAsync(ImageEncodingProperties.CreateUncompressed(MediaPixelFormat.Bgra8));
                var photo = await capture.CaptureAsync();
                var softwareBitmap = photo.Frame.SoftwareBitmap;
                WriteableBitmap bitmap = new WriteableBitmap(softwareBitmap.PixelWidth, softwareBitmap.PixelHeight);
                softwareBitmap.CopyToBuffer(bitmap.PixelBuffer);
                await capture.FinishAsync();
                return bitmap;
            }
            catch (Exception e)
            {
                if (!cancelled)
                    await errorDialog();
                manager.navigateMain(typeof(FrameViewerPage));
            }
            return new WriteableBitmap(0,0);
        }

        //Navigates to confirmPage once image is taken
        public async void takeFrontImage_Click()
        {
            //Set image and navigate to next frame
            frontFaceImage = await takeImage();
            frame.Navigate(typeof(ConfirmPicture), this);
        }

        //Navigates to confirm page once image is taken, but for side image
        public async void takeSideImage_Click()
        {
            sideFaceImage = await takeImage();
            frame.Navigate(typeof(ConfirmPicture), this);
        }

        //if image is confirmed move to takeside image
        //else take image again
        public void frontImageConfirm(bool isConfirmed)
        {
           if (isConfirmed)
           {
                FaceMode = "Side";
                frame.Navigate(typeof(TakeSidePicture), this);
            }
            else
                frame.Navigate(typeof(TakePicture), this);
        }

        //If confirmed move to virtual try on
        //else take image again
        public async Task sideImageConfirmAsync(bool isConfirmed)
        { 
            if (isConfirmed)
            {
                await mediaCapture.StopPreviewAsync();
                setImages();
                btnExit.IsEnabled = false; btnExit.Visibility = Visibility.Collapsed;
                frame.Navigate(typeof(VirtualTryOnPage), manager);
            }
            else
                frame.Navigate(typeof(TakeSidePicture), this);
        }

        private void setImages()
        {
            if (manager.getMode() == Mode.Results)
            {
                PatientImages pi = new PatientImages(frontFaceImage, sideFaceImage);
                manager.setPatientImages(pi);
            }
            else
            {
                VirtualTryOnImages vtom = new VirtualTryOnImages(frontFaceImage, sideFaceImage);
                manager.setVirtualTryOnImages(vtom);
            }
        }

        //Getters
        public WriteableBitmap getFrontImage()
        {
            return frontFaceImage;
        }

        public WriteableBitmap getSideImage()
        {
            return sideFaceImage;
        }

        private async void btnExit_Click(object sender, RoutedEventArgs e)
        {
            cancelled = true;
            try
            {
                await mediaCapture.StopPreviewAsync();
            }
            finally
            {
                //Normally I would just use Frame.GoBack() for this
                //But for some reason the last page stored when navigated to from VirtualTryOnPage
                //If FaceImagesPage again and not VirtualTryOn
                //So this is a quick fix that seems to work
                var previousPageType = Frame.BackStack.Last()?.SourcePageType;
                if (previousPageType == typeof(FaceImagesPage))
                    manager.navigateMain(typeof(VirtualTryOnPage));
                else
                    manager.navigateMain(typeof(FrameViewerPage));
            }
        }

        private async Task errorDialog()
        {
            var dialog = new WarningDialog();
            dialog.Text = "An Error Occurred: Please ensure that your camera is enabled";
            dialog.SecondaryButtonText = "";
            await dialog.ShowAsync();
        }

    }
}
