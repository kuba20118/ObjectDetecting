#!/bin/bash
echo "##"
pwd
ls
echo "##"
dotnet publish ./src/Detector.Api -c Release -o ./src/Detector.Api/bin/Docker
echo "##"
pwd
ls ./bin/Docker
echo "##"