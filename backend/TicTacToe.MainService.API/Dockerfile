FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TicTacToe.MainService.API/TicTacToe.MainService.API.csproj", "TicTacToe.MainService.API/"]
RUN dotnet restore "TicTacToe.MainService.API/TicTacToe.MainService.API.csproj"
COPY . .
WORKDIR "/src/TicTacToe.MainService.API"
RUN dotnet build "TicTacToe.MainService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TicTacToe.MainService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.MainService.API.dll"]
