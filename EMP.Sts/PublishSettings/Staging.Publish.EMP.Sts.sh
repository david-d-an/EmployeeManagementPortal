#!/bin/bash

# This script must be called from EMP.Api folder because of relative paths in the script

# Stop server
echo Stopping server..........
echo
launchctl unload ~/Library/LaunchAgents/dotnet.run.EMP.Sts.plist

# Create publish version
echo Building release version..........
echo
cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Sts
dotnet publish -c Release

# Copy to Nginx target folder
echo Pushing code to app folder..........
echo
cp -r ~/Development/Dotnet/EmployeeManagementPortal/EMP.Sts/bin/Release/netcoreapp3.1/publish/* \
/usr/local/var/www/EMP.Sts

# Start server
echo Starting server..........
echo
launchctl load ~/Library/LaunchAgents/dotnet.run.EMP.Sts.plist
