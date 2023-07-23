#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["C2_API.csproj", "./"]
RUN dotnet restore "C2_API.csproj"
COPY . .
WORKDIR "/src/2C_API"
RUN dotnet build "C2_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "C2_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "C2_API.dll"]