# Demand Analysis

Follow the README guides in back-end & front-end to set up the projects.

Make sure you have the dotnet dev [SDK & runtime](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.201-windows-x64-installer) installed: 

After building, make sure you have IIS installed, then install the [ASP.NET Core Hosting Bundle.](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-aspnetcore-3.1.3-windows-hosting-bundle-installer)


## Front-end 
**Make sure you've followed the README in `front-end` before doing this step.**

You can now add the front-end project to a new website in IIS. Make sure to set the physical path to `front-end\dist`

![Adding front-end to IIS](https://i.imgur.com/pRZ71tp.png)

## Back-end

**Make sure you've followed the README in `back-end` before doing this step.**
After building the back-end project, you can add an application to the IIS website
![Adding an application to the IIS website](https://i.imgur.com/CQiWbyf.png)

Set the alias to something like `api`, and set the physical path to the build directory of the `back-end` project `Demand-analysis\back-end\published\netcoreapp3.1`
![Configure the new application](https://i.imgur.com/bKPLC23.png)

You can now browse the front-end on http://demand-analysis.local and view available APIs through http://demand-analysis.local/api/
