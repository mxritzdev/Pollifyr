FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lephu-Umfrage/Lephu-Umfrage.csproj", "Lephu-Umfrage/"]
RUN dotnet restore "Lephu-Umfrage/Lephu-Umfrage.csproj"
COPY . .
WORKDIR "/src/Lephu-Umfrage"
RUN dotnet build "Lephu-Umfrage.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lephu-Umfrage.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lephu-Umfrage.dll"]
