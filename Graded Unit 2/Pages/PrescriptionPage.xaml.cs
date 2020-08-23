using Graded_Unit_2;
using Graded_Unit_2.Pages.PrescriptionPages;
using Graded_Unit_2.Pages.TestPages;
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
using static Graded_Unit_2.Prescription;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages
{
    /// <summary>
    /// Used to take in someones prescription
    /// </summary>
    public sealed partial class PrescriptionPage : Page
    {
        //Attribute
        private AppManager manager;
        public Prescription prescription;

        //Constructor
        public PrescriptionPage()
        {
            this.InitializeComponent();
            //prescription = new Prescription(new EyeRX(), new EyeRX());
        }

        //Sets mananger and navigates to prescription form
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
        }

        //On continue, set managers prescription and navigates to next page
        public async void continue_Click(object sender, RoutedEventArgs e)
        {
            //Checks if null and is so displays dialog
            if (pRightEye.isNull() || pLeftEye.isNull())
            {
                //If user wants to skip prescription
                if (await validationDialog())
                {
                    manager.setPatientPrescription(prescription);
                    manager.navigateMain(typeof(FaceShapePage));
                }
            }
            else
            {
                EyeRX rightEye = pRightEye.getEyeRX();
                EyeRX leftEye = pLeftEye.getEyeRX();
                prescription = new Prescription(rightEye, leftEye);
                manager.setPatientPrescription(prescription);
                manager.navigateMain(typeof(FaceShapePage));
            }
            
        }

        //Lets user exit frame picker
        public void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            manager.quitFacePicker();
        }

        //dialog that asks user if they want to skip entering a prescription
        private async Task<bool> validationDialog()
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = "You have not entered a valid prescription, are you sure you want to continue?";
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                return true;
            else
                return false;
        }
    }
}
