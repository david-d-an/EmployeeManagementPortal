# Containerizing Instructions

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

### This instructions explains activities to build a docker image broken down in steps to allow manually control actions during build. A bash script **`build.sh`** can be activated to execute the entire build activities based on **`Dockerfile`**.<br>

### Currently, branch merge into **`master`** activates the GitHub action, **`master_EmpStsDocker6921.yml`**, which triggers deployment of a Docker container for **`EMP.Sts`** onto **`empstsdocker6921.azurewebsites.net`** as specified by GitHub.Dockerfile.<br>

<hr>

### Build
* Docker image is build by Dockerfile
```sh
> dotnet publish -c Release
> docker build -f ./DockerSettings/Dockerfile -t emp.sts .
```

### Test
* Application is running on http://localhost:8080
```sh
> docker run -d -p 8080:80 --name emp.sts.container emp.sts
```

### Log in as root user.
* -u 0 is not necessary as Dockerfile sets the default user as root
```sh
> docker exec -it emp.sts.container /bin/bash
```

### Get Image ID
```sh
> docker images -f=reference='*dong82/emp.sts*:*' -f=reference='emp.sts*:*'
```

### Tag Iamge
```sh
> docker tag $(docker images -q emp.sts:latest) dong82/emp.sts
```

### Get latest tag name
```sh
> tagname=$(./TagName.sh emp.sts)
```

### Push to Repository
```sh
> docker push dong82/emp.sts:$tagname
> docker push dong82/emp.sts:latest
```
