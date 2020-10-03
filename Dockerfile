FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/Zedouto.Api.Login/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY src/Zedouto.Api.Login ./
WORKDIR /app
RUN dotnet publish -c Release -o /out

# Expose container port
EXPOSE 8080

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Zedouto.Api.Login.dll"]