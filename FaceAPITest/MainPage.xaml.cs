using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FaceAPITest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        FaceServiceClient faceServiceClient = new FaceServiceClient("8ef53bf081ed41c29ea576a634e176d4");

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var file = await openJpg();
            FileRandomAccessStream stream = await Task.Run(async () => (FileRandomAccessStream)await file.OpenAsync(FileAccessMode.Read));
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.SetSource(stream);
            FacePhoto.Source = bitmapImage;
            Title.Text = "Detecting...";
            var faces = new List<Face>();
            Stream detectStream = await Task.Run(() => File.OpenRead(file.Path));
            faces = await detectFaces(detectStream);
            Title.Text = String.Format("Detection Finished. {0} face(s) detected", faces.Count);
        }

        private async Task<StorageFile> openJpg()
        {

            var openDlg = new Windows.Storage.Pickers.FileOpenPicker();
            openDlg.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            openDlg.FileTypeFilter.Add(".jpg");
            var result = await openDlg.PickSingleFileAsync();
            return result;
        }

        private async Task<List<Face>> detectFaces(Stream stream)
        {

            var faces = await faceServiceClient.DetectAsync(stream, false, false, new List<FaceAttributeType> { FaceAttributeType.Gender, FaceAttributeType.Age});
            return faces.ToList<Face>();
        }
    }
}
