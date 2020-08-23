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
    /// Lets user select their ideal side length
    /// </summary>
    public sealed partial class SideLengthPage : Page
    {
        //Attributes
        private AppManager manager;
        public List<ImageSource> Images = new List<ImageSource> { new BitmapImage(new Uri("ms-appx:///Images/frameDimensions.jpg", UriKind.Absolute)), new BitmapImage(new Uri("ms-appx:///Images/colleague help.jpg", UriKind.Absolute))};
 
        //Constructor
        public SideLengthPage()
        {
            this.InitializeComponent();
        }

        //sets manager
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
        }

        //Short click
        private void short_Click(object sender, RoutedEventArgs e)
        {
            manager.addPatientDetail(Detail.SideLength, "SmallSL");
            manager.navigateMain(typeof(FaceWidthPage));
        }

        //Just right click
        private void normal_Click(object sender, RoutedEventArgs e)
        {
            manager.addPatientDetail(Detail.SideLength, "MediumSL");
            manager.navigateMain(typeof(FaceWidthPage));
        }

        //Long click
        private void long_Click(object sender, RoutedEventArgs e)
        {
            manager.addPatientDetail(Detail.SideLength, "LongSL");
            manager.navigateMain(typeof(FaceWidthPage));
        }

        //Lets user quit frame picker
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            manager.quitFacePicker();
        }
    }
}
