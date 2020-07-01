#!/usr/bin/env bash
set -euxo pipefail

AGENT_VERSION="${1?Missing the APM dotnet agent version}"

## Use docker to bump the version to ensure the environment is easy to reproduce.
docker run --rm -t \
  --user $UID \
  -w /app \
  -e HOME=/tmp \
  -e AGENT_VERSION="${AGENT_VERSION}" \
  -v "$(pwd)/opbeans-dotnet:/app" \
  mcr.microsoft.com/dotnet/core/sdk:2.2 /bin/sh -c "
    dotnet add package Elastic.Apm.NetCoreAll -v ${AGENT_VERSION}"

## Bump agent version in the Dockerfile
sed -ibck "s#\(org.label-schema.version=\)\(.*\)#\1\"${AGENT_VERSION}\"#g" Dockerfile

exit 0
# Commit changes
git add opbeans-dotnet/opbeans-dotnet.csproj Dockerfile
git commit -m "Bump version ${AGENT_VERSION}"
