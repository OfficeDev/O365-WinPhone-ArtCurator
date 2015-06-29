// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
 
namespace O365_WinPhone_ArtCurator
{
    public class AppSettings
    {
        ApplicationDataContainer _settings;

        // The key names of our settings.
        const string UserEmailKeyName = "LoggedInUserEmail";
        const string LastAuthorityKeyName = "LastAuthority";
        const string TenantIdKeyName = "TenantId";
        const string LoggedInUserKeyName = "LoggedInUser";
        const string TargetFolderKeyName = "TargetFolder";
        const string LikeMarkAsReadKeyName = "LikeMarkAsRead";
        const string LikeRespondKeyName = "LikeRespond";
        const string LikeResponseKeyName = "LikeResponse";
        const string DislikeMarkAsReadKeyName = "DislikeMarkAsRead";
        const string DislikeRespondKeyName = "DislikeRespond";
        const string DislikeResponseKeyName = "DislikeResponse";

        // The default value of our settings.
        const string UserEmailSettingDefault = "";
        const string LastAuthorityDefault = "";
        const string TenantIdDefault = "";
        const string LoggedInUserDefault = "";
        const string TargetFolderDefault = "Inbox";
        const bool LikeMarkAsReadDefault = true;
        const bool LikeRespondDefault = true;
        const string LikeResponseDefault = "Excellent submission. Please email me at your convenience to discuss a sale.";
        const bool DislikeMarkAsReadDefault = true;
        const bool DislikeRespondDefault = true;
        const string DislikeResponseDefault = "This submission isn't what I'm looking for. Thank you anyway.";

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public AppSettings()
        {
            // Get the settings for this application.
            _settings = ApplicationData.Current.LocalSettings;
        }

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        private bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (_settings.Values.ContainsKey(Key))
            {
                // If the value has changed
                if (_settings.Values[Key] != value)
                {
                    // Store the new value
                    _settings.Values[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                _settings.Values.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        private T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (_settings.Values.ContainsKey(Key))
            {
                value = (T)_settings.Values[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }
            return value;
        }

        /// <summary>
        /// Gets or sets the last logged in user email.
        /// </summary>
        public string UserEmail
        {
            get
            {
                return GetValueOrDefault<string>(UserEmailKeyName, UserEmailSettingDefault);
            }
            set
            {
                AddOrUpdateValue(UserEmailKeyName, value);
            }
        }

        /// <summary>
        /// Gets or sets the last authority against which the user signed in.
        /// </summary>
        public string LastAuthority
        {
            get
            {
                return GetValueOrDefault<string>(LastAuthorityKeyName, LastAuthorityDefault);
            }
            set
            {
                AddOrUpdateValue(LastAuthorityKeyName, value);
            }
        }

        /// <summary>
        /// Gets or sets the tenant Id against which the user signed in.
        /// </summary>
        public string TenantId
        {
            get
            {
                return GetValueOrDefault<string>(TenantIdKeyName, TenantIdDefault);
            }
            set
            {
                AddOrUpdateValue(TenantIdKeyName, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the last user to sign in. 
        /// </summary>
        public string LoggedInUser
        {
            get
            {
                return GetValueOrDefault<string>(LoggedInUserKeyName, LoggedInUserDefault);
            }
            set
            {
                AddOrUpdateValue(LoggedInUserKeyName, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the target folder to look in for new messages. 
        /// </summary>
        public string TargetFolder
        {
            get
            {
                return GetValueOrDefault<string>(TargetFolderKeyName, TargetFolderDefault);
            }

            set
            {
                AddOrUpdateValue(TargetFolderKeyName, value);
            }
        }

        /// <summary>
        /// Gets or sets the saved setting to mark an email as read or not when liked. 
        /// </summary>
        public bool LikeMarkAsRead
        {
            get
            {
                return GetValueOrDefault<bool>(LikeMarkAsReadKeyName, LikeMarkAsReadDefault);
            }

            set
            {
                AddOrUpdateValue(LikeMarkAsReadKeyName, value);
            }
        }

        /// <summary>
        /// Gets or sets the saved setting to send a response or not when liked. 
        /// </summary>
        public bool LikeRespond
        {
            get
            {
                return GetValueOrDefault<bool>(LikeRespondKeyName, LikeRespondDefault);
            }

            set
            {
                AddOrUpdateValue(LikeRespondKeyName, value);
            }
        }

        /// <summary>
        /// Gets or sets the saved response when responding with a "like". 
        /// </summary>
        public string LikeResponse
        {
            get
            {
                return GetValueOrDefault<string>(LikeResponseKeyName, LikeResponseDefault);
            }

            set
            {
                AddOrUpdateValue(LikeResponseKeyName, value);
            }
        }

        /// <summary>
        /// Gets or sets the saved setting to mark an email as read or not when disliked. 
        /// </summary>
        public bool DislikeMarkAsRead
        {
            get
            {
                return GetValueOrDefault<bool>(DislikeMarkAsReadKeyName, DislikeMarkAsReadDefault);
            }

            set
            {
                AddOrUpdateValue(DislikeMarkAsReadKeyName, value);
            }
        }

        /// <summary>
        /// Gets or sets the saved setting to send a response or not when disliked. 
        /// </summary>
        public bool DislikeRespond
        {
            get
            {
                return GetValueOrDefault<bool>(DislikeRespondKeyName, DislikeRespondDefault);
            }

            set
            {
                AddOrUpdateValue(DislikeRespondKeyName, value);
            }
        }

        /// <summary>
        /// Gets or sets the saved response when responding with a "dislike". 
        /// </summary>
        public string DislikeResponse
        {
            get
            {
                return GetValueOrDefault<string>(DislikeResponseKeyName, DislikeResponseDefault);
            }

            set
            {
                AddOrUpdateValue(DislikeResponseKeyName, value);
            }
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