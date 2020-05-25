#!/bin/bash
DOCKER_ENV=''
DOCKER_TAG=''
case "$TRAVIS_BRANCH" in
  "master")
    DOCKER_ENV=production
    DOCKER_TAG=latest
    ;;
  "develop")
    DOCKER_ENV=development
    DOCKER_TAG=dev
    ;;    
esac

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
echo "1. logged"
#cd ./src/Detector.Api
echo "###"
pwd
ls 
echo "###"
docker build -f ./src/Detector.Api/Dockerfile -t detector.api:latest ./src/Detector.Api
echo "2. built"
echo "###"
pwd
ls ./src/Detector.Api/bin/docker
echo "###"
echo "###"
pwd
ls
echo "###"
docker tag detector.api:latest $DOCKER_USERNAME/detector.api:latest
echo "3. tagged"
docker push $DOCKER_USERNAME/detector.api:latest
echo "4. pushed"