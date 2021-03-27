# 3.1-buster is a Debian
# 3.1-buster-slim image has dotnet 3.1 runtime
# 3.1-buster image has dotnet 3.1 SDK
# mcr.microsoft.com/dotnet/aspnet:3.1 is 3.1-buster-slim

# Build a container image with dotnet runtime to be deployed on Azure
FROM mcr.microsoft.com/dotnet/aspnet:3.1-buster-slim AS base

ENV DOTNET_ENVIRONMENT=Docker
ENV ASPNETCORE_ENVIRONMENT=Docker

# Install SSH server
RUN apt-get update -y \
    && apt-get install -y --no-install-recommends openssh-server
RUN mkdir -p /run/sshd && echo "root:Docker!" | chpasswd
COPY ./EMP.Sts/DockerSettings/sshd_config /etc/ssh/
EXPOSE 2222

# Build a temproary container image with dotnet SDK to build EMP.Sts app
FROM mcr.microsoft.com/dotnet/sdk:3.1-buster AS build-env
WORKDIR /App
COPY ./EMP.Common ./EMP.Common
COPY ./EMP.Sts ./EMP.Sts
RUN dotnet restore ./EMP.Common/EMP.Common.csproj 
RUN dotnet restore ./EMP.Sts/EMP.Sts.csproj
RUN dotnet publish ./EMP.Sts/EMP.Sts.csproj -c Release -o ./out

# Copy published EMP.Sts on the final container image. 
FROM base AS publish-env
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT [ \
    "/bin/bash", \
    "-c", \
    "/usr/sbin/sshd && dotnet EMP.Sts.dll" \
]
