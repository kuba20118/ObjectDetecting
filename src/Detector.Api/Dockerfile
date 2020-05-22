FROM mcr.microsoft.com/dotnet/core/sdk:3.1
RUN apt-get update && apt-get install -y \
    libgomp1 \
    libgdiplus \
    libc6-dev \
    apt-utils 

WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT development
ENTRYPOINT dotnet Detector.Api.dll