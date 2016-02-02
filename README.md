# Interop REST Mail Contacts Calendar Android App

Office Interoperability Examples - REST Mail, Contacts, and Calendar App

This example app demonstrates the Representational State Transfer (REST) interface for Office 365, including authentication, interacting with the calendar, querying the address book, and sending email. 

In the app, after you log into an Office 365 account, you can create single and recurring meetings on a calendar. Meetings can be scheduled with a given location, time slot, and a set of invitees, where the available locations and attendees are queried from Office 365. Each invitee has the option to accept, decline, or tentatively accept a meeting, or to email the organizer. An organizer has the option to reply all or forward the meeting invitation, and to send a "running late" message to the invitees.

###Table of Contents

* [System Requirements](#system-requirements)

* [Install Android SDK](#install-android-sdk)

* [Register the App with Office 365 Developer Account](#register-the-app-with-office-365-developer-account)

* [Configuration](#configuration)

* [Dependencies](#dependencies)

###System Requirements

To us the Interop REST Mail Contacts Calendar you need to install the Android SDK, which requires that your System meet the following requirements ([from the Android Developer Site](http://developer.android.com/sdk/index.html#Requirements)):

#####Windows

* Microsoft® Windows® 8/7/Vista (32 or 64-bit)
* 2 GB RAM minimum, 4 GB RAM recommended
* 400 MB hard disk space
* At least 1 GB for Android SDK, emulator system images, and caches
* 1280 x 800 minimum screen resolution
* Java Development Kit (JDK) 7
* Optional for accelerated emulator: Intel® processor with support for Intel® VT-x, Intel® EM64T (Intel® 64), and Execute Disable (XD) Bit functionality

#####Mac OS X

* Mac® OS X® 10.8.5 or higher, up to 10.9 (Mavericks)
* 2 GB RAM minimum, 4 GB RAM recommended
* 400 MB hard disk space
* At least 1 GB for Android SDK, emulator system images, and caches
* 1280 x 800 minimum screen resolution
* Java Runtime Environment (JRE) 6
* Java Development Kit (JDK) 7
* Optional for accelerated emulator: Intel® processor with support for Intel® VT-x, Intel® EM64T (Intel® 64), and Execute Disable (XD) Bit functionality
* On Mac OS, run Android Studio with Java Runtime Environment (JRE) 6 for optimized font rendering. You can then configure your project to use Java Development Kit (JDK) 6 or JDK 7.

#####Linux

* GNOME or KDE desktop
* GNU C Library (glibc) 2.15 or later
* 2 GB RAM minimum, 4 GB RAM recommended
* 400 MB hard disk space
* At least 1 GB for Android SDK, emulator system images, and caches
* 1280 x 800 minimum screen resolution
* Oracle® Java Development Kit (JDK) 7
* Tested on Ubuntu® 14.04, Trusty Tahr (64-bit distribution capable of running 32-bit applications).

###Install Android SDK

The Android SDK can be downloaded from the Android Developer site [here](http://developer.android.com/sdk/index.html). You may also need to install the [Java SE Development Kit 7u80](http://www.oracle.com/technetwork/java/javase/downloads/jdk7-downloads-1880260.html). If you have questions about installing the Java SDK, you can refer to this [tutorial](http://www.wikihow.com/Install-the-Java-Software-Development-Kit).

For the full installation instructions visit [the Android Developer Website](http://developer.android.com/sdk/installing/index.html).

###Register the App with Office 365 Developer Account

1. To register your app you need to have an account with the Office 365 Dev Program. To sign up, visit [dev.office.com/devprogram](http://dev.office.com/devprogram) or you can go directly to the [profile creation page](https://profile.microsoft.com/RegSysProfileCenter/wizardnp.aspx?wizid=14b845d0-938c-45af-b061-f798fbb4d170&lcid=1033) and create a profile. Once you have created your profile, you also need to sign up for an Office 365 developer account. The link to create your account will be in the confirmation email you receive after creating your profile. You can view detailed instructions on signing up for a developer account [here](https://msdn.microsoft.com/en-us/library/office/fp179924.aspx#o365_signup).

2. Once you have created an Office 365 Dev Account, go to [graph.microsoft.io](http://graph.microsoft.io/en-us/) to register your app and click "App Registration" then click "Office 365 App Registration Tool" or you can go directly to the registration page [dev.office.com/app-registration](http://dev.office.com/app-registration).

  ![Get started](/img/ms-graph-get-started.jpg) | ![Next step](/img/ms-graph-get-started-2.jpg)
  --- | ---

3. Give your app a name and select "Native App" in the "App type" line. Then pick a "Redirect URI" the preferred naming convention is: "your Office 365 domain + a unique name for your app" for example I named my app https://greencricketcreations.onmicrosoft.com/MyCalendarApp, but it is not required. The Redirect URI isn't a real website, it is more of a unique identifier for your app. Once you have entered a name and Redirect URI set the permissions. The necessary permissions are:

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

###Configuration

1. Download or clone [The Interop REST Mail Calendar Android App](https://github.com/OfficeDev/Interop-REST-Mail-Contacts-Calendar-Sample).

2. Start Android Studio

3. Click on "Open an existing Android Studio project" and select the folder that contains the app, then open the "MeetingManagerAndroid" folder then the "Android" folder and select the build.gradle file.

  ![Open the App in Android Studio](/img/android-studio-open-project.jpg)

  ![App file location](/img/android-studio-file-location.jpg)

4. Click Run > Run 'app' or click the green triangle play button. 

  ![Run the app](/img/android-studio-run-app.jpg)

5. Click the down arrow next to the selection for "Android virtual device:" to select a different device or click the ellipsis next to it to add more devices, then click "Ok".

 * If you get the error message: 

    emulator: ERROR: x86 emulation currently requires hardware acceleration!
    Please ensure Intel HAXM is properly installed and usable.
    CPU acceleration status: HAX kernel module is not installed!

  Refer to this [Stackoverflow question](http://stackoverflow.com/questions/26355645/error-in-launching-avd) for how to install HAXM

  ![Start the emulator](/img/android-studio-run-emulator.jpg)

6. Your app will now start in the emulator. But in order to connect the app to an account you first have to [Register the app with Office 365](#register-the-app-with-office-365-developer-account) and copy your "CLIENT ID" and "REDIRECT URI".

7. You can enter your "CLIENT ID" and "REDIRECT URI" by clicking on the three vertical dots in the upper right of the emulator screen and click "settings" and enter them there or navigate to "app" > "src/main" > "java" > "com/microsoft/office365/meetingmgr" > Constants.java and paste your "CLIENT ID" and "REDIRECT URI" as strings and save the file.

  ![Click the three vertical dots](/img/android-studio-run-emulator-settings.jpg) | ![Click Settings](/img/android-studio-run-emulator-select-settings.jpg) | ![Enter your "CLIENT ID" and "REDIRECT URI"](/img/android-studio-run-emulator-string-entry.jpg)
  --- | --- | ---

8. Click "CONNECT TO OFFICE 365" and the app will start

  ![](/img/emulator-screen.jpg)

9. Your app is now ready. Go create something awesome!