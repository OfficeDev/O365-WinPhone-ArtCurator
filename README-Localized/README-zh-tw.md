# Art Curator for Windows Phone

這個範例會示範如何使用 Outlook 郵件 API 來取得 Office 365 的電子郵件和附件。它針對 [iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator)、[Android](https://github.com/OfficeDev/O365-Android-ArtCurator)、[Web (Angular Web 應用程式)](https://github.com/OfficeDev/O365-Angular-ArtCurator) 和 Windows Phone 建立。請閱讀我們的[媒體上的文件](https://medium.com/office-app-development)。

Art Curator 提供不同的方法來檢視您的收件匣。假設您擁有一家銷售藝術 T 恤的公司。身為公司的擁有人，您會收到大量藝術家的電子郵件，希望您購買他們的設計。請勿使用 Outlook 和開啟每個個別的電子郵件，而是下載附加的圖片，然後開啟它來檢視，您可以先使用 Art Curator 來預覽收件匣的附件 (../限於 .jpg 和 .png 檔案)，以更有效率的方式挑選所要的設計。

[![Art Curator Screenshot](../README Assets/AC_WinPhone.png)](https://youtu.be/4LOvkweDfhY "按一下以查看執行中的範例")

這個範例會示範來自 **Outlook 郵件 API** 的下列作業︰
* [取得資料夾](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetFolders)
* [收到郵件](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Getmessages) (包括篩選和使用選取)
* [取得附件](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetAttachments)
* [更新訊息](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Updatemessages)
* [建立和傳送訊息](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Sendmessages) (包含和不含附件)

<a name="prerequisites"></a>
## 必要條件

此範例需要下列項目： 

 - Windows 8.1
 - Visual Studio 2013 含 Update 3
 - [Office 365 API Tools 1.4.50428.2 版](http://aka.ms/k0534n)
 - [Office 365 開發人員網站](http://aka.ms/ro9c62)
 - [Windows 應用程式開發人員帳戶](https://appdev.microsoft.com/StorePortals/en-us/Account/signup/start)

### 設定範例

請遵循以下步驟來設定範例：

  1. 使用 Visual Studio 2013 開啟 **O365-Windows-Phone-Art-Curator.sln** 檔案。
  2. 建置解決方案。NuGet 套件還原功能會載入 packages.config 檔案中列出的組件。
  3. 登錄及設定應用程式以使用 Office 365 服務 (如下所述)。

### 登錄使用 Office 365 API 的應用程式

您可以透過 Visual Studio 的 Office 365 API 工具來執行此工作 (這會自動執行註冊程序)。請務必從 Visual Studio 圖庫下載並安裝 Office 365 API 工具。

**附註**：如果您在安裝套件時看到任何錯誤 (例如，*找不到 "Microsoft.IdentityModel.Clients.ActiveDirectory"*)，請確定放置解決方案的本機路徑不會過長/過深。將解決方案移靠近您的磁碟機根目錄可解決這個問題。

  1. 在 [方案總管] 視窗中，選擇 [O365-Windows-Phone-Art-Curator]**** -> [新增] -> [連線的服務]。
  2. [服務管理員] 對話方塊會出現。選擇 [Office 365] 並註冊您的應用程式。
  3. 在登入對話方塊中，輸入 Office 365 承租人的使用者名稱和密碼。我們建議您使用 Office 365 開發人員網站。通常，這個使用者名稱的模式為 {username}@{tenant}.onmicrosoft.com。如果您沒有開發人員網站，您可以在 MSDN 權益中取得免費的開發人員網站，或申請免費的試用版。請注意，使用者必須為承租人管理使用者，但對於 Office 365 開發人員網站過程中所建立的承租人，可能已經發生此情況。開發人員帳戶通常僅限於一次登入。
  4. 登入之後，您會看到所有服務清單。一開始不會選取權限，因為應用程式尚未註冊來使用任何服務。
  5. 若要註冊這個範例中使用的服務，請選擇下列權限︰ 
     *(郵件) - *以使用者身分傳送郵件*
     * (郵件) - *讀取和寫入使用者郵件*
  6. 按一下 [服務管理員] 對話方塊中的 [確定]。

<a name="build"></a>
## 執行應用程式

在 Visual Studio 中載入解決方案之後，按下 F5 以建置並部署。

<a name="understand"></a>
## 瞭解程式碼
   
### 限制

目前的版本不包含下列功能。

* .png 和 .jpg 以外的檔案支援
* 處理有多個附件的單一電子郵件
* 分頁 (取得 50 個以上的電子郵件)
* 處理資料夾名稱的唯一性
* 提交資料夾必須是最上層的資料夾 

<a name="questions-and-comments"></a>
## 問題與意見

- 如果您在執行這個範例時有任何問題，請參閱[記錄問題](https://github.com/OfficeDev/O365-WinPhone-ArtCurator/issues)。
- 如需 Office 365 API 的一般問題，請張貼到[堆疊溢位](http://stackoverflow.com/)。請確定以 [office365] 標記您的問題或意見。
 
<a name="additional-resources"></a>
## 其他資源

* [Office 365 API 平台概觀](http://msdn.microsoft.com/office/office365/howto/platform-development-overview)
* [Office 開發中心](http://dev.office.com/)
* [Art Curator for iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator)
* [Art Curator for Android](https://github.com/OfficeDev/O365-Android-ArtCurator)
* [Art Curator for Web](https://github.com/OfficeDev/O365-Angular-ArtCurator)

## 著作權

Copyright (c) Microsoft.著作權所有，並保留一切權利。
