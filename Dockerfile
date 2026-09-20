FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY . /src/

WORKDIR /src
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.7-alpine AS runtime
WORKDIR /app
COPY --from=build /src/out ./
COPY --from=opbeans/opbeans-frontend:latest /app/build /opbeans-frontend

LABEL \
    org.label-schema.schema-version="1.0" \
    org.label-schema.vendor="Elastic" \
    org.label-schema.name="opbeans-dotnet" \
    org.label-schema.version="1.33.0" \
    org.label-schema.url="https://hub.docker.com/r/opbeans/opbeans-dotnet" \
    org.label-schema.vcs-url="https://github.com/elastic/opbeans-dotnet" \
    org.label-schema.license="MIT"

EXPOSE 80
ENTRYPOINT ["dotnet", "opbeans-dotnet.dll"]
