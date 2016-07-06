# Art Curator für Windows Phone

In diesem Beispiel wird veranschaulicht, wie Sie die Outlook-E-Mail-API verwenden, um E-Mails und Anhänge aus Office 365 abzurufen. Die API ist für [iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator), [Android](https://github.com/OfficeDev/O365-Android-ArtCurator), [Web (Angular Web App)](https://github.com/OfficeDev/O365-Angular-ArtCurator) und Windows Phone ausgelegt. Informationen hierzu finden Sie auch in unserem [Artikel in Medium](https://medium.com/office-app-development) .

Mit Art Curator können Sie Ihren Posteingang auf andere Weise anzeigen. Angenommen, Sie besitzen ein Unternehmen, das künstlerisch gestaltete T-Shirts verkauft. Als Inhaber des Unternehmens erhalten Sie eine Vielzahl von E-Mails von Künstlern mit Designs, die Sie von den Künstlern erwerben sollen. Anstatt jede einzelne E-Mail mit Outlook zu öffnen, das angehängte Bild herunterzuladen und es dann zum Ansehen zu öffnen, können Sie mit Art Curator zuerst alle Anhänge Ihres Posteingangs (nur JPG- und PNG-Dateien) anzeigen, um auf effiziente Weise die gewünschten Designs auszuwählen.

[![Art Curator Screenshot](../README Assets/AC_WinPhone.png)](https://youtu.be/4LOvkweDfhY "Click to see the sample in action.")

In diesem Beispiel werden folgende Vorgänge der **Outlook-E-Mail-API** veranschaulicht:
* [Abrufen von Ordnern](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetFolders)
* [Abrufen von Nachrichten](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Getmessages) (einschließlich Filtern und Verwendung der Auswahl) 
* [Abrufen von Anhängen](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetAttachments)
* [Aktualisieren von Nachrichten](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Updatemessages)
* [Erstellen und Senden von Nachrichten](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Sendmessages) (mit und ohne Anhang) 

<a name="prerequisites"></a>
## Voraussetzungen

Für dieses Beispiel ist Folgendes erforderlich:  

  - Windows 8.1
  - Visual Studio 2013 mit Update 3
  - [Office 365 API-Tools Version 1.4.50428.2](http://aka.ms/k0534n)
  - Eine [Website für Office 365-Entwickler](http://aka.ms/ro9c62)
  - Ein [Windows App-Entwicklerkonto](https://appdev.microsoft.com/StorePortals/en-us/Account/signup/start)

### Konfigurieren des Beispiels

Führen Sie die folgenden Schritte aus, um das Beispiel zu konfigurieren:

   1. Öffnen Sie die Datei **O365-Windows-Phone-Art-Curator.sln** mit Visual Studio 2013.
   2. Erstellen Sie die Lösung. Die Funktion „NuGet Package Restore“ lädt die in der Datei „packages.config“ enthaltenen Assemblys.
   3. Registrieren Sie und konfigurieren Sie die App zur Nutzung von Office 365-Diensten (siehe unten).

### Registrieren der App für die Nutzung der Office 365-APIs

Dies ist mithilfe der Office 365 API-Tools für Visual Studio möglich, mit denen der Registrierungsvorgang automatisiert wird. Achten Sie darauf, die Office 365 API-Tools aus der Visual Studio Gallery herunterzuladen.

**Hinweis**: Sollten bei der Installation der Pakete Fehler auftreten (z. B. *Unable to find "Microsoft.IdentityModel.Clients.ActiveDirectory"*), überprüfen Sie, ob der lokale Pfad, in dem Sie die Lösung platziert haben, nicht zu lang/tief ist. Dieses Problem lässt sich beheben, indem Sie den Pfad auf Ihrem Laufwerk verkürzen.

   1. Wählen Sie im Projektmappen-Explorer **O365-Windows-Phone-Art-Curator** Projekt -> Hinzufügen -> Verbundener Dienst.
   2. Daraufhin wird das Dialogfeld „Dienst-Manager“ angezeigt. Wählen Sie „Office 365“ und „App registrieren“.
   3. Geben Sie im Anmeldedialogfeld den Benutzernamen und das Kennwort für Ihren Office 365-Mandanten ein. Es wird empfohlen, dass Sie Ihre Website für Office 365-Entwickler verwenden. Häufig entspricht der Benutzername dem Muster {username}@{tenant}.onmicrosoft.com. Wenn Sie nicht über eine Website für Entwickler verfügen, können Sie im Rahmen Ihres MSDN-Abonnements eine kostenlose Entwicklerwebsite erhalten oder sich für eine kostenlose Testversion registrieren. Der Benutzer muss Administrator des Mandanten sein. Bei Mandanten, die als Teil einer Office 365-Entwicklerwebsite erstellt wurden, ist dies wahrscheinlich bereits der Fall. Auch Entwicklerkonten sind in der Regel auf eine Anmeldung beschränkt.
   4. Nachdem Sie sich angemeldet haben, wird eine Liste aller Dienste angezeigt. Zunächst sind keine Berechtigungen ausgewählt, da die App noch nicht für die Verwendung von Diensten registriert ist. 
   5. Wählen Sie zum Registrieren für die in diesem Beispiel verwendeten Dienste die folgenden Berechtigungen aus:  
      * (Mail) - *E-Mails als Benutzer senden*
      * (Mail) - *Benutzer-E-Mails lesen und schreiben*
   6. Klicken Sie im Dialogfeld „Dienst-Manager“ auf „OK“.

<a name="build"></a>
## Ausführen der App

Nachdem Sie die Lösung in Visual Studio geladen haben, drücken Sie F5, um sie zu erstellen und bereitzustellen.

<a name="understand"></a>
## Grundlegendes zum Code
   
### Einschränkungen

Die folgenden Funktionen sind in der aktuellen Version nicht enthalten.

* Unterstützung anderer Dateien als .png und .jpg
* Verarbeitung von E-Mails mit mehreren Anhängen
* Paging (Abrufen von mehr als 50 E-Mails)
* Verarbeitung der Eindeutigkeit von Ordnernamen
* Der Ordner zum Senden muss ein Ordner der obersten Ebene sein.  

<a name="questions-and-comments"></a>
## Fragen und Kommentare

- Wenn Sie beim Ausführen dieses Beispiels Probleme haben, [melden Sie sie bitte](https://github.com/OfficeDev/O365-WinPhone-ArtCurator/issues).
- Allgemeine Fragen zu den Office 365-APIs können Sie auf [Stack Overflow](http://stackoverflow.com/) stellen. Stellen Sie sicher, dass Ihre Fragen oder Kommentare mit [office365] markiert sind.
  
<a name="additional-resources"></a>
## Zusätzliche Ressourcen

* [Office 365 APIs – Plattformübersicht](http://msdn.microsoft.com/office/office365/howto/platform-development-overview)
* [Office Dev Center](http://dev.office.com/)
* [Art Curator für iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator)
* [Art Curator für Android](https://github.com/OfficeDev/O365-Android-ArtCurator)
* [Art Curator für Web](https://github.com/OfficeDev/O365-Angular-ArtCurator)

## Copyright

Copyright (c) Microsoft. Alle Rechte vorbehalten.
 
 