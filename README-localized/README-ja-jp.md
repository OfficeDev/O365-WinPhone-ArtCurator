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
# Windows Phone 用 Art Curator

このサンプルでは、Outlook メール API を使用して Office 365 からメールと添付ファイルを取得する方法を示します。この API は、[iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator)、[Android](https://github.com/OfficeDev/O365-Android-ArtCurator)、[Web (Angular Web アプリ)](https://github.com/OfficeDev/O365-Angular-ArtCurator)、および Windows Phone 用にビルドされています。[Medium の記事](https://medium.com/office-app-development)をご覧ください。

Art Curator を使用すると、通常とは異なる方法で受信トレイを表示できます。芸術的な T シャツを販売する会社を経営していると想像してみてください。会社のオーナーであるあなたのもとには、買ってほしいと思うデザインを示したたくさんのメールがアーティストから届きます。Outlook を使用して個々のメールを開き、添付の画像をダウンロードしてから開いて表示する代わりに、Art Curator を使用すると、受信トレイの添付ファイル優先 (.jpg と .png ファイルに限定) ビューが最初に表示され、より効率的な方法で気に入ったデザインを選べるようになります。

[![Art Curator Screenshot](./README Assets/AC\_WinPhone.png)](https://youtu.be/4LOvkweDfhY "こちらをクリックしてサンプルの動作をご覧ください。")

このサンプルでは、**Outlook Mail API** からの次の操作を例示します。
* [フォルダーの取得](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetFolders)
* [メッセージの取得](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Getmessages) (フイルター処理と選択機能の使用も含む)
* [添付ファイルの取得](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetAttachments)
* [メッセージの更新](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Updatemessages)
* [メッセージの作成と送信](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Sendmessages) (添付ファイルあり/なし) 

<a name="prerequisites"></a>
## 前提条件

このサンプルを実行するには次のものが必要です。  

  - Windows 8.1
  - 更新プログラム 3 が適用された Visual Studio 2013。
  - [Office 365 API Tools バージョン 1.4.50428.2](http://aka.ms/k0534n)
  - [Office 365 開発者向けサイト](http://aka.ms/ro9c62)
  - [Windows アプリ 開発者アカウント](https://appdev.microsoft.com/StorePortals/en-us/Account/signup/start)

### サンプルを構成する

次の手順に従ってサンプルを構成します。

   1. Visual Studio 2013 を使用して **O365-Windows-Phone-Art-Curator.sln** ファイルを開きます。
   2. ソリューションをビルドします。NuGet パッケージ復元機能で packages.config ファイルにリストされたアセンブリが読み込まれます。
   3. Office 365 サービスを使用するようアプリを登録し構成します (詳細は以下をご覧ください)。

### Office 365 API を使用するようアプリを登録する

登録は Office 365 API Tools for Visual Studio で自動的に行うことができます。必ず Visual Studio ギャラリーから Office 365 API ツールをダウンロードしてインストールしてください。

**注**: パッケージのインストール中にエラーが表示された場合は (「*"Microsoft.IdentityModel.Clients.ActiveDirectory" が見つかりません*」など)、ソリューションを配置した場所のローカル パスが長すぎたり深すぎたりしていないかをご確認ください。ドライブのルート近くにソリューションを移動すると問題が解決します。

   1. \[ソリューション エクスプローラー] ウィンドウで、\[**O365-Windows-Phone-Art-Curator**] プロジェクト、\[追加]、\[接続済みサービス] の順に選びます。
   2. サービス マネージャーのダイアログ ボックスが表示されます。Office 365 を選択し、アプリを登録します。
   3. \[サインイン] ダイアログ ボックスで、Office 365 テナント用のユーザー名とパスワードを入力します。自分の Office 365 開発者向けサイトを使用することをお勧めします。多くの場合、このユーザー名は {username}@{tenant}.onmicrosoft.com というパターンになります。自分の開発者向けサイトを持っていない場合、MSDN 特典の一部として無料で、または無料試用版にサインアップすることで入手できます。ユーザーはテナント管理ユーザーである必要があることにご注意ください。Office 365 開発者向けサイトの一部として作成されたテナントでは、ほとんどの場合テナント管理ユーザーになります。また、開発者アカウントは通常 1 つのサインインに制限されます。
   4. サインイン後、すべてのサービスが表示されます。最初はサービスを使用するようアプリが登録されていないため、アクセス許可は何も選択されていない状態になっています。 
   5. このサンプルで使用するサービスを登録するには、次のアクセス許可を選択します。  
      * (メール) - *ユーザーとしてメールを送信*
      * (メール) - *ユーザーのメールの読み取りと書き込み*
   6. \[サービス マネージャー] ダイアログ ボックスで \[OK] をクリックします。

<a name="build"></a>
## アプリを実行する

Visual Studio にソリューションを読み込ませたら、F5 を押してビルドと展開を行います。

<a name="understand"></a>
## コードを理解する
   
### 制限事項

現在のバージョンには、次の機能は含まれていません。

* .png と .jpg 以外のファイルのサポート
* 1 通に複数の添付ファイルが含まれているメールの処理
* ページング (50 通を超えるメールの受け取り)
* フォルダー名の一意性の処理
* 送信フォルダーは最上位のフォルダーでなければならない  

<a name="questions-and-comments"></a>
## 質問とコメント

- このサンプルの実行で問題が発生した場合は、[問題を報告](https://github.com/OfficeDev/O365-WinPhone-ArtCurator/issues)してください。
- Office 365 API に関する全般的な質問は、[Stack Overflow](http://stackoverflow.com/) に投稿してください。質問やコメントには、必ず \[office365] とタグを付けてください。
  
<a name="additional-resources"></a>
## その他のリソース

* [Office 365 API プラットフォームの概要](http://msdn.microsoft.com/office/office365/howto/platform-development-overview)
* [Office デベロッパー センター](http://dev.office.com/)
* [iOS 用 Art Curator](https://github.com/OfficeDev/O365-iOS-ArtCurator)
* [Android 用 Art Curator](https://github.com/OfficeDev/O365-Android-ArtCurator)
* [Web 用 Art Curator](https://github.com/OfficeDev/O365-Angular-ArtCurator)

## 著作権

Copyright (c) Microsoft.All rights reserved.
 


このプロジェクトでは、[Microsoft オープン ソース倫理規定](https://opensource.microsoft.com/codeofconduct/)が採用されています。詳細については、「[倫理規定の FAQ](https://opensource.microsoft.com/codeofconduct/faq/)」を参照してください。また、その他の質問やコメントがあれば、[opencode@microsoft.com](mailto:opencode@microsoft.com) までお問い合わせください。
