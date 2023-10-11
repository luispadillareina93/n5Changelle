FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["n5.permissions.WebApi.csproj", "n5.permissions/"]
COPY ["./n5.permissions.Application.csproj", "n5.permissions.Application/"]
COPY ["n5.permissions.Infraestructure/n5.permissions.Infraestructure.csproj", "n5.permissions.Infraestructure/"]
COPY ["n5.permissions.Domain/n5.permissions.Domain.csproj", "n5.permissions.Domain/"]
RUN dotnet restore "n5.permissions/n5.permissions.WebApi.csproj"
COPY . .
WORKDIR "/src/n5.permissions"
RUN dotnet build "n5.permissions.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "n5.permissions.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "n5.permissions.WebApi.dll"]