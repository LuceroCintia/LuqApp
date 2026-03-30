#!/usr/bin/env bash
set -euo pipefail

if ! command -v dotnet >/dev/null 2>&1; then
  echo "ERROR: dotnet SDK no está instalado en este entorno."
  echo "Instalá .NET 8 SDK y luego ejecutá:"
  echo "  dotnet restore Taller.sln"
  echo "  dotnet test tests/Taller.Tests/Taller.Tests.csproj -c Debug"
  exit 127
fi

dotnet --info

dotnet restore Taller.sln
dotnet test tests/Taller.Tests/Taller.Tests.csproj -c Debug --nologo --verbosity minimal
