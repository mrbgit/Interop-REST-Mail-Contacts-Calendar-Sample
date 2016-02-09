# Interop REST Mail Contacts Calendar Android App

###Table of Contents

* [Register the App with Office 365 Developer Account](#register-the-app-with-office-365-developer-account)

* [Android Studio Configuration](#android-studio-configuration)

  * [Install Android SDK](#install-android-sdk)

  * [Android Configuration](#android-configuration)

Office Interoperability Examples - REST Mail, Contacts, and Calendar App

This example app demonstrates the Representational State Transfer (REST) interface for Office 365, including authentication, interacting with the calendar, querying the address book, and sending email. 

In the app, after you log into an Office 365 account, you can view your calendar, create single and recurring meetings on a calendar. Meetings can be scheduled with a given location, time slot, and a set of invitees, where the available locations and attendees are queried from Office 365. Each invitee has the option to accept, decline, or tentatively accept a meeting, or to email the organizer. An organizer has the option to reply all or forward the meeting invitation, and to send a "running late" message to the invitees.

The basic app is able to:

View your calendar | View meeting details | Send a message to other meeting attendees
--- | --- | ---
![Calendar Page](/img/app-calendar.jpg) | ![Details Page](/img/app-meeting-details.jpg) | ![Send a message](/img/app-reply-all.jpg)

Modify meeting details | Create a new meeting
--- | ---
![modify meeting details](/img/app-modify-meeting.jpg) | ![create new meeting](/img/app-create-meeting.jpg)

###Register the App with Office 365 Developer Account

1. No matter what you use to configure your app, you will need to register it with and Office 365 Developer Account. To register your app you need to have an account with the Office 365 Dev Program. To sign up, visit [dev.office.com/devprogram](http://dev.office.com/devprogram) or you can go directly to the [profile creation page](https://profile.microsoft.com/RegSysProfileCenter/wizardnp.aspx?wizid=14b845d0-938c-45af-b061-f798fbb4d170&lcid=1033) and create a profile. Once you have created your profile, you also need to sign up for an Office 365 developer account. The link to create your account will be in the confirmation email you receive after creating your profile. You can view detailed instructions on signing up for a developer account [here](https://msdn.microsoft.com/en-us/library/office/fp179924.aspx#o365_signup).

2. Once you have created an Office 365 Dev Account, go to [graph.microsoft.io](http://graph.microsoft.io/en-us/) to register your app and click "App Registration" then click "Office 365 App Registration Tool" or you can go directly to the registration page [dev.office.com/app-registration](http://dev.office.com/app-registration).

  ![Get started](/img/ms-graph-get-started.jpg) | ![Next step](/img/ms-graph-get-started-2.jpg)
  --- | ---

3. Give your app a name and select "Native App" in the "App type" line. Then pick a "Redirect URI" the preferred naming convention is: "your Office 365 domain + a unique name for your app", but it is not required, it must however be formatted as a URI and be unique. For example I named my app https://greencricketcreations.onmicrosoft.com/MyCalendarApp. The Redirect URI isn't a real website, it is more of a unique identifier for your app. Once you have entered a name and Redirect URI set the permissions. The necessary permissions are:

  * Read user profiles
  * Read user contacts
  * Read and write user calendars
  * Read user calendars
  * Send mail as user
  * Read and write user mail

4. Once you have filled out the form, click "Register App".

  ![Register App](/img/ms-graph-get-started-3.jpg)

5. When your registration is complete, you will receive a "Client ID". Make note of the "Client ID" and "Redirect URI" you will need them for step 7 in [Configuration](#configuration).

6. If you need more control of the registration options, you can follow these [detailed instructions](https://github.com/jasonjoh/office365-azure-guides/blob/master/RegisterAnAppInAzure.md) to register your app in Azure. Note that these instructions use the Azure classic portal. You can access the [Azure classic portal here](https://manage.windowsazure.com/).

###Install Android SDK

The Android SDK can be downloaded from the Android Developer site [here](http://developer.android.com/sdk/index.html). You may also need to install the [Java SE Development Kit 7u80](http://www.oracle.com/technetwork/java/javase/downloads/jdk7-downloads-1880260.html). If you have questions about installing the Java SDK, you can refer to this [tutorial](http://www.wikihow.com/Install-the-Java-Software-Development-Kit).

For the full installation instructions visit [the Android Developer Website](http://developer.android.com/sdk/installing/index.html).

###Android Configuration

1. Download or clone [The Interop REST Mail Calendar Android App](https://github.com/OfficeDev/Interop-REST-Mail-Contacts-Calendar-Sample).

2. Start Android Studio

3. Click on "Open an existing Android Studio project" and select the folder that contains the app, then open the "MeetingManagerAndroid" folder then the "Android" folder and select the build.gradle file.

  ![Open the App in Android Studio](/img/android-studio-open-project.jpg)

  ![App file location](/img/android-studio-file-location.jpg)

4. Click Run > Run 'app' or click the green triangle play button. 

  ![Run the app](/img/android-studio-run-app.jpg)

5. The first time you run the app you will need to add an ARM device to the emulator. To do this:  

  Click on the ellipsis (three dots in a row) next to the down arrow. 
  ![Add ARM emulator](/img/android-studio-add-emulator.jpg)

  Click "Create Virtual Device" on the next menu.
  ![Create virtual device](/img/android-studio-create-virtual-device.jpg)

  Choose a device with a resolution higher than 1200 X 800 and click "Next"
  ![Choose new device](/img/android-studio-choose-new-device.jpg)

  Check "Show downloadable system images" and select an option with "armeabi-v7a" for ABI and click next. 
  ![Choose emulator OS](/img/android-studio-choose-os.jpg)

  Select the device you created and click the green arrow at the end of the line
  ![Click finish](/img/android-studio-click-finish.jpg)

6. The next time your start the app, you can skip step 5 and select a device by clicking the down arrow next to "Android virtual device:". To add more devices simply repeat step 5 for as many devices as you want to add.

 * If you get the error message: 

    emulator: ERROR: x86 emulation currently requires hardware acceleration!
    Please ensure Intel HAXM is properly installed and usable.
    CPU acceleration status: HAX kernel module is not installed!

  Refer to this [Stackoverflow question](http://stackoverflow.com/questions/26355645/error-in-launching-avd) for how to install HAXM

  ![Start the emulator](/img/android-studio-run-emulator.jpg)

7. Your app will now start in the emulator. But in order to connect the app to an account you first have to [Register the app with Office 365](#register-the-app-with-office-365-developer-account) and copy your "CLIENT ID" and "REDIRECT URI".

8. You can enter your "CLIENT ID" and "REDIRECT URI" by clicking on the three vertical dots in the upper right of the emulator screen and click "settings" and enter them there or navigate to "app" > "src/main" > "java" > "com/microsoft/office365/meetingmgr" > Constants.java and paste your "CLIENT ID" and "REDIRECT URI" as strings and save the file.

  ![Click the three vertical dots](/img/android-studio-run-emulator-settings-2.jpg) | ![Click Settings](/img/android-studio-run-emulator-select-settings-2.jpg) | ![Enter your "CLIENT ID" and "REDIRECT URI"](/img/android-studio-run-emulator-string-entry-2.jpg)
  --- | --- | ---

9. Click "CONNECT TO OFFICE 365" and the app will start. 

  ![](/img/emulator-screen.jpg)

11. Your app is now ready for you to build with. Go create something awesome!

###Copyright

Copyright (c) 2015 Microsoft. All rights reserved.
