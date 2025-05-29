# Etapa de build
FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /app

COPY *.sln ./
COPY Api/*.csproj ./Api/
RUN dotnet restore Api/api.csproj

COPY . ./
WORKDIR /app/Api
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS runtime
WORKDIR /app

COPY --from=build /app/Api/out ./
COPY --from=build /app/Api/appsettings*.json ./
#Copia configurações

COPY apply-migrate-and-run.sh ./
RUN chmod +x apply-migrate-and-run.sh

ENTRYPOINT ["./apply-migrate-and-run.sh"]
