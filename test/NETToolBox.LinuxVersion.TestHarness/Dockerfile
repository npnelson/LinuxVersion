#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["tests/NETToolBox.LinuxVersion.TestHarness/NETToolBox.LinuxVersion.TestHarness.csproj", "tests/NETToolBox.LinuxVersion.TestHarness/"]
COPY ["src/NETToolBox.LinuxVersion/NETToolBox.LinuxVersion.csproj", "src/NETToolBox.LinuxVersion/"]
RUN dotnet restore "tests/NETToolBox.LinuxVersion.TestHarness/NETToolBox.LinuxVersion.TestHarness.csproj"
COPY . .
WORKDIR "/src/tests/NETToolBox.LinuxVersion.TestHarness"
RUN dotnet build "NETToolBox.LinuxVersion.TestHarness.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NETToolBox.LinuxVersion.TestHarness.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS final 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NETToolBox.LinuxVersion.TestHarness.dll"]