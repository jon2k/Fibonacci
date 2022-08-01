﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Fibonacci/Fibonacci.csproj", "Fibonacci/"]
RUN dotnet restore "Fibonacci/Fibonacci.csproj"
COPY . .
WORKDIR "/src/Fibonacci"
RUN dotnet build "Fibonacci.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fibonacci.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fibonacci.dll"]