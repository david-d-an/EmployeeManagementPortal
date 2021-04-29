# EmployeeManagementPortal

> # Overview
The protal provides Employee management features of a company.
The app is based 3 technologies bewlow.

1. Angular 11 to provide user interface on web browser. (tested on Chrome)
1. Web API to provide API service for data provision
1. Open ID / OAuth2 to provide decoupled authentication service for
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
To be able to built and run, the following SDK must be installed

1. Dotnet Core 3.1
1. Angular CLI 11
<br><br>

> # Execution Instrustions
Download or clone the master branch to your local and run the follwoing commands in separate command line consoles

1. Start API app (https://localhost:15000 by default)
```sh
  > cd EMP.Api
  > dotnet restore
  > dotnet run
```

2. Start Sts app (https://localhost:5500 by default)
```sh
  > cd EMP.Api
  > dotnet restore
  > dotnet run
```

3. Start Web app (https://localhost:5000 by default)
```sh
  > cd EMP.Api
  > dotnet restore
  > dotnet run
```
<br><br>

> # General Workflow
The app follows a typical web application workf flow:

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

As you noticed, it's a MySql database to try things outisde SQL Server. The dumps for data, views and stored procedures are located in EMP.DataAccess/MySql folder.

If you wish to restore database in you local environment or your own cloud, the script will provide a complete set to finish the task.
<br><br>

> #Deployment
The three main projects have their own deployment scripts inside the folders:

1. EMP.API
    * PuishSettings <br>
        PublishInstructions.md: explains deployment procedure for Nginx server <br>
        Staging.Publish.EMP.Api.sh: Bash script to deploy to local Nginx <br>
        Production.Publish.EMP.Api.sh: Bash script to deploy to Azure App Service <br>
    * DockerSettings <br>
        ContainerInstructions.md: Explanations of activities in build.sh <br>
        build.sh: Bash script to build docker image of EMP.Api app and push to Docker repo <br>

2. EMP.Sts
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
* Note: build.sh is written to pull and push from my own Docker Hub repository. I will have to update the script to parametirize the targe reposotry in the future.



