#!/bin/bash

# This script must be called from EMP.Api folder because of relative paths in the script

# Stop server
echo Stopping server..........
echo
launchctl unload ~/Library/LaunchAgents/dotnet.run.EMP.Web.plist

# Create publish version
echo Building Staging version..........
echo
cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Web
dotnet publish -c Staging

# Copy to Nginx target folder
echo Pushing code to staging app folder..........
echo
cp -r ~/Development/Dotnet/EmployeeManagementPortal/EMP.Web/bin/Staging/netcoreapp3.1/publish/* \
/usr/local/var/www/EMP.Web

# Start server
echo Starting server..........
echo
launchctl load ~/Library/LaunchAgents/dotnet.run.EMP.Web.plist
