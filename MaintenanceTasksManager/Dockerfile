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
WORKDIR /src/ApplicationService
RUN dotnet build -c Release -o /app

WORKDIR /src/Messaging
RUN dotnet build -c Release -o /app

WORKDIR /src/DataAccess
RUN dotnet build -c Release -o /app

WORKDIR /src/Presentation
RUN dotnet build -c Release -o /app

WORKDIR /src/Tests.ApplicationService
RUN dotnet build -c Release -o /app

WORKDIR /src/Tests.Presentation
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MaintenanceTasksManager.dll"]
