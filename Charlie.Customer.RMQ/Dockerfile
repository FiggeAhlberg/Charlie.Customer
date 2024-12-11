#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Charlie.Customer.RMQ/Charlie.Customer.RMQ.csproj", "Charlie.Customer.RMQ/"]
RUN dotnet restore "./Charlie.Customer.RMQ/./Charlie.Customer.RMQ.csproj"
COPY . .
WORKDIR "/src/Charlie.Customer.RMQ"
RUN dotnet build "./Charlie.Customer.RMQ.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Charlie.Customer.RMQ.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Charlie.Customer.RMQ.dll"]