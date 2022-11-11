FROM mcr.microsoft.com/dotnet/sdk:5.0-focal

RUN apt-get update && \
    apt-get install -y \
      openjdk-11-jdk \
      ca-certificates-java && \
    apt-get clean;

RUN update-ca-certificates -f

ENV JAVA_HOME /usr/lib/jvm/java-11-openjdk-amd64/
RUN export JAVA_HOME

ARG APP_ENV="Development"
ENV ASPNETCORE_ENVIRONMENT ${APP_ENV}
ENV SERVICE_NAME="mytask"

WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj  ./
RUN dotnet restore

# copy everything else and build 
COPY . ./

WORKDIR /app
RUN dotnet publish -c Release -o out

COPY ./Liquibase /app/out/Liquibase
COPY run-dev.bash /app/out/run-dev.bash
RUN sed -i 's/\r$//'  /app/out/run-dev.bash

WORKDIR /app/out
