# Publish Instructions

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

### This instructions explains publish activities broken down in steps to allow manually control actions tuding publishing. A bash script **`Staging.Publish.EMP.Sts.sh`** can be activated to execute the entire publish. Another bash script **`Production.Publish.EMP.Sts.sh`** is a production version of script which has the same build actions but the deploy location is Azure Web App **`EmpSts6921.AzureWebsites.net`**'.
### If you are interested in Dockerized deployment, take a look at the instructions in **`DockerSettings`** folder.
<hr>

#### Create publish version
```sh
> cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Sts
> dotnet publish -c Release
```

#### Test publish version
```sh
> cd bin/Release/netcoreapp2.2/publish/
> dotnet EMP.Sts.dll
```

#### Copy to Nginx target folder
```sh
> cp -r /Users/david.d.an/Development/Dotnet/EmployeeManagementPortal/EMP.Sts/bin/Release/netcoreapp2.2/publish/* /usr/local/var/www/EMP.Sts
```

#### Start Dotnet service
```sh
> cd /usr/local/var/www/EMP.Sts
> dotnet /usr/local/var/www/EMP.Sts/EMP.Sts.dll --urls=https://localhost:22001
```

#### Better way to start Dotnet service behind Nginx
```sh
> cd /Users/david.d.an/Library/LaunchAgents
> launchctl unload /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Sts.plist
> launchctl load /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Sts.plist
```
See [dotnet.run.EMP.Sts.plist] for the details of execution plan.
Also, see [nginx.conf] for the details of Nginx configuration.

#### Restart Nginx as necessary.
```sh
> sudo nginx -s stop && sudo nginx
```
For Nginx config, see [ngingx.conf].
Nginx configuration directory is normally located at [/usr/local/etc/nginx] directory.

[dotnet.run.EMP.Sts.plist]: <file:///Users/david.d.an/Library/LaunchAgents/dotnet.run.Sts.plist>
[ngingx.conf]: <file:///usr/local/etc/nginx/ngingx.conf>
[/usr/local/etc/nginx]: <file:///usr/local/etc/nginx>