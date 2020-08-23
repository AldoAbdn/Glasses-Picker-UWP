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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages.PrescriptionPages
{
    /// <summary>
    /// Dialog used to edit VADistance and VANear of prescription
    /// </summary>
    public sealed partial class PrescriptionDialog : ContentDialog
    {
        //Dependency properties
        public static readonly DependencyProperty VADistanceProperty = DependencyProperty.Register(
            "VADistance", typeof(string), typeof(PrescriptionDialog), new PropertyMetadata(default(string)));

        public string VADistance
        {
            get { return (string)GetValue(VADistanceProperty); }
            set { SetValue(VADistanceProperty, value); }
        }

        public static readonly DependencyProperty VANearProperty = DependencyProperty.Register(
           "VANear", typeof(string), typeof(PrescriptionDialog), new PropertyMetadata(default(string)));

        public string VANear
        {
            get { return (string)GetValue(VANearProperty); }
            set { SetValue(VANearProperty, value); }
        }

        //Constructor
        public PrescriptionDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
