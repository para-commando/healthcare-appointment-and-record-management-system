# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5010

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["authentication-management.csproj", "."]
RUN dotnet restore "./authentication-management.csproj"
COPY . .
RUN dotnet build "authentication-management.csproj" -c Release -o /app/build

# Publish image
FROM build AS publish
RUN dotnet publish "authentication-management.csproj" -c Release -o /app/publish

# Final image that will be run
FROM base AS final
WORKDIR /app
# -from flag in the COPY command refers to a specific build stage
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "authentication-management.dll"]

# more on multi stage builds https://docs.docker.com/get-started/docker-concepts/building-images/multi-stage-builds/

# "-c Release" commands specifies the configuration for the build or publish process:

#   Release Configuration:
#   Optimized for performance.
#   Produces a smaller, more efficient executable suitable for production environments.
#   Disables debugging symbols and unnecessary metadata.
#   Without this flag, the default configuration is usually Debug, which includes additional debugging information and is not optimized for performance.