FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY MaintenanceTasksManager.sln ./
COPY ApplicationService/*.csproj ./ApplicationService/
COPY Messaging/*.csproj ./Messaging/
COPY DataAccess/*.csproj ./DataAccess/
COPY Presentation/*.csproj ./Presentation/
COPY Tests.ApplicationService/*.csproj ./Tests.ApplicationService/
COPY Tests.Presentation/*.csproj ./Tests.Presentation/

RUN dotnet restore
COPY . .
WORKDIR /src
RUN dotnet build MaintenanceTasksManager.sln -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Presentation.dll"]
