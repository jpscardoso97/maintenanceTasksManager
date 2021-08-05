FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ManagementTasksManager/ManagementTasksManager.csproj", "ManagementTasksManager/"]
RUN dotnet restore "ManagementTasksManager/ManagementTasksManager.csproj"
COPY . .
WORKDIR "/src/ManagementTasksManager"
RUN dotnet build "ManagementTasksManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ManagementTasksManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ManagementTasksManager.dll"]
