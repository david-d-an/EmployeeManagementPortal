# FROM mcr.microsoft.com/dotnet/aspnet:3.1
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

RUN apt-get update -y \
    && apt-get install -y --no-install-recommends openssh-server
RUN mkdir -p /run/sshd && echo "root:Docker!" | chpasswd
COPY ./DockerSettings/sshd_config /etc/ssh/
EXPOSE 2222

COPY bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
# ENTRYPOINT ["dotnet", "EMP.Sts.copy.dll"]
# CMD ["/etc/sshd", "-D"]
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