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
COPY ./DockerSettings/sshd_config /etc/ssh/
EXPOSE 2222

# Install node to manage Angular app
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build a temproary container image with dotnet SDK to build EMP.Web app
FROM mcr.microsoft.com/dotnet/sdk:3.1-buster AS build
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /src
COPY ["./EMP.Web.csproj", "netcore-angular-docker/"]
RUN dotnet restore "netcore-angular-docker/EMP.Web.csproj"
COPY . ./netcore-angular-docker
WORKDIR "/src/netcore-angular-docker"
RUN dotnet build "EMP.Web.csproj" -c Docker -o /app/build

# Build publish version within the temporary container image
FROM build AS publish
RUN dotnet publish "EMP.Web.csproj" -c Docker -o /app/publish

# Copy published EMP.Web on the final container image. 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ \
    "/bin/bash", \
    "-c", \
    "/usr/sbin/sshd && dotnet EMP.Web.dll" \
]

