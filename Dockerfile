# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Set the working directory to /app
WORKDIR /app
EXPOSE 80

# Copy the project files from the current directory to the Docker image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /ConfirmationService

COPY . ./
# Restore the NuGet packages
RUN dotnet restore

# Build the project
RUN dotnet build

# Start the project when the container starts
ENTRYPOINT ["dotnet", "run", "--project", "ConfirmationService.Host"]