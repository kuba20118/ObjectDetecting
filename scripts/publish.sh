#!/bin/bash
echo "##"
pwd
ls
echo "##"
dotnet publish ./src/Detector.Api -c Release -o ./bin/Docker
echo "##"
pwd
ls ./bin/Docker
echo "##"