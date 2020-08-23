using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.ProjectOxford.Face;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.FileProperties;
using Windows.Devices.Sensors;
using Windows.Graphics.Display;
using Windows.System.Display;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages.AboutYouPages
{
    /// <summary>
    /// Uses Microsoft FaceAPI to guess patients age and gender and this is used to fill in DetailsForm page
    /// </summary>
    public sealed partial class DetailsFromCamera : Page
    {
        //Attributes
        private AboutYouPage mainPage;
        private bool cancelled = false; 
        //Media Capture 
        MediaCapture mediaCapture;
        //Face API
        FaceServiceClient faceServiceClient = new FaceServiceClient("8ef53bf081ed41c29ea576a634e176d4");
        //Display request - stops screen lock being activated
        private DisplayRequest displayRequest;

        //Constructor
        public DetailsFromCamera()
        {
            this.InitializeComponent();
        }

        //Starts capture element preview for taking photos
        //NEW TO ME
        private async Task startPreview()
        {
            try
            {
                mediaCapture = new MediaCapture();
                await mediaCapture.InitializeAsync();
                previewControl.Source = mediaCapture;
                await mediaCapture.StartPreviewAsync();
                btnTakeImage.IsEnabled = true;
                btnExit.IsEnabled = true;
                return;
            }
            catch(Exception e)
            {
                await validationDialog("Camera failed to start. Please check your camera and try again");
            }
        }

        //Sets main page and fixes orientation for camera
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainPage = (AboutYouPage)e.Parameter;
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            displayRequest = new DisplayRequest();
            displayRequest.RequestActive();
        }

        //Resets orientation when finished
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.None;
            displayRequest.RequestRelease();
        }

        //Once page is loaded, checks if user wants to opt out of photo
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var result = await disclaimerDialog();
            if (!result)
                mainPage.navigateToForm();
            else
                await startPreview();
        }


        //Takes image
        //NEW TO ME 
        private async void btnTakeImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnTakeImage.IsEnabled = false;
                prFaceAPI.IsActive = true; prFaceAPI.Visibility = Visibility.Visible;
                //Face API would not take stream straight from .CapturePhotoToStreamAsync
                //Method below involes temp storing the image and getting stream from stored image
                //https://docs.microsoft.com/en-us/windows/uwp/audio-video-camera/basic-photo-video-and-audio-capture-with-mediacapture
                var localFolder = ApplicationData.Current.TemporaryFolder;
                StorageFile file = await localFolder.CreateFileAsync("photo.jpg", CreationCollisionOption.ReplaceExisting);
                using (var captureStream = new InMemoryRandomAccessStream())
                {
                    await mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), captureStream);
                    using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        var decoder = await BitmapDecoder.CreateAsync(captureStream);
                        var encoder = await BitmapEncoder.CreateForTranscodingAsync(fileStream, decoder);
                        var properties = new BitmapPropertySet {{ "System.Photo.Orientation", new BitmapTypedValue(PhotoOrientation.Normal, PropertyType.UInt16) }};
                        await encoder.BitmapProperties.SetPropertiesAsync(properties);
                        await encoder.FlushAsync();
                    }
                }
                var stream = await Task.Run(() => File.OpenRead(file.Path));
                await getDetailsFromPhoto(stream);
                //Deletes file for data protection purposes 
                await file.DeleteAsync();
                await mediaCapture.StopPreviewAsync();
            }
            catch(Exception exc)
            {
                if (!cancelled)
                    await validationDialog("Face API failed. Please check your internet connection and try again");
            }
            finally
            {
                mainPage.navigateToForm();
            }
        }

        //Uploads image to faceapi and stores results
        //NEW TO ME
        private async Task getDetailsFromPhoto(Stream stream)
        {
            Face[] faces;
            List<Face> facesList = new List<Face>();
            //Tries to call face api, if fail show error
            try
            {
                faces = await faceServiceClient.DetectAsync(stream, false, false, new List<FaceAttributeType> { FaceAttributeType.Gender, FaceAttributeType.Age });
                facesList = faces.ToList<Face>();
            }
            catch (Exception e)
            {
                if (!cancelled)
                    await validationDialog("Face API failed. Please check your internet connection and try again");
            }
            if (facesList.Count > 0 && !cancelled)
            {
                var face = facesList[0];
                if (face.FaceAttributes.Gender != null || face.FaceAttributes.Gender != "")
                    mainPage.gender = face.FaceAttributes.Gender[0].ToString().ToUpper() + face.FaceAttributes.Gender.Substring(1);
                if (face.FaceAttributes.Age >= 16)
                    mainPage.age = Convert.ToInt32(face.FaceAttributes.Age);
            }
        }

        //Shows dialog asking if user wants to opt out of photo, returns result
        private async Task<bool> disclaimerDialog()
        {
            var dialog = new WarningDialog();
            dialog.Title = "Photo Recognition";
            dialog.Text = "This app will use this image to get your age and gender. Your image will be uploaded to the Microsoft Face API service. Microsoft reserves the right to store your image for analysis to improve their FaceAPI serivce. If you do not want agree to these terms you can click skip to enter your details manually. Do you want to use the facial recognition feature?";
            dialog.PrimaryButtonText = "Yes";
            dialog.SecondaryButtonText = "Skip";
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                return true;
            else
                return false;
        }

        //Lets user exit preview
        private async void btnExit_Click(object sender, RoutedEventArgs e)
        {
            cancelled = true;
            try
            {
                await mediaCapture.StopPreviewAsync();
            }
            finally
            {   
                mainPage.navigateToForm();
            }
        }

        //Used to show a dialog
        private async Task validationDialog(String message)
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = message;
            dialog.SecondaryButtonText = "";
            var result = await dialog.ShowAsync();
        }
    }
}	
