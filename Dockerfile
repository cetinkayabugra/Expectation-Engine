FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ExpectationEngine.API/ExpectationEngine.API.csproj", "ExpectationEngine.API/"]
RUN dotnet restore "ExpectationEngine.API/ExpectationEngine.API.csproj"
COPY . .
WORKDIR "/src/ExpectationEngine.API"
RUN dotnet build "ExpectationEngine.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExpectationEngine.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpectationEngine.API.dll"]
