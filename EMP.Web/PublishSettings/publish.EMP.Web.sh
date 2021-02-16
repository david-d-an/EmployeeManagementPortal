#!/bin/bash

# Stop server
echo Stopping server..........
echo
launchctl unload /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Web.plist

# Create publish version
echo Building release version..........
echo
cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Web
dotnet publish -c Release

# Copy to Nginx target folder
echo Pushing code to app folder..........
echo
cp -r /Users/david.d.an/Development/Dotnet/EmployeeManagementPortal/EMP.Web/bin/Release/netcoreapp3.1/publish/* /usr/local/var/www/EMP.Web

# Start server
echo Starting server..........
echo
launchctl load /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Web.plist
