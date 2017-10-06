# Dotnet-simulate-chrome-Stress-Test-on-Mac
#Chromedriver<br>
#Selenium<br>
#Csharp<br>

Using c# to simulate chrome browser to do stress test in Mac

First of all, please make sure you have chromedriver of Selenium, I do remember I put the chromedriver into the top folder.
So if you want to use it, please move it to the /bin/Debug/netcoreapp2.0 or you can modify the path in the code.

---

If you want to publish the .net app, please go to the folder StressApplication path and enter the command as below
$ dotnet publish

If you want to execute the .net app, please go the the publish folder(/StressApplication/bin/Debug/netcoreapp2.0/publish) and enter the command as below
$ dotnet StressApplication.dll 

Just directly to execute(dotnet) the application dll file.

BTW, you also can deploy the .net app to Google App Engine(GAE)

If you want to share the app

---

For published files

If you haven't install dotnet in Mac please follow the steps as below:

  1. Install dotnet 
  brew cask install dotnet

  2. Find the folder in the mac terminal

  3. enter
  dotnet StressApplication.dll

---

# About the settings
You can edit the "appsettings.json" to set the default settings.
