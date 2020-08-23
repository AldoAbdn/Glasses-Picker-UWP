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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages
{
    /// <summary>
    /// NavPage that displays other menus.
    /// Lets users navigate between browser page and frame picker pages
    /// holds filter and sort dropdowns that are used to generate BrowserDetails
    /// </summary>
    public sealed partial class NavPage : Page
    {
        //Attributes
        private AppManager manager;

        //Constructor
        public NavPage()
        {
            this.InitializeComponent();
            ddFilter.ButtonClickEventHandler += new RoutedEventHandler(dropdownChanged_Click);
            ddSort.ButtonClickEventHandler += new RoutedEventHandler(dropdownChanged_Click);
        }

        //Used by Manager to navigate navpage frame
        public void navigateNav(System.Type type)
        {
            frameNavPage.Navigate(type, this.manager);
        }

        //Used to navigate back one page
        public void navigateBack()
        {
            frameNavPage.GoBack();
        }

        //Set manager
        //Checks what mode manager is in and navigates to appropriate page
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
            manager.setNavPage(this);
            //Set browser details
            manager.setBrowserDetails(generateBrowserDetails());
            if (manager.getMode() == Mode.Browse)
            {
                btnBrowse.IsChecked = true;
            }
            else
            {
                btnFramePicker.IsChecked = true;
                //I didn't use to have to call this manually but this event doesn't fire otherwise
                navButton_Checked(btnFramePicker, new RoutedEventArgs());
            }
        }

        //Opens and closes side nav
        private void toggleSideNav(object sender, RoutedEventArgs e)
        {
            ToggleButton tb = (ToggleButton)sender;
            tb.IsChecked = !tb.IsChecked;
            splitViewHamburger.IsPaneOpen = !splitViewHamburger.IsPaneOpen;
        }

        //Handles navigation 
        //Navigates to different pages depending on mode and what radio button was checked
        private async void navButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((RadioButton)sender == btnBrowse)
            {
                if (manager.getMode() == Mode.Results)
                {
                    //If user confirms they want to navigate away from results
                    if (await disclaimerDialog())
                    {
                        manager.destroyPatient();
                        manager.setMode(Mode.Browse);
                        setUI();
                        manager.navigateNav(typeof(FrameBrowserPage));
                    }
                    else
                    {
                        btnFramePicker.IsChecked = true;
                    }
                }
                else
                {
                    manager.setMode(Mode.Browse);
                    manager.navigateNav(typeof(FrameBrowserPage));
                    setUI();
                }
            }
            else if ((RadioButton)sender == btnFramePicker)
            {
                if (manager.getMode() == Mode.Browse)
                {
                    manager.setMode(Mode.FrameSelector);
                    setUI();
                    manager.navigateNav(typeof(FramePickerPage));
                }
                else if (manager.getMode() == Mode.Results)
                {
                    setUI();
                    manager.navigateNav(typeof(ResultsPage));
                }
            }
        }

        //When dropdown is changed, generate new browserDetails and update 
        public void dropdownChanged_Click(object sender, RoutedEventArgs e)
        {
            manager.setBrowserDetails(generateBrowserDetails());
            //Tries to update current browser page instead of navigating to a new instance
            //This was to speed up performance on ARM build
            try
            { 
                var browserPage = (FrameBrowserPage)frameNavPage.Content;
                browserPage.setFrames(manager.generateFrameList(false));
            }
            catch
            {
                manager.navigateNav(typeof(FrameBrowserPage));
            }
        }

        //Used to set header text based what page is current being displayed
        private void setText(String text)
        {
            txtHeader.Text = text;
        }

        //Hides dropdowns depending on mode
        private void setUI()
        {
            if (manager.getMode() == Mode.Browse)
            {
                ddFilter.Visibility = Visibility.Visible;
                ddSort.Visibility = Visibility.Visible;
                setText("Browse");
            }
            else
            {
                ddFilter.Visibility = Visibility.Collapsed;
                ddSort.Visibility = Visibility.Collapsed;
                if (manager.getMode() == Mode.Results)
                    setText("Your Frames");
                else
                    setText("Frame Picker");
            }
        }

        //Shows dialog if user tries to leave results page, explaining that all frame picker details will be lost
        private async Task<bool> disclaimerDialog()
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = "If you navigate away from your personalized results they will not be saved, and you sure you want to do this?";
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                return true;
            else
                return false;
        }

        //Creates browser details form current dropdown
        private BrowserDetails generateBrowserDetails()
        {
            BrowserDetails browserDetails = ddFilter.generateBrowserDetails();
            browserDetails.sortWords = ddSort.generateSortWords();
            return browserDetails;
        }
    }
}
