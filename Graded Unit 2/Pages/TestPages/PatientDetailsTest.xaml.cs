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
using static Graded_Unit_2.AppManager;
using static Graded_Unit_2.AppManager.Patient;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages.TestPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PatientDetailsTest : Page
    {
        AppManager manager;
        Patient patient;
        PatientDetails patientDetails;

        public PatientDetailsTest()
        {
            this.InitializeComponent();
            patient = new Patient();
            patientDetails = new PatientDetails();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
         //   patient = manager.getPatient();
           // patientDetails = patient.getPatientDetails();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            manager.navigateNav(typeof(FrameBrowserPage));
        }
    }
}
