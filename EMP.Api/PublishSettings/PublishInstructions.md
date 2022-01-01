# Publish Instructions

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

### This instructions explains publish activities broken down in steps to allow manually control actions during publishing. A bash script **`Staging.Publish.EMP.Api.sh`** can be activated to execute the entire publish. Another bash script **`Production.Publish.EMP.Api.sh`** is a production version of script which has the same build actions but the deploy location is Azure Web App **`EmpApi6921.AzureWebsites.net`**'.
### If you are interested in Dockerized deployment, take a look at the instructions in **`DockerSettings`** folder.
<hr>

#### Create publish version
```sh
> cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Api
> dotnet publish -c Release
```

#### Test publish version
```sh
> bin/Release/netcoreapp3.1/publish/EMP.Api
```

#### Copy to Nginx target folder
```sh
> sudo mkdir -p /usr/local/var/www/EMP.Api
> sudo cp -r /Users/david.an/Development/Dotnet/EmployeeManagementPortal/EMP.Api/bin/Release/netcoreapp3.1/publish/* /usr/local/var/www/EMP.Api
```

#### Start Dotnet service
```sh
> cd /usr/local/var/www/EMP.Api
> /usr/local/share/dotnet[/x64]/dotnet /usr/local/var/www/EMP.Api/EMP.Api.dll --urls=https://localhost:23001
```

#### Better way to start Dotnet service behind Nginx
```sh
> cd /Users/david.d.an/Library/LaunchAgents
> launchctl unload /Users/david.an/Library/LaunchAgents/dotnet.run.EMP.Api.plist
> launchctl load /Users/david.an/Library/LaunchAgents/dotnet.run.EMP.Api.plist
```
See [dotnet.run.EMP.Api.plist] for the details of execution plan.
Also, see [nginx.conf] for the details of Nginx configuration.

#### Restart Nginx as necessary.
```sh
> sudo nginx -s stop && sudo nginx
```
For Nginx config, see [ngingx.conf].
Nginx configuration directory is normally located at [/usr/local/etc/nginx] directory.

[dotnet.run.EMP.Api.plist]: <file:///Users/david.d.an/Library/LaunchAgents/dotnet.run.Api.plist>
[ngingx.conf]: <file:///usr/local/etc/nginx/ngingx.conf>
[/usr/local/etc/nginx]: <file:///usr/local/etc/nginx>