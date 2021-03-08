#!/bin/bash

# Stop server
echo Stopping server..........
echo
launchctl unload /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Sts.Copy.plist

# Create publish version
echo Building release version..........
echo
cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Sts.Copy
dotnet publish -c Release

# Copy to Nginx target folder
echo Pushing code to app folder..........
echo
cp -r ~/Development/Dotnet/EmployeeManagementPortal/EMP.Sts.Copy/bin/Release/netcoreapp3.1/publish/* \
/usr/local/var/www/EMP.Sts

# Start server
echo Starting server..........
echo
launchctl load /Users/david.d.an/Library/LaunchAgents/dotnet.run.EMP.Sts.Copy.plist
