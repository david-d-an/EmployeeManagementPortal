# Base Environment
FROM mcr.microsoft.com/dotnet/aspnet:3.1-buster-slim AS base

ENV DOTNET_ENVIRONMENT=Docker
ENV ASPNETCORE_ENVIRONMENT=Docker

RUN apt-get update -y \
    && apt-get install -y --no-install-recommends openssh-server
RUN mkdir -p /run/sshd && echo "root:Docker!" | chpasswd
COPY ./EMP.Sts/DockerSettings/sshd_config /etc/ssh/
EXPOSE 2222

# Build Environment
FROM mcr.microsoft.com/dotnet/sdk:3.1-buster AS build-env
WORKDIR /App

COPY ./EMP.Common ./EMP.Common
COPY ./EMP.Sts ./EMP.Sts
RUN dotnet restore ./EMP.Common/EMP.Common.csproj 
RUN dotnet restore ./EMP.Sts/EMP.Sts.csproj

RUN dotnet publish ./EMP.Sts/EMP.Sts.csproj -c Release -o ./out

# Publish Environment
FROM base AS publish-env
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT [ \
    "/bin/bash", \
    "-c", \
    "/usr/sbin/sshd && dotnet EMP.Sts.dll" \
]
