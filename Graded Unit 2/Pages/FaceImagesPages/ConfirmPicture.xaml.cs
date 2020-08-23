using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

namespace Graded_Unit_2.Pages.FaceImagesPages
{
    /// <summary>
    /// Used to confirm a preview of an image. This is used in FaceImagesPage 
    /// </summary>
    public sealed partial class ConfirmPicture : Page
    {
        //Attributes
        FaceImagesPage mainPage;

        //Constructor
        public ConfirmPicture()
        {
            this.InitializeComponent();
        }

        //On navigates to, set mainPage equal to passed in value
        //Check if front image or side image, set grid background
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainPage = (FaceImagesPage)e.Parameter;
            if (mainPage.FaceMode == "Front")
                mainGridImageBrush.ImageSource = mainPage.getFrontImage();

            else
                mainGridImageBrush.ImageSource = mainPage.getSideImage();
        }

        //Checks if yes or no was clicked. 
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            bool val;
            if (btn.Name == "btnNo")
            {
                val = false;
            }
            else if (btn.Name == "btnYes")
            {
                val = true;
            }
            else
            {
                val = false;
            }
            if (mainPage.FaceMode == "Front")
            {
                mainPage.frontImageConfirm(val);
            }
            else
            {
                await mainPage.sideImageConfirmAsync(val);
            }
        }
    }
}
