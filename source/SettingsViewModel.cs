// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
 
namespace O365_WinPhone_ArtCurator
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        AppSettings _appSettings;

        public SettingsViewModel()
        {
            _appSettings = App._settings;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Email
        {
            get
            {
                return _appSettings.UserEmail;
            }
        }

        public string TargetFolder
        {
            get
            {
                return _appSettings.TargetFolder; 
            }
            set
            {
                _appSettings.TargetFolder = value;
                App._viewModel.RefreshPage = true; 
            }
        }

        public bool LikeMarkAsRead
        {
            get
            {
                return _appSettings.LikeMarkAsRead;
            }
            set
            {
                _appSettings.LikeMarkAsRead = value;
            }
        }

        public bool LikeRespond
        {
            get
            {
                return _appSettings.LikeRespond;
            }
            set
            {
                _appSettings.LikeRespond = value;
                RaisePropertyChanged("LikeRespond");
            }
        }

        public string LikeResponse
        {
            get
            {
                return _appSettings.LikeResponse;
            }
            set
            {
                _appSettings.LikeResponse = value;
            }
        }

        public bool DislikeMarkAsRead
        {
            get
            {
                return _appSettings.DislikeMarkAsRead;
            }
            set
            {
                _appSettings.DislikeMarkAsRead = value;
            }
        }

        public bool DislikeRespond
        {
            get
            {
                return _appSettings.DislikeRespond;
            }
            set
            {
                _appSettings.DislikeRespond = value;
                RaisePropertyChanged("DislikeRespond");
            }
        }

        public string DislikeResponse
        {
            get
            {
                return _appSettings.DislikeResponse;
            }
            set
            {
                _appSettings.DislikeResponse = value;
            }
        }

        public void SignOut()
        {
            AuthenticationHelper.SignOut();
            App._viewModel.ShowErrorUI = false;
            App._viewModel.ShowMailUI = false;
            App._viewModel.ShowAppBar = false;
            App._viewModel.ShowConnectUI = true;
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