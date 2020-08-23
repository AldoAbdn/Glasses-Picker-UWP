using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Graded_Unit_2.Pages;
using Windows.Storage;

namespace Graded_Unit_2
{
    /// <summary>
    /// Manager. This holds most of the data and controls navigation in the app. 
    /// </summary>
    partial class AppManager
    {
        //Attributes
        private Mode mode;
        private Windows.UI.Xaml.Controls.Frame mainFrame;
        private NavPage navPage;
        private Patient currentPatient;
        private FrameSelector frameSelector;
        private Frame currentFrame;
        private BrowserDetails browserDetails;
        private VirtualTryOnImages virtualTryOnImages;

        //Constructor
        internal AppManager(List<Frame> frames)
        {
            this.mode = Mode.Browse;
            this.frameSelector = new FrameSelector(frames);
            this.currentPatient = null;
        }

        //Used to exit facepicker early
        public async void quitFacePicker()
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = "You are leaving the frame picker, all of you data will be lost are you sure you want to continue?";
            dialog.PrimaryButtonText = "Yes";
            dialog.SecondaryButtonText = "No";
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                mode = Mode.Browse;
                destroyPatient();
                navigateMain(typeof(NavPage));
            }
        }

        //Navigation
        public void navigateMain(System.Type page)
        {
            this.mainFrame.Navigate(page, this);
        }

        public void navigateMainBack()
        {
            this.mainFrame.GoBack();
        }

        public void navigateNav(System.Type page)
        {
            this.navPage.navigateNav(page);
        }

        public void navigateNavBack()
        {
            this.navPage.navigateBack();
        }

        //Frame Selector
        public List<Frame> generateFrameList(bool byPatient)
        {
            if (byPatient)
            {
               return this.frameSelector.generateFrameList(this.currentPatient);
            }
            else
            {
               return this.frameSelector.generateFrameList(this.browserDetails);    
            }
        }

        //Patient 
        public void createPatient()
        {
            this.currentPatient = new Patient();
        }

        public void destroyPatient()
        {
            this.currentPatient = null;
        }

        public void addPatientDetail(Detail detailType, Object value)
        {
            this.currentPatient.addPatientDetail(detailType, value);
        }

        //Setters
        public void setMode(Mode mode)
        {
            this.mode = mode;
        }

        public void setMainFrame(Windows.UI.Xaml.Controls.Frame mainFrame)
        {
            this.mainFrame = mainFrame;
        }

        public void setNavPage(NavPage navPage)
        {
            this.navPage = navPage;
        }

        public void setPatientPrescription(Prescription prescription)
        {
            this.currentPatient.setPrescription(prescription);
        }

        public void setPatientFrames(List<Frame> frameList)
        {
            this.currentPatient.setFrames(frameList);
        }

        public void setPatientImages(PatientImages patientImages)
        {
            this.currentPatient.setPatientImages(patientImages);
        }

        public void setCurrentFrame(Frame frame)
        {
            this.currentFrame = frame;
        }

        public void setBrowserDetails(BrowserDetails browserDetails)
        {
            this.browserDetails = browserDetails;
        }

        public void setVirtualTryOnImages(VirtualTryOnImages virtualTryOnImages)
        {
            this.virtualTryOnImages = virtualTryOnImages;
        }

        //Getters
        public Mode getMode()
        {
            return this.mode;
        }

        public PatientImages getPatientImages()
        {
            return this.currentPatient.getPatientImages();
        }

        public Frame getCurrentFrame()
        {
            return this.currentFrame;
        }

        public FrameImages getCurrentFrameImages()
        {
            return this.currentFrame.getFrameImages();
        }

        public BrowserDetails getBrowserDetails()
        {
            return this.browserDetails;
        }

        public VirtualTryOnImages getVirtualTryOnImages()
        {
            return this.virtualTryOnImages;
        }
    }
}
