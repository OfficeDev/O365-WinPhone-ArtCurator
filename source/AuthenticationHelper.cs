// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.Discovery;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
 
namespace O365_WinPhone_ArtCurator
{
    public class AuthenticationHelper
    {
        // Static variable stores the Outlook client so that we don't have to create it more than once.
        public static OutlookServicesClient _outlookClient = null;

        // The ClientID is added as a resource in App.xaml by Visual Studio when you register the app with Office 365. 
        // As a convenience, we load that value into a variable called ClientID. This way the variable 
        // will always be in sync with whatever client id is added to App.xaml.
        private static readonly string ClientID = App.Current.Resources["ida:ClientID"].ToString();
        private static Uri _returnUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();

        // Properties used for communicating with your Windows Azure AD tenant.
        // The AuthorizationUri is added as a resource in App.xaml when you regiter the app with 
        // Office 365. As a convenience, we load that value into a variable called CommonAuthority, adding Common to this Url to signify
        // multi-tenancy. This way it will always be in sync with whatever value is added to App.xaml.
        private static readonly string CommonAuthority = @"https://login.windows.net/Common";
        private static readonly Uri DiscoveryServiceEndpointUri = new Uri("https://api.office.com/discovery/v1.0/me/");
        private const string DiscoveryResourceId = "https://api.office.com/discovery/";

        // Property for storing the authentication context.
        internal static AuthenticationContext AuthenticationContext { get; set; }

        public static async Task<AuthenticationResult> AuthenticateSilently()
        {
            // First, look for the authority used during the last authentication.
            // If that value is not populated, return null, because this means that the 
            // last user disconnected.
            if (String.IsNullOrEmpty(App._settings.LastAuthority))
            {
                Debug.WriteLine("Unable to authenticate silenty (no authority found)."); 
                return null;
            }
            else
            {
                AuthenticationContext = await AuthenticationContext.CreateAsync(App._settings.LastAuthority);

                AuthenticationResult result = await AuthenticationContext.AcquireTokenSilentAsync(DiscoveryResourceId, ClientID);
                return result;
            }
        }

        public static async void BeginAuthentication()
        {
            // First, look for the authority used during the last authentication.
            // If that value is not populated, use CommonAuthority.
            string authority = null;
            if (String.IsNullOrEmpty(App._settings.LastAuthority))
            {
                authority = CommonAuthority;
            }
            else
            {
                authority = App._settings.LastAuthority;
            }

            AuthenticationContext = await AuthenticationContext.CreateAsync(authority);
            AuthenticationContext.AcquireTokenAndContinue(DiscoveryResourceId, ClientID, _returnUri, null);
        }

        // Get an access token for the given context and resource ID silently. We run this only after the user has already been authenticated.
        private static async Task<string> GetTokenHelperAsync(AuthenticationContext context, string resourceId)
        {
            string accessToken = null;
            AuthenticationResult result = null;

            result = await context.AcquireTokenSilentAsync(resourceId, ClientID);

            if (result.Status == AuthenticationStatus.Success)
            {
                accessToken = result.AccessToken;

                // Store values for logged-in user, tenant ID, and authority, so that
                // they can be re-used if the user re-opens the app without disconnecting.
                App._settings.LoggedInUser = result.UserInfo.UniqueId;
                App._settings.UserEmail= result.UserInfo.DisplayableId;
                App._settings.TenantId = result.TenantId;
                App._settings.LastAuthority = context.Authority;

                return accessToken;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks that an OutlookServicesClient object is available. 
        /// </summary>
        /// <returns>The OutlookServicesClient object. </returns>
        public static async Task<OutlookServicesClient> GetOutlookClientAsync(string capability)
        {
            if (_outlookClient != null)
            {
                return _outlookClient;
            }
            else
            {
                try
                {
                    // See the Discovery Service Sample (https://github.com/OfficeDev/Office365-Discovery-Service-Sample)
                    // for an approach that improves performance by storing the discovery service information in a cache.
                    DiscoveryClient discoveryClient = new DiscoveryClient(
                        async () => await GetTokenHelperAsync(AuthenticationContext, DiscoveryResourceId));

                    // Get the specified capability ("Mail").
                    CapabilityDiscoveryResult result =
                        await discoveryClient.DiscoverCapabilityAsync(capability);

                    _outlookClient = new OutlookServicesClient(
                        result.ServiceEndpointUri,
                        async () => await GetTokenHelperAsync(AuthenticationContext, result.ServiceResourceId));

                    return _outlookClient;
                }
                // The following is a list of all exceptions you should consider handling in your app.
                // In the case of this sample, the exceptions are handled by returning null upstream. 
                catch (DiscoveryFailedException dfe)
                {

                    // Discovery failed.
                    Debug.WriteLine("Exception: " + dfe.Message);
                    AuthenticationContext.TokenCache.Clear();
                    return null;
                }
                catch (ArgumentException ae)
                {
                    // Argument exception
                    Debug.WriteLine("Exception: " + ae.Message);
                    AuthenticationContext.TokenCache.Clear();
                    return null;
                }
            }
        }

        /// <summary>
        /// Signs the user out of the service.
        /// </summary>
        public static void SignOut()
        {
            AuthenticationContext.TokenCache.Clear();

            // Clean up all existing clients.
            _outlookClient = null;

            // Clear stored values from last authentication.
            App._settings.TenantId = null;
            App._settings.LastAuthority = null;
        }
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