#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
ENV ConnectionStrings:DefaultConnection=

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FindMeApi/FindMeApi.csproj", "FindMeApi/"]
RUN dotnet restore "FindMeApi/FindMeApi.csproj"
COPY . .
WORKDIR "/src/FindMeApi"
RUN dotnet build "FindMeApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FindMeApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FindMeApi.dll"]