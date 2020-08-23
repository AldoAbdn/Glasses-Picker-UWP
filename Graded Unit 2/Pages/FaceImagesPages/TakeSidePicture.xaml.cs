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
    /// Used to capture side image
    /// </summary>
    public sealed partial class TakeSidePicture : Page
    {
        //Attributes
        FaceImagesPage mainPage;

        //Constructor
        public TakeSidePicture()
        {
            this.InitializeComponent();
        }

        //Sets mainPage
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainPage = (FaceImagesPage)e.Parameter;
        }

        //Calls mainPage button click when take image is clicked
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnTakeSideImage.IsEnabled = false;
            mainPage.takeSideImage_Click();
        }
    }
}
