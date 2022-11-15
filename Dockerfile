FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY ["GDifare.Distribucion.Clientes.API.csproj", "./"]
COPY ["nuget.config", "./"]
RUN dotnet restore "GDifare.Distribucion.Clientes.API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "GDifare.Distribucion.Clientes.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "GDifare.Distribucion.Clientes.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GDifare.Distribucion.Clientes.API.dll"]