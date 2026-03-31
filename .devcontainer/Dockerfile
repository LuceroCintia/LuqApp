FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /workspace/LuqApp

# Optional quality-of-life tools
RUN apt-get update && apt-get install -y --no-install-recommends \
    git curl ca-certificates && \
    rm -rf /var/lib/apt/lists/*

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_NOLOGO=1
