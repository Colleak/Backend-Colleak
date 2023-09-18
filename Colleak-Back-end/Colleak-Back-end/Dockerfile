#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Colleak-Back-end/Colleak-Back-end.csproj", "Colleak-Back-end/"]
RUN dotnet restore "Colleak-Back-end/Colleak-Back-end.csproj"
COPY . .
WORKDIR "/src/Colleak-Back-end"
RUN dotnet build "Colleak-Back-end.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Colleak-Back-end.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Colleak-Back-end.dll"]