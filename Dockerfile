FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY AcV2/*.csproj ./AcV2/
RUN dotnet restore AcV2/AcV2.csproj
COPY . .
RUN dotnet publish AcV2/AcV2.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "AcV2.dll"]
