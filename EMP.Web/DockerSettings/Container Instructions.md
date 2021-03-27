# Containerizing Instructions

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

### This instructions explains activities to build a docker image broken down in steps to allow manually control actions during build. A bash script **`build.sh`** can be activated to execute the entire build activities based on **`Dockerfile`**.<br>

### Currently, branch merge into **`master`** activates the GitHub action, **`master_EmpWebDocker6921.yml`**, which triggers deployment of a Docker container for **`EMP.Web`** onto **`empwebdocker6921.azurewebsites.net`** as specified by GitHub.Dockerfile.<br>

<hr>

### Build
* Docker image is build by Dockerfile
```sh
> docker build -f ./DockerSettings/Dockerfile -t emp.web .
```

### Test
* Application is running on http://localhost:8090
```sh
> docker run -d -p 8090:80 --name emp.web.container emp.web
```

### Log in as root user.
* -u 0 is not necessary as Dockerfile sets the default user as root
```sh
> docker exec -it emp.web.container /bin/bash
```

### Get Image ID
```sh
> docker images -f=reference='*dong82/emp.web*:*' -f=reference='emp.web*:*'
```

### Tag Iamge
```sh
> docker tag $(docker images -q emp.web:latest) dong82/emp.web
```

### Get latest tag name
```sh
> tagname=$(./TagName.sh emp.web)
```

### Push to Repository
```sh
> docker push dong82/emp.web:$tagname
> docker push dong82/emp.web:latest
```
