FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

RUN mkdir -p src
WORKDIR /src


# Copy csproj and restore as distinct layers
COPY src/ ./
RUN dotnet restore ./Zedouto.Api.Login/Zedouto.Api.Login.csproj

RUN dotnet publish -c Release -o /out ./Zedouto.Api.Login

# Expose container port
EXPOSE 8080

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Zedouto.Api.Login.dll"]