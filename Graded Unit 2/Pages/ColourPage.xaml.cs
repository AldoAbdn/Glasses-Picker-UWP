using Graded_Unit_2.CustomControls;
using Graded_Unit_2.Models;
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
    /// Used to get users favourite colours
    /// </summary>
    public sealed partial class ColourPage : Page
    {
        //Attributes
        private AppManager manager;
        private List<Colour> Colours;

        //Constructor
        public ColourPage()
        {
            this.InitializeComponent();
            Colours = new List<Colour> { new Colour("Red", "Red"), new Colour("Green", "Green"), new Colour("Purple", "Purple"), new Colour("Pink", "Pink"), new Colour("Black", "Black"), new Colour("#FFCD7F32", "Bronze"), new Colour("#FFFFDF00", "Gold"), new Colour("#FFC8A2C8", "Lilac"), new Colour("Silver", "Silver"), new Colour("Blue", "Blue"), new Colour("#FFDE5D83", "Blush"), new Colour("Brown", "Tort"), new Colour("#FFA98307", "Honey"), new Colour("DarkBlue", "Navy"), new Colour("Gray", "Gray"), new Colour("LightGreen", "Olive"), new Colour("Brown", "Brown"), new Colour("#FF8E9294", "Pewter"),  new Colour("#FFB784A7", "Mauve"), new Colour("#FFCC9966", "Caramel"), new Colour("#FF292E37", "Gun"), new Colour("Green", "Green"), new Colour("#FF685642", "Tobacco"), new Colour("#FF800020", "Burgundy")};
        }

        //Sets manager
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
        }

        //Gets selected colours from GridView
        private List<String> getSelectedColours()
        {
            List<String> colours = new List<string>();
            var items = gvColours.SelectedItems;
            foreach(Colour colour in items)
            {
                //Gets the instance of ColourItem that grid view item is bound to
                //Gets Colour model that values for ColourItem are bound to
                //And then accesses colour attribute of that 
                colours.Add(colour.colour);
            }
            return colours;
        }

        //Sets patientDetails colours attributes, or shows dialog if no colours have been clicked
        private void continue_Click(object sender, RoutedEventArgs e)
        {
            if (gvColours.SelectedItems.Count == 0)
            {
                validationDialog("You must enter at least one colour");
            }
            else
            {
                List<String> colours = getSelectedColours();
                manager.addPatientDetail(Detail.Colour, colours);
                manager.navigateMain(typeof(MaterialPage));
            }
        }

        //Used to show a dialog
        private async void validationDialog(String message)
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = message;
            dialog.SecondaryButtonText = "";
            var result = await dialog.ShowAsync();
        }

        //Used to exit frame picker
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            manager.quitFacePicker();
        }

        private void gvColours_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            int columns; int rows;
            if (gridView.ActualHeight > gridView.ActualWidth)
            {
                rows = 6; columns = 4;
            }
            else
            {
                rows = 4; columns = 6;
            }
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemWidth = e.NewSize.Width / columns;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemHeight = e.NewSize.Height / rows - 1;
        }
    }
}
