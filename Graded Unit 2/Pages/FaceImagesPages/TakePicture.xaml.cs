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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages.FaceImagesPages
{
    /// <summary>
    /// Used to capture front image in FaceImagesPage
    /// </summary>
    public sealed partial class TakePicture : Page
    {
        //Attributes
        FaceImagesPage mainPage;

        //Constructor
        public TakePicture()
        {
            this.InitializeComponent();
        }

        //Sets main page
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainPage = (FaceImagesPage)e.Parameter;
        }

        //Calls mainPage click event handler when take image button is clicked
        private void takeImage_Click(object sender, RoutedEventArgs e)
        {
            btnTakePicture.IsEnabled = false;
            mainPage.takeFrontImage_Click();
        }
    }
}
