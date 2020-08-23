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

namespace Graded_Unit_2.Pages.AboutYouPages
{
    /// <summary>
    /// Lets a patient input their age and gender
    /// </summary>
    public sealed partial class DetailsForm : Page
    {
        //Attributes
        public AboutYouPage mainPage;

        //Constructor
        public DetailsForm()
        {
            this.InitializeComponent();
            mainPage = new AboutYouPage();
            setupAgeCB();
        }

        //Sets manager
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainPage = (AboutYouPage)e.Parameter;
            setComboBoxes();
        }

        //when continue clicked, checks if values are null
        //If not, call main pages continue event handler 
        private void continue_Click(object sender, RoutedEventArgs e)
        {
            if (cbGender.SelectedItem != null && cbAge.SelectedItem != null)
            {
                mainPage.continue_Click(sender, e);
            }
            else
            {
                validationDialog("You have not entered valid details, please fill out the form before continuing");
            }
        }

        //When face recog clicked, navigate to face recog page
        private void faceRecog_Click(object sender, RoutedEventArgs e)
        {
            mainPage.navigateToFaceRecog();
        }

        //Validation dialog, doesnt return anything
        private async void validationDialog(String message)
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = message;
            dialog.SecondaryButtonText = "";
            var result = await dialog.ShowAsync();
        }

        //Sets main pages gender to a cb value
        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            ComboBoxItem cbi = (ComboBoxItem)cb.SelectedItem;
            var value = cbi.Content;
            if (cb.Name == "cbGender")
                mainPage.gender = Convert.ToString(value);
            else
                mainPage.age = Convert.ToInt32(value);
        }

        //Used to populate age ComboBox 
        private void setupAgeCB()
        {
            for (int i = 16; i < 120; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = i;
                cbi.Name = "CB" + i.ToString();
                cbAge.Items.Add(cbi);
            }
        }

        //Used to set comboboxes to values from manager if
        //They are populated
        private void setComboBoxes()
        {
            if (mainPage.gender != null)
                cbGender.SelectedItem = cbGender.FindName("CB" + mainPage.gender);
            if (mainPage.age >= 16)
                cbAge.SelectedItem = cbAge.FindName("CB" + mainPage.age.ToString());
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            mainPage.btnQuit_Click(sender, e);
        }
    }
}
