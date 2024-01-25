FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /ConfirmationService
COPY ["ConfirmationService.Host/ConfirmationService.Host.csproj", "ConfirmationService.Host/"]
COPY ["ConfirmationService.BusinessLogic/ConfirmationService.BusinessLogic.csproj", "ConfirmationService.BusinessLogic/"]
COPY ["ConfirmationService.Core/ConfirmationService.Core.csproj", "ConfirmationService.Core/"]
COPY ["ConfirmationService.Infrastructure/ConfirmationService.Infrastructure.csproj", "ConfirmationService.Infrastructure/"]
COPY ["ConfirmationService.Tests/ConfirmationService.Tests.csproj", "ConfirmationService.Tests/"]

RUN dotnet restore "ConfirmationService.Host/ConfirmationService.Host.csproj"
COPY . .
WORKDIR "/ConfirmationService/ConfirmationService.Host"

RUN dotnet build "ConfirmationService.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConfirmationService.Host.csproj" -c Release -o /app/publish

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConfirmationService.Host.dll"]


