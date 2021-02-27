# Publish Instructions

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

#### Create publish version
```sh
> cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Web
> dotnet publish -c Release
```

#### Test publish version
```sh
> cd bin/Release/netcoreapp3.1/publish/
> dotnet EMP.Web.dll
```

#### Copy to Nginx target folder
```sh
> cp -r /Users/david.d.an/Development/Dotnet/EmployeeManagementPortal/EMP.Web/bin/Release/netcoreapp3.1/publish/* /usr/local/var/www/EMP.Web
```

#### Start Dotnet service
```sh
> cd /usr/local/var/www/EMP.Web
> dotnet /usr/local/var/www/EMP.Web/EMP.Web.dll --urls=http://localhost:21001
```

#### Better way to start Dotnet service
```sh
> cd /Users/david.d.an/Library/LaunchAgents
> launchctl load /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Web.plist
```
See [dotnet.run.EMP.Web.plist] for the details of execution plan.

#### Restart Nginx as necessary.
```sh
> sudo nginx -s stop && sudo nginx
```
For Nginx config, see [ngingx.conf].
Nginx configuration directory is normally located at [/usr/local/etc/nginx] directory.

[dotnet.run.EMP.Web.plist]: <file:///Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Web.plist>
[ngingx.conf]: <file:///usr/local/etc/nginx/ngingx.conf>
[/usr/local/etc/nginx]: <file:///usr/local/etc/nginx>