using Microsoft.Toolkit.Uwp.Services.Facebook;
using Microsoft.Toolkit.Uwp.Services.Twitter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages.Sharing.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShareSelectPage : Page
    {
        //Attributes
        private StorageFile image;
        private ShareType shareType = ShareType.Error;

        public ShareSelectPage()
        {
            this.InitializeComponent();
            //NEW TO ME
            TwitterService.Instance.Initialize("6GWfkd8FNvsoOkIIcvtkO27As", "eNXD7lxTEIejgHHUlNbwHPHvSYhSnrEscFnkb5EmZe9vgs22Ne", "http://ProjectTheia.Neckbearproductions.com");
            FacebookOAuthTokens facebookOAuthTokens = new FacebookOAuthTokens();
            facebookOAuthTokens.AppId = "238666966599408";
            facebookOAuthTokens.WindowsStoreId = "s-1-15-2-2776648472-1957620464-412982307-1569838514-1341643823-2232788273-4259109485";
            var initialized = FacebookService.Instance.Initialize(facebookOAuthTokens);
        }

        //Gets passed in image to be shared
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            image = (StorageFile)e.Parameter;
        }

        //Logs into twitter, posts image using API
        //NEW TO ME
        private async void btnTwitter_Click(object sender, RoutedEventArgs e)
        {
            //Disables cancel and shows load spinner
            loading();
            var posted = false;
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    // Post tweet with picture from App
                    var stream = (IRandomAccessStream)await image.OpenReadAsync();

                    posted = await TwitterService.Instance.TweetStatusAsync("Shared on Project Theia, Check out this frame!", stream);
                }
            }
            catch
            {
                Frame.Navigate(typeof(ShareResultPage), shareType);
            }
            finally
            {
                //Checks if posted
                if (posted)
                    shareType = ShareType.Twitter;

                Frame.Navigate(typeof(ShareResultPage), shareType);
            }
        }

        //Logs into facebook, Posts image using API
        //NEW TO ME
        private async void btnFacebook_Click(object sender, RoutedEventArgs e)
        {
            //Disables cancel and shows load spinner
            loading(5);
            var posted = false;
            try
            {
                //check if connected to internet
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    // Post a message on your wall using Facebook Dialog
                    var stream = await image.OpenReadAsync();
                    var result = await FacebookService.Instance.PostPictureToFeedAsync("Project Theia Share", "Shared via project Theia", stream);
                    if (result != null)
                        posted = true;
                }
            }
            catch
            {
                Frame.Navigate(typeof(ShareResultPage), shareType);
            }
            finally
            {             //Checks if posted
                if (posted)
                {
                    shareType = ShareType.Facebook;
                }
                Frame.Navigate(typeof(ShareResultPage), shareType);
            }
        }

        //Used to show and hide load spinner
        private void loading(int pbMax = 6)
        {
            prShare.IsActive = true;
            if (prShare.Visibility == Visibility.Visible)
            {
                prShare.Visibility = Visibility.Collapsed;
                btnFacebook.Visibility = Visibility.Visible;
                btnTwitter.Visibility = Visibility.Visible;
            }
            else
            {
                prShare.Visibility = Visibility.Visible;
                btnFacebook.Visibility = Visibility.Collapsed;
                btnTwitter.Visibility = Visibility.Collapsed;
            }
        }
    }
}
