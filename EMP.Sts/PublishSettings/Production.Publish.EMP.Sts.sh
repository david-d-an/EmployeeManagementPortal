#!/bin/bash

# This script must be called from EMP.Api folder because of relative paths in the script

appName="EmpSts6921"
resourceGroup="EMP"

# Create publish version
echo Building release version..........
echo
cd ~/Development/DotNet/EmployeeManagementPortal/EMP.Sts
dotnet publish -c Release

# Create deploy package in Zip format
echo Creating deploy.zip..........
echo
cd ./bin/release/netcoreapp3.1/publish
zip -r deploy.zip *

# Copy to Azure App Service
echo Pushing code to Azure web app..........
echo
az webapp deploy --src-path ./deploy.zip --name $appName -g $resourceGroup --type zip

# Removing deploy.zip
rm ./deploy.zip
