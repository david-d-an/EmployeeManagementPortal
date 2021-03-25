#!/bin/bash

if [ -z "$1" ] 
    then
    echo "Tag name is required"
    exit 1
fi
tag_num=$1

echo
echo "### Publishing Release code"
dotnet publish -c Release
echo
echo "### Building image"
docker build -f ./DockerSettings/Dockerfile -t emp.sts .

imageid=$(docker images -q emp.sts:latest)
echo
echo "### Tagging image (${imageid}) to: ${tag_num}"
docker tag $(docker images -q emp.sts:latest) dong82/emp.sts:$tag_num
echo
echo "### Tagging image (${imageid}) to: latest"
docker tag dong82/emp.sts:$tag_num dong82/emp.sts:latest

echo
echo "### Pushing image (${imageid}) to Docker Hub: dong82/emp.sts:latest"
docker push dong82/emp.sts:$tag_num
echo
echo "### Pushing image (${imageid}) to Docker Hub: dong82/emp.sts:${tag_num}"
docker push dong82/emp.sts:latest
