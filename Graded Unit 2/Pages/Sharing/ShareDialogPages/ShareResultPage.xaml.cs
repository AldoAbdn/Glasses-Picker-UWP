using Microsoft.Toolkit.Uwp.Services.Facebook;
using Microsoft.Toolkit.Uwp.Services.Twitter;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Graded_Unit_2.Pages.Sharing.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShareResultPage : Page
    {
        ShareType shareType;

        public ShareResultPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            shareType = (ShareType)e.Parameter;
            if (shareType == ShareType.Twitter)
            {
                showTwitterFeed();
                txtContent.Text = "Tweet Sent";
            }
            else if (shareType == ShareType.Facebook)
            {
                showFacebookFeed();
                txtContent.Text = "Image Posted";
            }
            else
                txtContent.Text = "An Error Occured. If you signed into your social media account, a post may have still been created";
            ccLiveFeed.Visibility = Visibility.Visible;
        }

        //Logs out user
        private async void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (shareType == ShareType.Twitter)
                TwitterService.Instance.Logout();
            else if (shareType == ShareType.Facebook)
                await FacebookService.Instance.LogoutAsync();
            else
            {
                try
                {
                    TwitterService.Instance.Logout();
                    await FacebookService.Instance.LogoutAsync();
                }
                catch
                {

                }
            }
        }

        //Used to show tweet that was sent
        private async void showTwitterFeed()
        {
            try
            {
                ccLiveFeed.ContentTemplate = this.Resources["TweetDataTemplate"] as DataTemplate;
                if (TwitterService.Instance.Provider.LoggedIn)
                {
                    var tweets = await TwitterService.Instance.GetUserTimeLineAsync(TwitterService.Instance.UserScreenName, 1);
                    ccLiveFeed.Content = tweets.ToList<Tweet>()[0];
                }
            }
            catch
            {
                txtContent.Text = "An Error Occured. If you signed into your social media account, a post may have still been created";
            }
        }

        //Used to show image that was posted
        private async void showFacebookFeed()
        {
            try
            {
                ccLiveFeed.ContentTemplate = this.Resources["FacebookPostDataTemplate"] as DataTemplate;
                if (FacebookService.Instance.Provider.LoggedIn)
                {
                    var posts = await FacebookService.Instance.RequestAsync(FacebookDataConfig.MyFeed, 1);
                    ccLiveFeed.Content = posts[0];
                }
            }
            catch
            {
                txtContent.Text = "An Error Occured. If you signed into your social media account, a post may have still been created";
            }

        }
    }
}
