#!/bin/bash
set -e

echo "Aplicando migrations..."
#dotnet ef database update --no-build

echo "Iniciando a aplicação..."
dotnet api.dll
