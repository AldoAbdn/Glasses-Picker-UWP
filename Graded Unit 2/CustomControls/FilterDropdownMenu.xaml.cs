using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Graded_Unit_2.CustomControls
{
    /// <summary>
    /// Dropdown Menu. Used on nav page to select custom filters for results. 
    /// Generates an instance of BrowserDetails based on what toggleButtons are selected
    /// </summary>
    public sealed partial class FilterDropdownMenu : UserControl
    {
        BrowserDetails browserDetails;
        List<StackPanel> stackPanels;
        public event RoutedEventHandler ButtonClickEventHandler;

        public FilterDropdownMenu()
        {
            this.InitializeComponent();
            //Generates stack panel list
            //Used to close all other stackpanels when a new one is opened
            stackPanels = new List<StackPanel>();
            foreach (var child in spMain.Children)
            {
                if (child.GetType() == typeof(StackPanel))
                {
                    stackPanels.Add((StackPanel)child);
                }
            }
            browserDetails = new BrowserDetails();
        }

        //creates a new instance of BrowserDetails
        //This is used in NavPage
        //Result is combined with result from SortDropdown 
        public BrowserDetails generateBrowserDetails()
        {
            BrowserDetails browserDetails = new BrowserDetails();
            //Attributes
            List<String> gender = new List<String>();
            List<String> faceShape = new List<String>();
            List<String> colours = new List<String>();
            List<String> materials = new List<String>();
            List<String> types = new List<String>();
            bool isSunglass = false;
            List<String> sideLengths = new List<String>();
            List<String> faceWidths = new List<String>();
            List<String> brands = new List<String>();
            bool isVari = false;

            //Checks each toggleButton
            foreach (ToggleButton tb in spGender.Children)
            {
                if (tb.IsChecked == true)
                {
                    gender.Add((String)tb.Content);
                }
            }
            browserDetails.gender = gender;
            foreach (ToggleButton tb in spFaceShape.Children)
            {
                if (tb.IsChecked == true)
                {
                    faceShape.Add((String)tb.Content);
                }
            }
            browserDetails.faceShape = faceShape;
            foreach (ToggleButton tb in spColour.Children)
            {
                if (tb.IsChecked == true)
                {
                    colours.Add((String)tb.Content);
                }
            }
            browserDetails.colours = colours;
            foreach (ToggleButton tb in spMaterial.Children)
            {
                if (tb.IsChecked == true)
                {
                    materials.Add((String)tb.Content);
                }
            }
            browserDetails.materials = materials;
            foreach (ToggleButton tb in spSideLength.Children)
            {
                if (tb.IsChecked == true)
                {
                    sideLengths.Add((String)tb.Content);
                }
            }
            foreach (ToggleButton tb in spType.Children)
            {
                if (tb.IsChecked == true)
                {
                    types.Add((String)tb.Content);
                }
            }
            browserDetails.types = types;
            browserDetails.sideLengths = sideLengths;
            foreach (ToggleButton tb in spWidth.Children)
            {
                if (tb.IsChecked == true)
                {
                    faceWidths.Add((String)tb.Content);
                }
            }
            browserDetails.faceWidths = faceWidths;
            foreach (ToggleButton tb in spBrand.Children)
            {
                if (tb.IsChecked == true)
                {
                    brands.Add((String)tb.Content);
                }
            }
            browserDetails.brands = brands;
            if (tbSunglass.IsChecked == true)
                isSunglass = true;
            else
                isSunglass = false;
            browserDetails.isSunglass = isSunglass;
            if (tbVari.IsChecked == true)
                isVari = true;
            else
                isVari = false;
            browserDetails.isVari = isVari;
            //Returns result
            return browserDetails;
        }

        //Used to reset back to original state(at startup)
        private void reset()
        {
            foreach (var stackPanel in stackPanels)
            {
                foreach (ToggleButton button in stackPanel.Children)
                {
                    if (stackPanel.Name != "spSunglass" && stackPanel.Name != "spVari")
                        button.IsChecked = true;
                    else
                        button.IsChecked = false;
                }
            }
        }

        //Used to close all the other stackpanels except the one that was opened
        private void closeOtherStackPanels(Button clickedButton)
        {
            var spName = "sp" + clickedButton.Name.Substring(3);
            foreach (StackPanel stackPanel in stackPanels)
            {
                if (stackPanel.Visibility == Visibility.Visible && stackPanel.Name != spName)
                    stackPanel.Visibility = Visibility.Collapsed;
            }
        }

        //Opens stackpanel for clicked button
        private void btnDropdownToggle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel stackPanel = this.FindName("sp" + button.Name.Substring(3)) as StackPanel;
            if (stackPanel.Visibility == Visibility.Collapsed)
                stackPanel.Visibility = Visibility.Visible;
            else
                stackPanel.Visibility = Visibility.Collapsed;
            closeOtherStackPanels((Button)sender);
        }

        //Toggles main dropdown menu 
        private void toggleButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickEventHandler(sender, e);
        }

        //Calls reset when main button is rightclicked or 'righttapped'
        private void btnMain_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            reset();
            ButtonClickEventHandler(sender, e);
            spMain.Visibility = Visibility.Collapsed;
        }
    }
}
