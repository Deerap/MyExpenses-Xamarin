My Expenses
==========

Expense taking cross platform application for Windows Phone, Android, and iOS built with Xamarin inside of Visual Studio 2013. 
Expenses are stored locally in a Sqlite-net database. You can add new expenses and edit or delete existing. All business logic is shared in one portable class library.

Written in C# with ([Xamarin](http://www.xamarin.com))  **Created in Visual Studio 2013**

For Windows Phone you must install SQLite for Windows Phone Extension: http://visualstudiogallery.msdn.microsoft.com/cd120b42-30f4-446e-8287-45387a4f40b7

** For Azure Mobile Services Integration please read the setup at the bottom of this page! **

## How much code is shared?
Up to 80% of code is shared across platforms. All of the Models, Services, View Models, and tons of helper classes are all found in one single PCL library. 

## What technology is used?
Everything is written in C# with Xamarin with a base PCL library. This project couldn't have been done without the following:

### Json.NET
https://components.xamarin.com/view/json.net - Used both the NuGet in the PCL and component for iOS for facade linking. 

### Azure Mobile Services
https://www.nuget.org/packages/WindowsAzure.MobileServices/ and https://components.xamarin.com/view/azure-mobile-services 
Used optionally to store data up into Azure and sync between devices! 
Use the NuGet in Windows Phone and PCL and then the components for each iOS and Android. See below for more information.

### HTTP Client Libraries
https://www.nuget.org/packages/Microsoft.Net.Http - Brings HTTP Client functionality to Windows Phone in PCL.

### Windows Phone Toolkit
http://phone.codeplex.com/ - Everyone's favorite WP toolkit!

### ANDHud
https://components.xamarin.com/view/AndHUD - Brings in a nice spinner for Xamarin.Android

### BTProgressHud
https://components.xamarin.com/view/btprogresshud - Great spinner for iOS

### MonoTouch.Dialog
http://docs.xamarin.com/guides/ios/user_interface/monotouch.dialog/ - A wonderful library for Xamarin.iOS to create user interfaces quick with not a lot of code.

### Sqlite-net PCL
https://github.com/praeclarum/sqlite-net - A wonderful library for cross platform sqlite databases.
Now in PCL form with: https://github.com/oysteinkrog/Sqlite.net-pcl

### Azure Mobile Services Integration

* Create a new Azure Mobile Services Table Called "Expense"
* Follow this guide to setup the Table for Authentication: http://www.windowsazure.com/en-us/develop/mobile/tutorials/get-started-with-users-dotnet/

* Setup Azure Scripts for Insert, Read, Update: http://www.windowsazure.com/en-us/develop/mobile/tutorials/authorize-users-in-scripts-dotnet/

* Setup Twitter App for Authentication: http://www.dotnetcurry.com/ShowArticle.aspx?ID=860

* Optionally you can setup Facebook, Microsoft, or Google, however the sample is setup for Twitter

* Open "AzureService.cs" a shared file in MyEpenses.Android (or iOS or WindowsPhone) and 
* Comment Back In & Edit: MobileClient = new MobileServiceClient(
        
"https://"+"PUT-SITE-HERE" +".azure-mobile.net/",
        
"PUT-YOUR-API-KEY-HERE");

* This information can be found on Azure