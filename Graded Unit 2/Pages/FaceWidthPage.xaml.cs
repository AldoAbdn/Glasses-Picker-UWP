using Graded_Unit_2.Pages.TestPages;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages
{
    /// <summary>
    /// Lets user pick their face width
    /// </summary>
    public sealed partial class FaceWidthPage : Page
    {
        //Attributes
        private AppManager manager;
        public List<ImageSource> Images = new List<ImageSource> { new BitmapImage(new Uri("ms-appx:///Images/frameDimensions.jpg", UriKind.Absolute)), new BitmapImage(new Uri("ms-appx:///Images/colleague help.jpg", UriKind.Absolute))};

        //Constructor
        public FaceWidthPage()
        {
            this.InitializeComponent();
        }

        //Sets manager
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
        }

        //If narrow button clicked
        private void narrow_Click(object sender, RoutedEventArgs e)
        {
            manager.addPatientDetail(Detail.FaceWidth, "SmallPD");
            manager.setMode(Mode.Results);
            manager.navigateMain(typeof(NavPage));
        }

        //If just right button clicked
        private void normal_Click(object sender, RoutedEventArgs e)
        {
            manager.addPatientDetail(Detail.FaceWidth, "MediumPD");
            manager.setMode(Mode.Results);
            manager.navigateMain(typeof(NavPage));
        }

        //if wide button clicked
        private void wide_Click(object sender, RoutedEventArgs e)
        {
            manager.addPatientDetail(Detail.FaceWidth, "LargePD");
            manager.setMode(Mode.Results);
            manager.navigateMain(typeof(NavPage));
        }

        //Lets user exit frame picker
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            manager.quitFacePicker();
        }
    }
}
