// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

using Microsoft.OData.Core;
using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace O365_WinPhone_ArtCurator
{
    /// <summary>
    /// MailHelper wraps the Office 365 SDK methods in a way that supports this app. It helps keep the Office 365 logic out of the page controllers.
    /// </summary>
    public class MailHelper
    {
        private static Boolean isSubscribed = false;

        /// <summary>
        /// Compose and send a new email.
        /// </summary>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="bodyContent">The body of the email.</param>
        /// <param name="recipients">A semicolon separated list of email addresses.</param>
        internal async Task<String> ComposeAndSendMailAsync(string subject,
                                                            string bodyContent,
                                                            string recipients)
        {
            // The identifier of the composed and sent message.
            string newMessageId = string.Empty;

            // Prepare the recipient list
            var toRecipients = new List<Recipient>();
            string[] splitter = { ";" };
            var splitRecipientsString = recipients.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            foreach (string recipient in splitRecipientsString)
            {
                toRecipients.Add(new Recipient
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = recipient.Trim(),
                        Name = recipient.Trim(),
                    },
                });
            }

            // Prepare the draft message.
            var draft = new Message
            {
                Subject = subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Text,
                    Content = bodyContent
                },
                ToRecipients = toRecipients,
                Importance = Importance.Normal
            };

            try
            {
                // Make sure we have a reference to the Outlook Services client.
                var outlookClient = await AuthenticationHelper.GetOutlookClientAsync("Mail");

                // Send the mail.
                await outlookClient.Me.SendMailAsync(draft, true);

                return draft.Id;
            }

            // Catch any exceptions related to invalid OData.
            catch (Microsoft.OData.Core.ODataException ode)
            {
                throw new Exception("We could not send the message: " + ode.Message);
            }
            catch (Exception e)
            {
                throw new Exception("We could not send the message: " + e.Message);
            }
        }

        /// <summary>
        /// Returns the folder ID where user recieves new art emails. 
        /// </summary>
        /// <param name="folderName">The name of the target folder.</param>
        /// <returns>The ID of the target folder.</returns>
        internal async Task<String> GetTargetFolderAsync(String folderName)
        {
            Folder f = new Folder();
            // Get Outlook client to perform operations.
            var outlookClient = await AuthenticationHelper.GetOutlookClientAsync("Mail");

            Debug.WriteLine("Getting folders...");

            // Get the folder that matches folderName.
            var mailFolder = await outlookClient.Me.Folders
                              .Where(_f => _f.DisplayName.Equals(folderName))
                              .ExecuteAsync();

            // Check if folder was found. Return the first one if found. 
            if (mailFolder.CurrentPage.Count == 0)
            {
                Debug.WriteLine("Failed to get folder: {0}.", folderName);
                return null; 
            }
            else
            {
                f = mailFolder.CurrentPage.First() as Folder;
                return f.Id;
            }
        }

        /// <summary>
        /// Returns the emails from the target folder.
        /// </summary>
        /// <param name="folderId">The ID of the target folder.</param>
        /// <returns>A List of Message objects.</returns>
        internal async Task<List<Message>> GetMessagesAsync(String folderId)
        {
            List<Message> messages = new List<Message>();

            try
            {
                // Get Outlook client to perform operations.
                var outlookClient = await AuthenticationHelper.GetOutlookClientAsync("Mail");

                // Set parameters for HTTP request (unread messages with attachments). 
                var mailRequest = outlookClient.Me.Folders[folderId].Messages
                                  .Where(m => m.HasAttachments == true && m.IsRead == false)
                                  .Expand(m => m.Attachments)
                                  .Take(50);

                // Make sure that event handler is only assigned once.
                // Can remove this once the Office 365 SDK is updated.
                // https://github.com/Microsoft/Vipr/issues/73
                if (!isSubscribed)
                {
                    mailRequest.Context.BuildingRequest += Context_BuildingRequest;
                    isSubscribed = true;
                }

                var mail = await mailRequest.ExecuteAsync();

                // Iterate over collection and add to a message List to return.
                foreach (var message in mail.CurrentPage)
                {
                    if (message.Attachments.CurrentPage[0].ContentType == "image/png" ||
                        message.Attachments.CurrentPage[0].ContentType == "image/jpeg" ||
                        message.Attachments.CurrentPage[0].ContentType == "image/jpg")
                    {
                        messages.Insert(0, message as Message);
                    }
                }

                Debug.WriteLine("Successfully got messages.");
                return messages; 
            }

            catch (Exception e)
            {
                Debug.WriteLine("Unable to get messages: " + e.Message);
                return null; 
            }
        }

        /// <summary>
        /// Marks the email as read.
        /// </summary>
        /// <param name="messageId">The ID of the message to mark as read.</param>
        internal async Task<Boolean> MarkAsReadAsync(String messageId)
        {
            try
            {
                var outlookClient = await AuthenticationHelper.GetOutlookClientAsync("Mail");
                IMessage message = await outlookClient.Me.Messages.GetById(messageId).ExecuteAsync();

                // Mark message as read.
                message.IsRead = true;
                await message.UpdateAsync();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to mark message as read: " + e.Message);
                return false; 
            }
        }

        /// <summary>
        /// Creates a draft message and stores it in Drafts.
        /// </summary>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="RecipientAddress"></param>
        /// <returns>The ID of the draft.</returns>
        internal async Task<string> CreateDraftAsync(
            string Subject,
            string Body,
            string RecipientAddress)
        {

            // Make sure we have a reference to the Outlook Services client.
            var outlookClient = await AuthenticationHelper.GetOutlookClientAsync("Mail");

            ItemBody body = new ItemBody
            {
                Content = Body,
                ContentType = BodyType.HTML
            };
            List<Recipient> toRecipients = new List<Recipient>();
            toRecipients.Add(new Recipient
            {
                EmailAddress = new EmailAddress
                {
                    Address = RecipientAddress
                }
            });
            Message draftMessage = new Message
            {
                Subject = Subject,
                Body = body,
                ToRecipients = toRecipients,
                Importance = Importance.Normal
            };

            // Save the draft message. Saving to Me.Messages saves the message in the Drafts folder.
            await outlookClient.Me.Messages.AddMessageAsync(draftMessage);

            Debug.WriteLine("Created draft: " + draftMessage.Id);

            return draftMessage.Id;

        }

        /// <summary>
        /// Method to add a file attachment to a saved draft.
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="fileContent"></param>
        /// <returns>Success.</returns>
        internal async Task AddFileAttachmentAsync(string MessageId, MemoryStream fileContent, string fileName)
        {
            // Make sure we have a reference to the Outlook Services client.
            var outlookClient = await AuthenticationHelper.GetOutlookClientAsync("Mail");

            var attachment = new FileAttachment();

            attachment.ContentBytes = fileContent.ToArray();
            attachment.Name = fileName;
            attachment.Size = fileContent.ToArray().Length;
            attachment.ContentType = "image/png";

            try
            {
                var storedMessage = outlookClient.Me.Messages.GetById(MessageId);
                await storedMessage.Attachments.AddAttachmentAsync(attachment);
                await storedMessage.SendAsync();
                Debug.WriteLine("Added attachment to message: " + MessageId);
                return;
            }
            catch (ODataErrorException ex)
            {
                // GetById will throw an ODataErrorException when the 
                // item with the specified Id can't be found in the contact store on the server. 
                Debug.WriteLine(ex.Message);
                return;
            }

        }

        /// <summary>
        /// Event handler to add a cache parameter to the request URI to get new results every time.
        /// </summary>
        void Context_BuildingRequest(object sender, Microsoft.OData.Client.BuildingRequestEventArgs e)
        {
            if (e.RequestUri.ToString().Contains("cache") || !e.RequestUri.ToString().Contains("$expand=Attachments"))
            {
                return;
            }
            else
            {
                e.RequestUri = new Uri(e.RequestUri.ToString() + "&cache=" + Guid.NewGuid().ToString());
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