// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
 
namespace O365_WinPhone_ArtCurator
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private MailHelper _mailHelper;

        public AppViewModel()
        {
            _mailHelper = new MailHelper();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool _showConnectUI; 
        public bool ShowConnectUI
        {
            get
            {
                return _showConnectUI;
            }
            set
            {
                _showConnectUI = value;
                RaisePropertyChanged();
            }
        }

        private bool _showMailUI;
        public bool ShowMailUI
        {
            get
            {
                return _showMailUI;
            }
            set {
                _showMailUI = value;
                RaisePropertyChanged();
            }
        }

        private bool _showErrorUI;
        public bool ShowErrorUI
        {
            get
            {
                return _showErrorUI;
            }
            set
            {
                _showErrorUI = value;
                RaisePropertyChanged();

                if (value)
                {
                    ShowConnectUI = false;
                    ShowMailUI = false;

                    RaisePropertyChanged("ShowConnectUI");
                    RaisePropertyChanged("ShowMailUI");
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged();
            }
        }

        private bool _showPopulateButton;
        public bool ShowPopulateButton
        {
            get
            {
                return _showPopulateButton;
            }
            set
            {
                _showPopulateButton = value;
                RaisePropertyChanged();
            }
        }

        private bool _showAppBar;
        public bool ShowAppBar
        {
            get
            {
                return _showAppBar;
            }
            set
            {
                _showAppBar = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Message> _emails = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages
        {
            get
            {
                return _emails;
            }
            set
            {
                _emails = value;
                RaisePropertyChanged(); 
            }
        }

        private bool _isWaiting;
        public bool IsWaiting
        {
            get
            {
                return _isWaiting;
            }
            set
            {
                _isWaiting = value;
                RaisePropertyChanged();
                RaisePropertyChanged("EnableControl");
            }
        }

        public bool EnableControl
        {
            get
            {
                return !_isWaiting;
            }
        }

        private bool _refreshPage;
        public bool RefreshPage
        {
            get
            {
                return _refreshPage;
            }
            set
            {
                _refreshPage = value;
            }
        }

        private Message _selectedMessage;
        public Message SelectedMessage
        {
            get
            {
                return _selectedMessage;
            }
            set
            {
                _selectedMessage = value;
                RaisePropertyChanged("SelectedMessage"); 
            }
        }

        /// <summary>
        /// Attempts to authenticate silently by using a cached token, thus
        /// bringing the user right to the app without having to go through the
        /// authentication process again.
        /// </summary>
        /// <returns>Whether or not the authentication was successful.</returns>
        public async Task<bool> AuthenticateSilenty()
        {
            AuthenticationResult result = await AuthenticationHelper.AuthenticateSilently();

            // If silent authentication doesn't work, show the "Connect to Office 365" button.
            if (result != null && result.Status == AuthenticationStatus.Success)
            {
                Debug.WriteLine("Successfully authenticated user silently.");
                ShowConnectUI = false;
                ShowMailUI = true;
                ShowAppBar = true;
                return true;
            }
            else
            {
                Debug.WriteLine("Unable to authenticate user silently.");
                ShowConnectUI = true;
                ShowMailUI = false;
                ShowAppBar = false;
                return false; 
            }
        }

        /// <summary>
        /// Kicks off in-browser authentication flow. 
        /// </summary>
        public void Authenticate()
        {
            AuthenticationHelper.BeginAuthentication(); 
        }

        /// <summary>
        /// Gets messages from the model. 
        /// </summary>
        public async void GetMessages()
        {
            // Clear emails that we've already gotten.
            Messages.Clear();

            // Show progress bar.
            IsWaiting = true;

            // Get target folder ID and then fetch emails.
            string targetFolder = App._settings.TargetFolder;
            string targetFolderId = await _mailHelper.GetTargetFolderAsync(targetFolder);

            if (targetFolderId != null)
            {
                if (ShowErrorUI)
                {
                    ShowErrorUI = false;
                }

                Messages = new ObservableCollection<Message>(await _mailHelper.GetMessagesAsync(targetFolderId));

                if (Messages == null)
                {
                    ErrorMessage = "Unable to get messages."; 
                    ShowErrorUI = true;
                }
                else if (Messages.Count == 0)
                {
                    ErrorMessage = "No new submissions in this folder.";
                    ShowErrorUI = true;
                    ShowPopulateButton = true;
                }
                else
                {
                    ShowErrorUI = false;
                    ShowPopulateButton = false; 
                    ShowMailUI = true;
                }
            }
            else
            {
                ErrorMessage = "Unable to find the requested folder.";
                ShowErrorUI = true;
                ShowPopulateButton = false;
            }
            
            // Hide progress bar. 
            IsWaiting = false; 
        }

        /// <summary>
        /// Respond based on actionType ("Like" or "Dislike") and settings.
        /// </summary>
        /// <param name="actionType">"Like" or "Dislike".</param>
        public async Task<bool> Respond(string actionType)
        {
            bool markAsRead;
            bool respond;
            string response;

            if (actionType.Equals("Like"))
            {
                markAsRead = App._settings.LikeMarkAsRead;
                respond = App._settings.LikeRespond;
                response = App._settings.LikeResponse;
            }
            else
            {
                markAsRead = App._settings.DislikeMarkAsRead;
                respond = App._settings.DislikeRespond;
                response = App._settings.DislikeResponse;
            }

            // Show the progress bar.
            IsWaiting = true;

            if (markAsRead)
            {
                Debug.WriteLine("Marking message as read...");

                // Mark message as read.
                bool success = await _mailHelper.MarkAsReadAsync(SelectedMessage.Id);

                if (success)
                {
                    Messages.Remove(SelectedMessage);

                    if (Messages.Count == 0)
                    {
                        ErrorMessage = "No new submissions in your inbox.";
                        ShowErrorUI = true;
                        ShowPopulateButton = true; 
                    }
                }
                else
                {
                    Debug.WriteLine("Failed to mark message as read. Check app permissions."); 
                }
            }

            if (respond)
            {
                Debug.WriteLine("Replying to message..."); 

                // Create a reply.
                string subject = "RE: " + SelectedMessage.Subject;
                string body = response;
                string recipient = SelectedMessage.From.EmailAddress.Address;

                // Send a reply.
                await _mailHelper.ComposeAndSendMailAsync(subject, body, recipient); 
            }

            // Hide the progress bar.
            IsWaiting = false;

            return true;
        }

        /// <summary>
        /// Send three emails with attachments to the user's inbox.
        /// 
        /// This is done just to provide developers with sample content to demo the app properly. 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> PopulateInbox()
        {
            Debug.WriteLine("Populating inbox...");
            string[] localImagePaths = new string[9] { "ms-appx:///Assets/Aero.png", "ms-appx:///Assets/Bird-art.png", "ms-appx:///Assets/Facial.png", "ms-appx:///Assets/Fearther-art.png", "ms-appx:///Assets/guitar-art.png", "ms-appx:///Assets/Leakage.png", "ms-appx:///Assets/Mountain-art.png", "ms-appx:///Assets/MS Robot.png", "ms-appx:///Assets/Win.png" };

            IsWaiting = true;

            foreach (string localImagePath in localImagePaths)
            {
                var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(localImagePath, UriKind.Absolute));
                string fileName = (file as Windows.Storage.StorageFile).Name;
                StorageFile localImage = await StorageFile.GetFileFromApplicationUriAsync(new Uri(localImagePath, UriKind.Absolute));

                // Create the draft message and get its ID back to use in the AddFileAttachmentAsync function.
                string draftId = await _mailHelper.CreateDraftAsync(fileName, "Check out " + fileName + "!", App._settings.UserEmail);

                // Add the image bytes to the draft that was just created and send it.
                using (var fileStream = await localImage.OpenStreamForWriteAsync())
                {
                    var memoryStream = new MemoryStream();
                    await fileStream.CopyToAsync(memoryStream);

                    await _mailHelper.AddFileAttachmentAsync(draftId, memoryStream, fileName); 
                }
            }

            IsWaiting = false;

            return true;      
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
