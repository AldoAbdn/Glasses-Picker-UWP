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
    /// Lets a user pick their favourite materials
    /// </summary>
    public sealed partial class MaterialPage : Page
    {
        //Attributes
        private AppManager manager;
        private List<Material> Materials;

        //Constructor
        public MaterialPage()
        {
            this.InitializeComponent();
            Materials = new List<Material> { new Material("Plastic", "ms-appx:///Images/FrameImages/18G1.jpg"), new Material("Metal", "ms-appx:///Images/FrameImages/21B1.jpg"), new Material("Flex", "ms-appx:///Images/FrameImages/22K1.jpg"), new Material("Titanium", "ms-appx:///Images/FrameImages/22G1.jpg") };
        }

        //Sets manager
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            manager = (AppManager)e.Parameter;
        }

        //Gets selected materials from GridView
        private List<String> getSelectedMaterials()
        {
            List<String> materials = new List<String>();
            var items = gvMaterials.SelectedItems;
            foreach (Material material in items) { 
                materials.Add(material.material);
            }
            return materials;
        }

        //Checks that at least one material has been selected
        //If yes then navigates to next page
        private void continue_Click(object sender, RoutedEventArgs e)
        {
            if (gvMaterials.SelectedItems.Count == 0)
            {
                validationDialog("You must select at least one material");
            }
            else
            {
                var materials = getSelectedMaterials();
                manager.addPatientDetail(Detail.Material, materials);
                manager.navigateMain(typeof(FrameTypePage));
            }
        }

        //Shows dialog
        private async void validationDialog(String message)
        {
            var dialog = new WarningDialog();
            dialog.Title = "Warning";
            dialog.Text = message;
            dialog.SecondaryButtonText = "";
            var result = await dialog.ShowAsync();
        }

        //Let user exit framepicker
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            manager.quitFacePicker();
        }

        private void gvMaterials_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            int rows = 1; int cols = 4;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemWidth = e.NewSize.Width / cols;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemHeight = e.NewSize.Height / rows;
        }
    }
}
