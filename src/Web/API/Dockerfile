# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the entire project directory into the container
COPY . .

# Restore dependencies
RUN dotnet restore ./src/Web/API/API.csproj

# Build the project
RUN dotnet build ./src/Web/API/API.csproj -c Release -o /app/build

# Publish the project
RUN dotnet publish ./src/Web/API/API.csproj -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "MyGameStat.API.dll"]
