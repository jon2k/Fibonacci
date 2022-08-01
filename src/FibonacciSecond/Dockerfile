FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FibonacciSecond/FibonacciSecond.csproj", "FibonacciSecond/"]
RUN dotnet restore "FibonacciSecond/FibonacciSecond.csproj"
COPY . .
WORKDIR "/src/FibonacciSecond"
RUN dotnet build "FibonacciSecond.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FibonacciSecond.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FibonacciSecond.dll"]
