#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY Hahn.ApplicationProcess.May2020.Web/Hahn.ApplicationProcess.May2020.Web.csproj Hahn.ApplicationProcess.May2020.Web/
COPY Hahn.ApplicationProcess.May2020.Data/Hahn.ApplicationProcess.May2020.Data.csproj Hahn.ApplicationProcess.May2020.Data/
COPY Hahn.ApplicationProcess.May2020.Domain/Hahn.ApplicationProcess.May2020.Domain.csproj Hahn.ApplicationProcess.May2020.Domain/
RUN dotnet restore "Hahn.ApplicationProcess.May2020.Web/Hahn.ApplicationProcess.May2020.Web.csproj"
COPY . .
WORKDIR "/src/Hahn.ApplicationProcess.May2020.Web"
RUN dotnet build "Hahn.ApplicationProcess.May2020.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hahn.ApplicationProcess.May2020.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hahn.ApplicationProcess.May2020.Web.dll"]
