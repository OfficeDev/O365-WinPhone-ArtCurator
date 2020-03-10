---
page_type: sample
products:
- office-outlook
- office-365
languages:
- csharp
extensions:
  contentType: samples
  createdDate: 6/26/2015 3:03:02 PM
---
# Art Curator for Windows Phone

本示例演示如何使用 Outlook 邮件 API 从 Office 365 获取电子邮件和附件。它针对 [iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator)、[Android](https://github.com/OfficeDev/O365-Android-ArtCurator)、[Web（Angular Web 应用）](https://github.com/OfficeDev/O365-Angular-ArtCurator)和 Windows Phone 构建。查看 [Medium](https://medium.com/office-app-development) 中的文章。

Art Curator 提供了一种不同的方式来查看收件箱。想象您拥有一家销售艺术 T 恤的公司。作为公司的所有者，您会收到大量艺术家发送的电子邮件，其中附有他们希望您购买的设计。您可以使用 Art Curator 让您以更加高效的方式先预览收件箱的附件（限于 .jpg 和 .png 文件）视图以选取您喜欢的设计，而不是使用 Outlook 打开每个单独的电子邮件，下载随附的图片，然后打开图片进行查看。

[![Art Curator Screenshot\](./README Assets/AC\_WinPhone.png)](https://youtu.be/4LOvkweDfhY "单击查看活动示例。")

此示例演示了 **Outlook Mail API** 中的以下操作：
* [获取文件夹](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetFolders)
* [获取邮件](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Getmessages) （包括筛选和使用选择）
* [获取附件](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetAttachments)
* [更新邮件](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Updatemessages)
* [创建和发送邮件](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Sendmessages) （带或不带附件） 

<a name="prerequisites"></a>
## 先决条件

此示例要求如下：  

  - Windows 8.1
  - Visual Studio 2013 （含 Update 3）
  - [Office 365 API 工具版本 1.4.50428.2](http://aka.ms/k0534n)
  - [Office 365 开发人员网站](http://aka.ms/ro9c62)
  - [Windows 应用开发人员帐户](https://appdev.microsoft.com/StorePortals/en-us/Account/signup/start)

### 配置示例

执行以下步骤以配置示例。

   1. 使用 Visual Studio 2013 打开 **O365-Windows-Phone-Art-Curator.sln** 文件。
   2. 构建解决方案。NuGet 程序包还原功能将加载 packages.config 文件中列出的程序集。
   3. 注册和配置应用以使用 Office 365 服务（详细说明如下）。

### 注册应用以使用 Office 365 API

您可以通过用于 Visual Studio 的 Office 365 API 工具（它可以自动执行注册过程）执行此操作。请务必从 Visual Studio 库下载和安装 Office 365 API 工具。

**注意**：如果您在安装程序包时发现任何错误（例如，*找不到“Microsoft.IdentityModel.Clients.ActiveDirectory”*），请确保放置该解决方案的本地路径不太长/不太深。将解决方案移动到更接近驱动器根目录的位置可以解决此问题。

   1. 在“解决方案资源管理器”窗口中，选择“**O365-Windows-Phone-Art-Curator** 项目”->“添加”->“连接的服务”。
   2. 会出现服务管理器对话框。选择 Office 365 并注册您的应用。
   3. 在登录对话框中，输入 Office 365 租户的用户名和密码。我们建议您使用 Office 365 开发人员网站。通常情况下，此用户名将遵循以下模式：{username}@{tenant}.onmicrosoft.com。如果您没有开发人员网站，则可以获取免费的开发人员网站作为 MSDN 权益的一部分或注册免费试用版。请注意，用户必须是租户管理员用户，但对于作为 Office 365 开发人员网站的一部分创建的租户而言，很有可能已经出现这种情况。此外，开发人员帐户通常限制为一个登录。
   4. 登录后，您将看到所有服务的列表。最初，没有选择任何权限，因为该应用尚未注册为使用任何服务。 
   5. 若要注册此示例中使用的服务，请选择以下权限：  
      * （邮件）- *以用户身份发送邮件*
      * （邮件）- *读取和编写用户邮件*
   6. 在“服务管理器”对话框中单击“确定”。

<a name="build"></a>
## 运行应用

在 Visual Studio 中加载解决方案后，按 F5 进行构建和部署。

<a name="understand"></a>
## 了解代码
   
### 限制

当前版本中不包括以下的功能。

* 文件支持不再局限于 .png 和 .jpg
* 处理带有多个附件的单个电子邮件
* 分页（获取超过 50 个电子邮件）
* 处理文件夹名称唯一性
* 提交文件夹必须是顶级文件夹  

<a name="questions-and-comments"></a>
## 问题和意见

- 如果你在运行此示例时遇到任何问题，请[记录问题](https://github.com/OfficeDev/O365-WinPhone-ArtCurator/issues)。
- 对于有关 Office 365 API 的常规问题，请发布到[堆栈溢出](http://stackoverflow.com/)。确保您的问题或意见使用了 \[office365] 标记。
  
<a name="additional-resources"></a>
## 其他资源

* [Office 365 API 平台概述](http://msdn.microsoft.com/office/office365/howto/platform-development-overview)
* [Office 开发人员中心](http://dev.office.com/)
* [Art Curator for iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator)
* [Art Curator for Android](https://github.com/OfficeDev/O365-Android-ArtCurator)
* [Art Curator for Web](https://github.com/OfficeDev/O365-Angular-ArtCurator)

## 版权信息

版权所有 (c) Microsoft。保留所有权利。
 


此项目已采用 [Microsoft 开放源代码行为准则](https://opensource.microsoft.com/codeofconduct/)。有关详细信息，请参阅[行为准则 FAQ](https://opensource.microsoft.com/codeofconduct/faq/)。如有其他任何问题或意见，也可联系 [opencode@microsoft.com](mailto:opencode@microsoft.com)。
