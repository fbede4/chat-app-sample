#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ChatApp.Api/ChatApp.Api.csproj", "ChatApp.Api/"]
COPY ["ChatApp.Domain/ChatApp.Domain.csproj", "ChatApp.Domain/"]
COPY ["ChatApp.Application/ChatApp.Application.csproj", "ChatApp.Application/"]
COPY ["ChatApp.Dal/ChatApp.Dal.csproj", "ChatApp.Dal/"]
RUN dotnet restore "ChatApp.Api/ChatApp.Api.csproj"
COPY . .
WORKDIR "/src/ChatApp.Api"
RUN dotnet build "ChatApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatApp.Api.dll"]