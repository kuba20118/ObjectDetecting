FROM microsoft/dotnet:runtime
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIROMENT docker
ENTRYPOINT dotnet Detector.Api.dll