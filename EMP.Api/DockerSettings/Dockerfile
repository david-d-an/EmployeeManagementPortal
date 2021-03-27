# 3.1-buster is a Debian
# 3.1-buster-slim image has dotnet 3.1 runtime
# 3.1-buster image has dotnet 3.1 SDK
# mcr.microsoft.com/dotnet/aspnet:3.1 is 3.1-buster-slim

# Build a container image with dotnet runtime to be deployed on Azure
FROM mcr.microsoft.com/dotnet/aspnet:3.1

ENV DOTNET_ENVIRONMENT=Docker
ENV ASPNETCORE_ENVIRONMENT=Docker

RUN apt-get update -y \
    && apt-get install -y --no-install-recommends openssh-server
RUN mkdir -p /run/sshd && echo "root:Docker!" | chpasswd
COPY ./DockerSettings/sshd_config /etc/ssh/
EXPOSE 2222

COPY bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
ENTRYPOINT [ \
    "/bin/bash", \
    "-c", \
    "/usr/sbin/sshd && dotnet EMP.Api.dll" \
]


# FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env

# WORKDIR /app

# COPY *.csproj ./
# RUN dotnet restore

# COPY . ./
# RUN dotnet publish -c Release -o out

# FROM mcr.microsoft.com/dotnet/aspnet:3.1
# WORKDIR /app
# COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", "EMP.Sts.copy.dll"]