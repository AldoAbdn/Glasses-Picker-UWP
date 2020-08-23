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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Graded_Unit_2.CustomControls
{
    /// <summary>
    /// I use this to display a list of images. Radio buttons let you change to a certain image
    /// </summary>
    public sealed partial class PhotoViewer : UserControl
    {
        //Dependency properties
        public List<ImageSource> Images
        {
            get { return (List<ImageSource>)GetValue(ImagesProperty); }
            set { SetValue(ImagesProperty, value); generateImageSelectors(); }
        }
        public static readonly DependencyProperty ImagesProperty = DependencyProperty.Register("Images", typeof(List<ImageSource>), typeof(PhotoViewer), null);

        public PhotoViewer()
        {
            this.InitializeComponent();
            generateImageSelectors();
        }

        private void generateImageSelectors()
        { 
            if (Images != null)
            {
                for (var i = 0; i < Images.Count; i++)
                {
                    //Generates column definition for each
                    ColumnDefinition column = new ColumnDefinition();
                    column.Width = new GridLength(1, GridUnitType.Star);
                    radioButtonGrid.ColumnDefinitions.Add(column);
                    //Add radio button
                    RadioButton radioButton = new RadioButton();
                    radioButtonGrid.Children.Add(radioButton);
                    Grid.SetColumn(radioButton, i);
                    radioButton.HorizontalAlignment = HorizontalAlignment.Center;
                    radioButton.GroupName = "imageSelectors";
                    radioButton.Content = Images[i];
                    radioButton.Style = (Style)Application.Current.Resources["imageSelector"];
                    radioButton.Checked += imageSelector_Checked;
                    if (i == 0)
                    {
                        radioButton.IsChecked = true;
                    }
                }
            }
        }

        private void imageSelector_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            currentImage.Source = (ImageSource)radioButton.Content;
        }
    }
}
