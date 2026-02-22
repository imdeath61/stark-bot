FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiar archivos y restaurar
COPY AcV2/*.csproj ./AcV2/
RUN dotnet restore AcV2/AcV2.csproj

# Copiar todo y compilar
COPY . .
RUN dotnet publish AcV2/AcV2.csproj -c Release -o out

# Usar imagen con soporte para Windows Desktop
FROM mcr.microsoft.com/dotnet/desktop:10.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "AcV2.dll"]