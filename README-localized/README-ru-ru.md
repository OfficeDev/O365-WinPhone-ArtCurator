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
# Art Curator для Windows Phone

В этом примере показано, как извлекать сообщения и вложения из Office 365 с помощью API приложения "Почта Outlook". Это приложение предназначено для [iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator), [Android](https://github.com/OfficeDev/O365-Android-ArtCurator), [Web (Angular веб-приложение)](https://github.com/OfficeDev/O365-Angular-ArtCurator), и Windows Phone. Ознакомьтесь с нашей [статьей про среду](https://medium.com/office-app-development).

Art Curator это новый подход к просмотру входящих писем. Представьте, что вы владеете компанией, которая продает дизайнерские футболки. Как владелец компании вы получаете много сообщений с рисунками от художников. Вместо того чтобы использовать Outlook и открывать каждое отдельное сообщение, скачивать вложенный рисунок и открывать его для просмотра, можно использовать Art Curator для просмотра вложений (только файлы JPG и PNG) из папки "Входящие". Таким образом выбирать понравившиеся рисунки намного удобнее.

[! [Снимок экрана Art Curator] (/РЕАДМЕ активы/AC\_WinPhone.png)](https://youtu.be/4LOvkweDfhY "Щелкните, чтобы просмотреть образец в действии.")

Этот пример показывает следующие операции из**API почты Outlook**:
* [Получить папки](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetFolders)
* [Получить сообщения](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Getmessages) (в том числе фильтрация и использование SELECT)
* [Получить вложения](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetAttachments)
* [Обновление сообщений](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Updatemessages)
* [Создавать и отправлять сообщения ](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Sendmessages) (с вложениями и без) 

<a name="prerequisites"></a>
## Предварительные требования

Для этого примера требуется следующее:  

  - Windows 8.1
  - Visual Studio 2013 с обновлением 3
  - [Инструменты API Office 365 версии 1.4.50428.2](http://aka.ms/k0534n)
  - [Сайт разработчиков Office 365](http://aka.ms/ro9c62)
  - [Учетная запись разработчика приложений Windows](https://appdev.microsoft.com/StorePortals/en-us/Account/signup/start)

### Настройка примера

Чтобы настроить пример, сделайте следующие шаги:

   1. Откройте файл **O365-Windows-Phone-Art-Curator. sln** в среде Visual Studio 2013.
   2. Создайте решение. Функция NuGet Package Restore загрузит сборки, указанные в файле packages.config.
   3. Зарегистрируйте и настройте приложение для использования служб Office 365 (подробности ниже).

### Зарегистрировать приложение, чтобы использовать API Office 365

Для этого можно использовать средства API Office 365 для Visual Studio (автоматизирующие процесс регистрации). Обязательно скачайте и установите средства API Office 365 из галереи Visual Studio.

**Примечание**. Если при установке пакетов выводится сообщение об ошибке (например, *не удается найти "Microsoft.IdentityModel.Clients.ActiveDirectory"*), убедитесь, что локальный путь, в который вы поместили решение, не слишком длинный. Чтобы устранить эту проблему, переместите решение ближе к корню диска.

   1. В окне "Обозреватель решений" выберите **O365-Windows-Phone-Art-Curator ** project -> Add -> Connected Service.
   2. Появится диалоговое окно диспетчера служб. Выберите Office 365 и Зарегистрируйте свое приложение.
   3. В диалоговом окне входа введите имя пользователя и пароль для клиента Office 365. Рекомендуем использовать Сайт разработчика Office 365. Чаще всего это имя пользователя выглядит так: {имя\_пользователя}@{клиент}.onmicrosoft.com. Если у вас нет сайта разработчика, вы можете получить Сайт разработчика бесплатно в рамках программы "Преимущества MSDN" или подписаться на бесплатную пробную версию. Обратите внимание, что пользователь должен быть администратором клиента. Скорее всего, у вас уже есть эта роль, если клиент создан в рамках Сайта разработчика Office 365. Кроме того, учетные записи разработчиков обычно ограничены одним входом.
   4. После входа вы увидите список всех служб. Изначально разрешения не выбраны, так как приложение пока что не зарегистрировано для использования служб. 
   5. Чтобы зарегистрироваться в службах, используемых в этом примере, выберите следующие разрешения:  
      * (Почта) — *Отправить почту от имени пользователя*
      * (Почта) — *Чтение и запись почты пользователя*
   6. Нажмите кнопку "ОК" в диалоговом окне диспетчера служб.

<a name="build"></a>
## Запуск приложения

После загрузки решения в Visual Studio нажмите клавишу F5, чтобы выполнить сборку и развернуть.

<a name="understand"></a>
## Разбор кода
   
### Ограничения

Указанные ниже функции не включены в текущую версию.

* Поддержка файлов, отличных от PNG и JPG
* Обработка одного сообщения электронной почты с несколькими вложениями
* Нумерование страниц (получение более чем 50 сообщений)
* Обработка уникальности имени папки
* Папка для передачи должна быть папкой верхнего уровня  

<a name="questions-and-comments"></a>
## Вопросы и комментарии

- Если у вас возникли проблемы с запуском этого образца, пожалуйста [зарегистрируйте неполадку](https://github.com/OfficeDev/O365-WinPhone-ArtCurator/issues).
- Общие вопросы про API Office 365, публикуйте в[Stack Overflow](http://stackoverflow.com/). Убедитесь в том, что ваши вопросы или комментарии помечены тегом \[Office365].
  
<a name="additional-resources"></a>
## Дополнительные ресурсы

* [Обзор на платформу API Office 365](http://msdn.microsoft.com/office/office365/howto/platform-development-overview)
* [Центр разработчиков Office](http://dev.office.com/)
* [Art Curator для iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator)
* [Art Curator для Android](https://github.com/OfficeDev/O365-Android-ArtCurator)
* [Art Curator для Web](https://github.com/OfficeDev/O365-Angular-ArtCurator)

## Авторские права

(c) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.
 


Этот проект соответствует [Правилам поведения разработчиков открытого кода Майкрософт](https://opensource.microsoft.com/codeofconduct/). Дополнительные сведения см. в разделе [часто задаваемых вопросов о правилах поведения](https://opensource.microsoft.com/codeofconduct/faq/). Если у вас возникли вопросы или замечания, напишите нам по адресу [opencode@microsoft.com](mailto:opencode@microsoft.com).
