using Microsoft.Toolkit.Uwp.Services.Facebook;
using Microsoft.Toolkit.Uwp.Services.Twitter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Compression;
using Windows.Graphics.Imaging;
using System.Net.NetworkInformation;
using Graded_Unit_2.Pages.Sharing.Pages;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages.Sharing
{
    /// <summary>
    /// Lets user pick Twitter or Facebook to share images
    /// </summary>
    public sealed partial class ShareDialog : ContentDialog
    {
        //Attributes
        public StorageFile image;
        public bool shareCancelled = false;

        //Constructor
        public ShareDialog(StorageFile image)
        {
            this.InitializeComponent();
            this.image = image;
            shareFrame.Navigate(typeof(ShareSelectPage), image);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        //Logs out user
        private async void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                TwitterService.Instance.Logout();
                await FacebookService.Instance.LogoutAsync();
            }
            finally
            {
                if (shareFrame.CurrentSourcePageType == typeof(ShareSelectPage))
                    shareCancelled = true;
            }
        }

        //Since process is async if a user signs in and then exits the dialog, a post may still be created
        //So this is just a warning explaining that 
        private async void ContentDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            if (shareCancelled)
            {
                var dialog = new WarningDialog();
                dialog.Title = "Share";
                dialog.Text = "Share Cancelled. If you signed into your social media account, a post may have still been created";
                dialog.PrimaryButtonText = "";
                await dialog.ShowAsync();
            }
        }
    }
}
