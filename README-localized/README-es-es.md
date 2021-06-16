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
# Art Curator para Windows Phone

En este ejemplo, se muestra cómo usar la API de correo de Outlook para obtener correos electrónicos y datos adjuntos de Office 365. Se ha creado para [iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator), [Android](https://github.com/OfficeDev/O365-Android-ArtCurator), [la Web (aplicación web Angular)](https://github.com/OfficeDev/O365-Angular-ArtCurator) y Windows Phone. Consulte nuestro [artículo sobre ](https://medium.com/office-app-development).

Art Curator es una forma diferente de ver la bandeja de entrada. Imagine que posee una empresa que vende camisetas artísticas. Como propietario de la empresa, recibe muchos mensajes de correo electrónico de artistas con diseños que desean que compre. En vez de usar Outlook y abrir cada correo electrónico por separado, descargar la imagen adjunta y, a continuación, abrirla para verla, puede usar Art Curator para proporcionarle una primera vista del archivo adjunto (limitada a archivos .jpg y .png) de su bandeja de entrada para elegir y seleccionar los diseños que le gustan de una forma más eficaz.

[![Captura de pantalla de Art Curator\](./README Assets/AC\_WinPhone.png)](https://youtu.be/4LOvkweDfhY "Haga clic para ver el ejemplo en funcionamiento.")

Este ejemplo muestra las siguientes operaciones de la **API de correo de Outlook**:
* [Obtener carpetas](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetFolders)
* [Recibir mensajes](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Getmessages) (incluyendo el filtrado y el uso de selección)
* [Obtener datos adjuntos](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#GetAttachments)
* [Actualizar mensajes](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Updatemessages)
* [Crear y enviar mensajes](https://msdn.microsoft.com/office/office365/APi/mail-rest-operations#Sendmessages) (con y sin datos adjuntos). 

<a name="prerequisites"></a>
## Requisitos previos

Este ejemplo necesita lo siguiente:  

  - Windows 8.1
  - Visual Studio 2013 con Update 3
  - [Herramientas de API de Office 365 versión 1.4.50428.2](http://aka.ms/k0534n)
  - Un [sitio para desarrolladores de Office 365](http://aka.ms/ro9c62)
  - Una [cuenta de desarrollador de aplicaciones de Windows](https://appdev.microsoft.com/StorePortals/en-us/Account/signup/start)

### Configuración del ejemplo

Siga estos pasos para configurar el ejemplo.

   1. Abra el archivo **O365-Windows-Phone-Art-Curator.sln** usando Visual Studio 2013.
   2. Cree la solución. La característica Restaurar los paquetes NuGet cargará los ensamblados enumerados en el archivo packages.config.
   3. Registre y configure la aplicación para que consuma los servicios de Office 365 (descritos a continuación).

### Registrar la aplicación para usar las API de Office 365

Puede hacerlo a través de las herramientas de la API de Office 365 de Visual Studio (que automatiza el proceso de registro). Asegúrese de descargar e instalar las herramientas de la API de Office 365 desde la Galería de Visual Studio.

**Nota:** Si observa algún error durante la instalación de los paquetes (por ejemplo, *No se puede encontrar "Microsoft.IdentityModel.Clients.ActiveDirectory"*), asegúrese de que la ruta de acceso local donde ubicó la solución no es demasiado larga o profunda. Para resolver este problema, mueva la solución más cerca de la raíz de la unidad.

   1. En la ventana Explorador de soluciones, elija el proyecto **O365-Windows-Phone-Art-Curator** -> Agregar -> Servicio conectado.
   2. Aparecerá el cuadro de diálogo Administrador de servicios. Elija Office 365 y registre su aplicación.
   3. En el cuadro de diálogo de inicio de sesión, escriba el nombre de usuario y la contraseña para el espacio empresarial de Office 365. Recomendamos que use el Sitio para desarrolladores de Office 365. A menudo, este nombre de usuario seguirá el patrón {nombre\_usuario}@{inquilino}.onmicrosoft.com. Si no tiene un Sitio para desarrolladores, puede obtener uno gratuito como parte de sus beneficios de MSDN o registrarse para obtener una prueba gratuita. Tenga en cuenta que el usuario debe ser un usuario de administrador de inquilinos, pero para los inquilinos creados como parte del Sitio para desarrolladores de Office 365, sería lo más probable. Además, las cuentas de desarrolladores generalmente están limitadas a un inicio de sesión.
   4. Una vez que haya iniciado sesión, verá una lista de todos los servicios. Inicialmente, no se seleccionarán permisos ya que la aplicación no está registrada para consumir ningún servicio todavía. 
   5. Para registrarse en los servicios usados en este ejemplo, elija los permisos siguientes:  
      * (Correo) - *Enviar correo como usuario*
      * (Correo): *Leer y escribir correo de usuario*
   6. Haga clic en Aceptar en el cuadro de diálogo Administrador de servicios.

<a name="build"></a>
## Ejecutar la aplicación

Después de cargar la solución en Visual Studio, presione F5 para crear e implementar.

<a name="understand"></a>
## Entender el código
   
### Limitaciones

Las características siguientes no se incluyen en la versión actual.

* Compatibilidad de archivos más allá de .png y .jpg
* Controlar un solo correo electrónico con varios datos adjuntos
* Paginación (obtener más de 50 correos electrónicos)
* Controlar la unicidad del nombre de carpeta
* La carpeta de envío debe ser una carpeta de nivel superior  

<a name="questions-and-comments"></a>
## Preguntas y comentarios

- Si tiene algún problema para ejecutar este ejemplo, [registre un problema](https://github.com/OfficeDev/O365-WinPhone-ArtCurator/issues).
- Para realizar preguntas generales acerca de las API de Office 365, publíquelas en [Stack Overflow](http://stackoverflow.com/). Asegúrese de que sus preguntas o comentarios se etiquetan con \[office365].
  
<a name="additional-resources"></a>
## Recursos adicionales

* [Información general sobre la plataforma de las API de Office 365](http://msdn.microsoft.com/office/office365/howto/platform-development-overview)
* [Centro para desarrolladores de Office](http://dev.office.com/)
* [Art Curator para iOS](https://github.com/OfficeDev/O365-iOS-ArtCurator)
* [Art Curator para Android](https://github.com/OfficeDev/O365-Android-ArtCurator)
* [Art Curator para la Web](https://github.com/OfficeDev/O365-Angular-ArtCurator)

## Copyright

Copyright (c) Microsoft. Todos los derechos reservados.
 


Este proyecto ha adoptado el [Código de conducta de código abierto de Microsoft](https://opensource.microsoft.com/codeofconduct/). Para obtener más información, vea [Preguntas frecuentes sobre el código de conducta](https://opensource.microsoft.com/codeofconduct/faq/) o póngase en contacto con [opencode@microsoft.com](mailto:opencode@microsoft.com) si tiene otras preguntas o comentarios.
