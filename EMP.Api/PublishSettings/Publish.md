# Publish Instructions

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

#### Create publish version
```sh
> cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Api
> dotnet publish -c Release
```

#### Test publish version
```sh
> cd bin/Release/netcoreapp3.1/publish/
> dotnet EMP.Api.dll
```

#### Copy to Nginx target folder
```sh
> cp -r /Users/david.d.an/Development/Dotnet/EmployeeManagementPortal/EMP.Api/bin/Release/netcoreapp3.1/publish/* /usr/local/var/www/EMP.Api
```

#### Start Dotnet service
```sh
> cd /usr/local/var/www/EMP.Api
> dotnet /usr/local/var/www/EMP.Api/EMP.Api.dll --urls=https://localhost:23001
```

#### Better way to start Dotnet service
```sh
> cd /Users/david.d.an/Library/LaunchAgents
> launchctl unload /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Api.plist
> launchctl load /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Api.plist
```
See [dotnet.run.EMP.Api.plist] for the details of execution plan.

#### Restart Nginx as necessary.
```sh
> sudo nginx -s stop && sudo nginx
```
For Nginx config, see [ngingx.conf].
Nginx configuration directory is normally located at [/usr/local/etc/nginx] directory.

[dotnet.run.EMP.Api.plist]: <file:///Users/david.d.an/Library/LaunchAgents/dotnet.run.Api.plist>
[ngingx.conf]: <file:///usr/local/etc/nginx/ngingx.conf>
[/usr/local/etc/nginx]: <file:///usr/local/etc/nginx>