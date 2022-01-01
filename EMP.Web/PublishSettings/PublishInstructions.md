# Publish Instructions

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

### This instructions explains publish activities broken down in steps to allow manually control actions during publishing. A bash script **`Staging.Publish.EMP.Web.sh`** can be activated to execute the entire publish. Another bash script **`Production.Publish.EMP.Web.sh`** is a production version of script which has the same build actions but the deploy location is Azure Web App **`EmpWeb6921.AzureWebsites.net`**'.
### If you are interested in Dockerized deployment, take a look at the instructions in **`DockerSettings`** folder.
<hr>

#### Create publish version
```sh
> cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Web
> dotnet publish -c Release
```

#### Test publish version
```sh
> bin/Release/netcoreapp3.1/publish/EMP.Web
```

#### Copy to Nginx target folder
```sh
> sudo mkdir -p /usr/local/var/www/EMP.Web
> sudo cp -r /Users/david.an/Development/Dotnet/EmployeeManagementPortal/EMP.Web/bin/Release/netcoreapp3.1/publish/* /usr/local/var/www/EMP.Web
```

#### Start Dotnet service
```sh
> cd /usr/local/var/www/EMP.Web
> /usr/local/share/dotnet[/x64]/dotnet /usr/local/var/www/EMP.Web/EMP.Web.dll --urls=https://localhost:21001
```

#### Better way to start Dotnet service behind Nginx
```sh
> cd /Users/david.an/Library/LaunchAgents
> launchctl load /Users/david.an/Library/LaunchAgents/dotnet.run.EMP.Web.plist
```
See [dotnet.run.EMP.Web.plist] for the details of execution plan.
Also, see [nginx.conf] for the details of Nginx configuration.

#### Restart Nginx as necessary.
```sh
> sudo nginx -s stop && sudo nginx
```
For Nginx config, see [ngingx.conf].
Nginx configuration directory is normally located at [/usr/local/etc/nginx] directory.

[dotnet.run.EMP.Web.plist]: <file:///Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Web.plist>
[ngingx.conf]: <file:///usr/local/etc/nginx/ngingx.conf>
[/usr/local/etc/nginx]: <file:///usr/local/etc/nginx>