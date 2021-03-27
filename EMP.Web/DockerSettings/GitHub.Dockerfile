# Base Environment
FROM mcr.microsoft.com/dotnet/aspnet:3.1-buster-slim AS base

ENV DOTNET_ENVIRONMENT=Docker
ENV ASPNETCORE_ENVIRONMENT=Docker

RUN apt-get update -y \
    && apt-get install -y --no-install-recommends openssh-server
RUN mkdir -p /run/sshd && echo "root:Docker!" | chpasswd
COPY ./EMP.Web/DockerSettings/sshd_config /etc/ssh/
EXPOSE 2222

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build Environment
FROM mcr.microsoft.com/dotnet/sdk:3.1-buster AS build
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /src
COPY ["./EMP.Web/EMP.Web.csproj", "netcore-angular-docker/"]
RUN dotnet restore "netcore-angular-docker/EMP.Web.csproj"
COPY ./EMP.Web ./netcore-angular-docker
WORKDIR "/src/netcore-angular-docker"
RUN dotnet build "EMP.Web.csproj" -c Docker -o /app/build

FROM build AS publish
RUN dotnet publish "EMP.Web.csproj" -c Docker -o /app/publish

# Publish Environment
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ \
    "/bin/bash", \
    "-c", \
    "/usr/sbin/sshd && dotnet EMP.Web.dll" \
]
