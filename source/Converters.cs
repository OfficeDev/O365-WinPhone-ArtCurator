// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
 
namespace O365_WinPhone_ArtCurator
{
    /// <summary>
    /// Converts a bool to the logical visiblity setting (i.e. true is Visible, false is Collapsed).
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (System.Convert.ToBoolean(value))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts byte data to an ImageSource that can be displayed in XAML. 
    /// </summary>
    public class AttachmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            try
            {
                Message message = value as Message;
                byte[] imageBytes = (message.Attachments[0] as FileAttachment).ContentBytes;

                BitmapImage image = new BitmapImage();
                image.DecodePixelWidth = 300;

                InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
                ms.AsStreamForWrite(imageBytes.Length).Write(imageBytes, 0, imageBytes.Length);
                ms.Seek(0);
                image.SetSource(ms);

                return image;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts DateTime into the format we want to display (derived from Outlook). 
    /// </summary>
    public class DateTimeReceivedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            DateTime date;
            DateTime.TryParse(value.ToString(), out date);

            int hour = 0;
            String xM = "";
            if (date.Hour > 12)
            {
                hour = date.Hour - 12;
                xM = "PM";

                if (date.Hour == 24)
                {
                    xM = "AM";
                }
            }
            else
            {
                hour = date.Hour;
                xM = "AM";

                if (date.Hour == 12)
                {
                    xM = "PM";
                }
            }

            int minute = date.Minute;
            string minuteDisplay = "";
            if (minute < 10)
            {
                minuteDisplay = "0" + minute.ToString();
            }
            else
            {
                minuteDisplay = minute.ToString();
            }

            String formatted = String.Format("{3} {4}/{5}/{6} {0}:{1} {2}", hour, minuteDisplay, xM, date.DayOfWeek, date.Month, date.Day, date.Year);
            return formatted;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
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