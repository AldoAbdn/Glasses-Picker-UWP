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

namespace Graded_Unit_2.Pages
{
    /// <summary>
    /// First page of FramePicker 
    /// </summary>
    public sealed partial class FramePickerPage : Page
    {
        //Attributes
        private AppManager manager;

        //Constructor
        public FramePickerPage()
        {
            this.InitializeComponent();
        }

        //Set manager
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
        }

        //Starts framepicker
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            manager.navigateMain(typeof(AboutYouPage));
        }
    }
}
