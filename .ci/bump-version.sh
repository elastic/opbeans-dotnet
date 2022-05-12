#!/usr/bin/env bash
set -euxo pipefail

AGENT_VERSION="${1?Missing the APM dotnet agent version}"

## In case the version is tag based in the format v<major>.<minor>.<patch> (v5.3.1)
AGENT_VERSION_WITHOUT_PREFIX=${AGENT_VERSION//v/}

## Use docker to bump the version to ensure the environment is easy to reproduce.
docker run --rm -t \
  --user $UID \
  -w /app \
  -e HOME=/tmp \
  -e AGENT_VERSION="${AGENT_VERSION_WITHOUT_PREFIX}" \
  -v "$(pwd)/opbeans-dotnet:/app" \
  mcr.microsoft.com/dotnet/core/sdk:3.1 /bin/sh -c "
    dotnet add package Elastic.Apm.NetCoreAll -v ${AGENT_VERSION_WITHOUT_PREFIX}"

## Bump agent version in the Dockerfile
sed -ibck "s#\(org.label-schema.version=\)\(\".*\"\)\(.*\)#\1\"${AGENT_VERSION_WITHOUT_PREFIX}\"\3#g" Dockerfile

# Commit changes
git add opbeans-dotnet/opbeans-dotnet.csproj Dockerfile
git commit -m "Bump version ${AGENT_VERSION_WITHOUT_PREFIX}"
