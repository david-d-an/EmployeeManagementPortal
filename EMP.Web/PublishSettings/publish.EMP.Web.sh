#!/bin/bash

# Stop server
echo Stopping server..........
echo
launchctl unload /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Web.plist

# Create publish version
echo Building release version..........
echo
cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Web
dotnet publish -c Staging

# Copy to Nginx target folder
echo Pushing code to app folder..........
echo
cp -r ~/Development/Dotnet/EmployeeManagementPortal/EMP.Web/bin/Staging/netcoreapp3.1/publish/* \
/usr/local/var/www/EMP.Web

# Start server
echo Starting server..........
echo
launchctl load /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Web.plist
