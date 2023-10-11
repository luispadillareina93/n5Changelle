#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["n5.permissions/n5.permissions.WebApi.csproj", "n5.permissions/"]
COPY ["n5.permissions.Application/n5.permissions.Application.csproj", "n5.permissions.Application/"]
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