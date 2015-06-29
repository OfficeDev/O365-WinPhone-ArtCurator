// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

using O365_WinPhone_ArtCurator.Common;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
 
namespace O365_WinPhone_ArtCurator
{
    public sealed partial class MainPage : Page, IWebAuthenticationContinuable
    {
        private NavigationHelper navigationHelper;
        private AppViewModel _viewModel;

        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);

            // Instantiate and set ViewModel.
            _viewModel = App._viewModel;
            this.DataContext = _viewModel;
            myAppBar.DataContext = _viewModel;
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                // Try to authenticate silently (using cached values). 
                bool authenticatedSilently = await _viewModel.AuthenticateSilenty();
                
                // If authenticated, get emails to display.
                if (authenticatedSilently)
                {
                    _viewModel.GetMessages();
                }
            }
            else if (_viewModel.RefreshPage)
            {
                _viewModel.RefreshPage = false; 
                _viewModel.GetMessages();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        #region IWebAuthenticationContinuable implementation

        // This method is automatically invoked when the application is reactivated after an authentication interaction through WebAuthenticationBroker. 
        // In order to make this method work as expected, you need to create a ContinuationManager object in App.xaml.cs.
        // See http://www.cloudidentity.com/blog/2014/08/28/use-adal-to-connect-your-universal-apps-to-azure-ad-or-adfs/ for
        // more information.
        public async void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
        {
            // Pass the authentication interaction results to ADAL, which will conclude the token acquisition operation and invoke the callback specified in AcquireTokenAndContinue (in AuthenticationHelper).
            AuthenticationResult result = await AuthenticationHelper.AuthenticationContext.ContinueAcquireTokenAsync(args);

            if (result.Status == AuthenticationStatus.Success)
            {
                Debug.WriteLine("User authenticated successfully.");

                // Store authentication details for silent authentication next time.
                App._settings.TenantId = result.TenantId;
                App._settings.LastAuthority = AuthenticationHelper.AuthenticationContext.Authority;
                App._settings.UserEmail = result.UserInfo.DisplayableId;

                // Hide "Connect to Office 365" button.
                _viewModel.ShowConnectUI = false;
                _viewModel.ShowMailUI = true;
                _viewModel.ShowAppBar = true;

                // Now that user has authenticated, get emails to display.
                _viewModel.GetMessages();
            }
            else
            {
                Debug.WriteLine(result.ErrorDescription);
            }

        }

        #endregion

        #region UI interaction methods

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Authenticate();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GetMessages();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        private void Attachment_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Set the tapped message as the selected message.
            Message selected = (sender as StackPanel).DataContext as Message;
            _viewModel.SelectedMessage = selected;

            // Navigate to details page to show details about message. 
            this.Frame.Navigate(typeof(DetailsPage));
        }

        private async void PopulateButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.PopulateInbox();
            SubmissionsSentMessage.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        #endregion              
        
    }
}

//********************************************************* 
// 
// O365-WinPhone-ArtCurator, https://github.com/OfficeDev/O365-WinPhone-ArtCurator
//
// Copyright (c) Microsoft Corporation
// All rights reserved. 
//
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// ""Software""), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:

// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
//********************************************************* 