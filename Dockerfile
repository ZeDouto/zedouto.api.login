FROM mcr.microsoft.com/dotnet/core/sdk:3.1-focal AS build
WORKDIR /app

RUN mkdir -p src
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY src/ ./
RUN dotnet restore ./Zedouto.Api.Login/Zedouto.Api.Login.csproj

RUN dotnet publish -c Release -o /out ./Zedouto.Api.Login

# Expose container port
EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT docker

RUN wget -q -O /etc/apk/keys/sgerrand.rsa.pub https://alpine-pkgs.sgerrand.com/sgerrand.rsa.pub
RUN wget https://github.com/sgerrand/alpine-pkg-glibc/releases/download/2.32-r0/glibc-2.32-r0.apk
RUN apk add glibc-2.32-r0.apk

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-focal
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Zedouto.Api.Login.dll"]