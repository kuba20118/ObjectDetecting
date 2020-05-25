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

echo "## .Core"

echo "1. log in"
docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
pwd
cd ./src/Detector.Api
pwd
echo "2. building"
docker build -t detector.api:latest .
echo "3. tagging"
docker tag detector.api:latest $DOCKER_USERNAME/detector.api:latest
echo "4. pushing"
docker push $DOCKER_USERNAME/detector.api:latest

echo "## React"
pwd
cd ../
pwd
cd ./front-end
pwd
echo "5. building"
docker build -t detector.front:latest .
echo "6. tagging"
docker tag detector.front:latest $DOCKER_USERNAME/detector.front:latest
echo "7. pushing"
docker push $DOCKER_USERNAME/detector.front:latest


