[![](https://vistr.dev/badge?repo=dong82.EmployeeManagementPortal)](https://github.com/Elfocrash/vistr.dev)
[![](https://img.shields.io/badge/-@dong82-%23181717?style=flat-square&logo=github)](https://hub.docker.com/repositories/dong82)
[![](https://img.shields.io/badge/-@dong82-%23181717?style=flat-square&logo=github)](https://github.com/dong82)
[![](https://img.shields.io/badge/-David%20An-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/david-an-88417619/)](https://www.linkedin.com/in/david-an-88417619/)


# Employee Management Portal

> # Purpose
This set of code is to provide a web application development template for .Net developers who are inclied to popular technologies including but not limited to:

1. .Net Core
1. Angular
1. Web API
1. OpenID / OAuth2 with JWT
1. Dependency Injection pattern
1. Repository / Unit of Work pattern
1. Azure App Service
1. Docker and Docker Hub
<br><br>

> # Fun Fact
The most important part of the peject is that the solution is 100% developed on Apple's OSX and tested aginst Debian and Ubuntu. The whole project started to prove the stability of .Net Core 3.1 on Linux based systems and it has worked out seamlessly so far. 
<br><br>

## Development Settings
I used the following settings during my development
1. Visual Studio Code for code development
1. DbVisualizer for Database development
1. AWS Portal for initla database provision
1. Azure Portal for later database provision
<br><br>

> # Overview
The application suite is callsed **Employeee Management Portal** (EMP in short) and runs a web protal that provides employee management features of a company.
The app is based on 4 technologies bewlow.

1. .Net Core 3.1
1. Angular 11 to provide user interface on web browser. (tested on Chrome)
1. Web API to provide API services for data provision
1. Open ID / OAuth2 to provide decoupled authentication services
<br><br>

> # Solution Structure
The entire solution is bound under EmployeeManagementPortal.sln and there are 9 projects total
```
  EmployeeManagementPortal.sln
    │
    ├─ EMP.Api   (Main API application)
    │
    ├─ EMP.Api.Test   (Unit test for EMP.Api)
    │
    ├─ EMP.Common   (Collection of small utility functions/classes)
    │
    ├─ EMP.Data   (Collection of models for object, repository, and unit of work)
    │
    ├─ EMP.DataAccess   (Implementation of EMP.Data)
    │
    ├─ EMP.DataAccess.Test   (Unit test for EMP.DataAccess)
    │
    ├─ EMP.DbScaffold   (Database scaffold to create object-models / context)
    │
    ├─ EMP.Sts   (OpenID authentication app)
    │
    └─ EMP.Web   (Web app to consume EMP.Api service)
```
<br><br>

> # Execution Environment
To build and run, the following SDK must be installed

1. Dotnet Core 3.1
1. Angular CLI 11
<br><br>

> # Stsrt Local Debugging
Download or clone the master branch to your local and run the follwoing commands in separate command line consoles. If you are using Visual Studio, Run a separate debug instance for each app. 

1. Start EMP.API app (https://localhost:15000 by default)
```sh
  > cd EMP.Api
  > dotnet restore
  > dotnet run
```

2. Start EMP.Sts app (https://localhost:5500 by default)
```sh
  > cd EMP.Sts
  > dotnet restore
  > dotnet run
```

3. Start EMP.Web app (https://localhost:5000 by default)
```sh
  > cd EMP.Web
  > dotnet restore
  > dotnet run
```
<br><br>

> # General Workflow
The Employee Management Portal follows a typical web application workf flow:

1. User access web app
2. User logs in by ID/Pwd
3. User attempts tranactions
4. User logs out or session expires
<br><br>

> # Access Points
The the URLs are set default in Properties/launchSetting.json of each application. 
You can change the port numbers as you wish but have to make sure that the ports are properly registered other applications as URL is the only way they can interact together.
1. Web Application: https://localhost:5000
2. Sts Application: https://localhost:5500
3. API Application: https://localhost:15000
<br><br>

> # Live Example
The apps are currently deployed on Azure App Service. Since the app is deployed on low tier servers, the stat-up may ake 1 - 2 minutes.

1. Web Application: https://empwebdocker6921.azurewebsites.net/
2. Sts Application: https://empstsdocker6921.azurewebsites.net/
3. API Application: https://empapidocker6921.azurewebsites.net/
<br><br>

> # User Credentials
The example DB has the following credentials to provide a quick examples.

1. Department Manager
   ID: bob@globomantics.com
   Password: Test123!!!

2. System Admin
   ID: alice@globomantics.com
   Password: Test123!!!
<br><br>

> # Database
The database is located in Azure with the following specs:
```json
{
    "server": "mycompany6921.mysql.database.azure.com",
    "databases": ["employee", "sts"],
    "authenticationType": "SqlLogin",
    "user": "appuser@mycompany6921",
    "password": "Soil9303",
    "port": 3306,
}
```


As you noticed, two MySql databases are used by the apps. The dumps for data, tables, views and stored procedures are located in Azure Blob Storage. See below for the URLs.

* Employees database: <br>
    This is dusiness database <br>
    https://empdatadumps.blob.core.windows.net/dbdumpfiles/dump-employees-202103161224.sql
* STS database: <br>
    This is authentication database <br>
    https://empdatadumps.blob.core.windows.net/dbdumpfiles/dump-sts-202103161102.sql 

The script files contain all necessary data and statements to restore databases should you wish to restore database in your local environment. 
<br><br>

> # Deployment
The three main projects have their own deployment scripts inside the folders:
<br>

1. EMP.API
    * PuishSettings <br>
        PublishInstructions.md: explains deployment procedure for Nginx server <br>
        Staging.Publish.EMP.Api.sh: Bash script to deploy to local Nginx <br>
        Production.Publish.EMP.Api.sh: Bash script to deploy to Azure App Service <br>
    * DockerSettings <br>
        ContainerInstructions.md: Explanations of activities in build.sh <br>
        build.sh: Bash script to build docker image of EMP.Api app and push to Docker repo <br>

2. EMP.Sts.
    * PuishSettings <br>
        PublishInstructions.md: explains deployment procedure for Nginx server <br>
        Staging.Publish.EMP.Sts.sh: Bash script to deploy to local Nginx <br>
        Production.Publish.EMP.Sts.sh: Bash script to deploy to Azure App Service <br>
    * DockerSettings <br>
        ContainerInstructions.md: Explanations of activities in build.sh <br>
        build.sh: Bash script to build docker image of EMP.Sts app and push to Docker repo <br>

3. EMP.Web
    * PuishSettings <br>
        PublishInstructions.md: explains deployment procedure for Nginx server <br>
        Staging.Publish.EMP.Web.sh: Bash script to deploy to local Nginx <br>
        Production.Publish.EMP.Web.sh: Bash script to deploy to Azure App Service <br>
    * DockerSettings <br>
        ContainerInstructions.md: Explanations of activities in build.sh <br>
        build.sh: Bash script to build docker image of EMP.Web app and push to Docker repo <br>
<br>

* Note: <br>
build.sh is hard-coded to pull and push from my own Docker Hub repository (https://hub.docker.com/u/dong82). I will have to update the scripts to parametirize the target reposotry in the future.
* Note: <br>
The instrucitons for **PublishSettings** are for Linux and OSX. If you are looking for Windows/IIS instructions, you can follow the typical deployment actions provided in Visual Studio.
<br><br>
