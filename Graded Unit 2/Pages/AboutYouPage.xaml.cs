using Graded_Unit_2.Pages.AboutYouPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    /// Used to get age and gender of user. First page of FramePicker
    /// </summary>
    public sealed partial class AboutYouPage : Page
    {
        //Attributes
        private AppManager manager;
        public String gender;
        public int age;

        //Constructor
        public AboutYouPage()
        {
            this.InitializeComponent();
            gender = null;
            age = new int();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Sets manager and creates a new patient
            manager = (AppManager)e.Parameter;
            manager.createPatient();
            //Sets page to faceRecog page
            navigateToFaceRecog();
        }

        //Used if someone doesnt want to take a photo 
        //Or if a photo is taken
        public void navigateToForm()
        {
            frame.Navigate(typeof(DetailsForm), this);
        }

        //If continue button is clicked, set age and gender and navigate to next page
        public void continue_Click(object sender, RoutedEventArgs e)
        {
            manager.addPatientDetail(Detail.Gender, gender);
            manager.addPatientDetail(Detail.Age, age);
            manager.navigateMain(typeof(PrescriptionPage));
        }
        
        //Navigates to details from camera for face recog
        public void navigateToFaceRecog()
        {
            frame.Navigate(typeof(DetailsFromCamera), this);
        }

        //Used to exit framePicker
        public void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            manager.quitFacePicker();
        }
    }
}
